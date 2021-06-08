import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UploadImageService } from 'src/app/shared/upload-image.service';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
  providers:[UploadImageService]
})
export class EditProfileComponent implements OnInit {
  imageUrl: any;
  fileToUpload: any;
  userName: any;
  isImageLoading: boolean = false;
  userDetails: any;

  constructor(private imageService : UploadImageService, public service: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userName = localStorage.getItem('registerUser');

    this.getImageFromService();

    this.service.getUserProfile(this.userName).subscribe(
      (res : any) => {
        this.userDetails = res;

        this.service.userData.setValue({
          Email: res.email,
          FullName: res.fullName
        });

        this.service.userProfile.setValue({
          Hobby: res.hobby,
          City: res.city,
          Work: res.work,
          About: res.about
        });
      },
      err => {
        console.log(err);
      },
    );
  }

  onSubmitChange() {
    this.service.userUpdate().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.toastr.success('Data updated!', 'User data updated');
        } else {
          this.toastr.error('Something went wrong', 'Updating failed.');
        }
      },
      err => {
        console.log(err);
      }
    );

    this.service.userProfileUpdate().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.toastr.success('Data updated!', 'User profile data updated');
        } else {
          this.toastr.error('Something went wrong', 'Updating failed.');
        }
      },
      err => {
        console.log(err);
      }
    );
    
  }

  handleFileInput(target: any) {
    const files = (target as HTMLInputElement).files;
    this.fileToUpload = files?.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event:any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }

  OnSubmitFoto(Image: any){
   this.imageService.postFile(this.fileToUpload, this.userName).subscribe(
     data =>{
       console.log('done');
       Image.value = null;
     }
   );
  }

  createImageFromBlob(image: Blob) {
         let reader = new FileReader();
         reader.addEventListener("load", () => {
          this.isImageLoading = true;
            this.imageUrl = reader.result;
         }, false);
  
         if (image) {
            reader.readAsDataURL(image);
         }
  }

  getImageFromService() {
    this.imageService.downloadFile(this.userName).subscribe(data => {

      switch (data.type) {
        case HttpEventType.DownloadProgress:
          break;
          case HttpEventType.Response:
            const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });

            this.createImageFromBlob(downloadedFile);
            break;
    }
    }, error => {
      this.isImageLoading = false;
      console.log(error);
    });
}

}
