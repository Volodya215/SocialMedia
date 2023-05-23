import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UploadImageService } from 'src/app/shared/upload-image.service';
import { UserService } from 'src/app/shared/user.service';
import { UserInterest, InterestsService } from 'src/app/shared/interest.service';
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
  selectedInterests: any;
  allInterests: any;
  showModal: boolean = false;

  constructor(private imageService : UploadImageService, public service: UserService, public interestService: InterestsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userName = localStorage.getItem('registerUser');

    this.getImageFromService();

    this.interestService.getAllInterests().subscribe(
      (res: any) => {
        this.allInterests = res;
      },
      err => {
        console.log(err);
      },
    );

    this.getUserInterests();

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
      this.isImageLoading = true;
    }
    reader.readAsDataURL(this.fileToUpload);
  }

  OnSubmitFoto(Image: any){
   this.imageService.postFile(this.fileToUpload, this.userName).subscribe(
     data =>{
      this.toastr.success('Foto changed!', 'New user foto');
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

getUserInterests() {
      this.interestService.getAllUserInterests(this.userName).subscribe((res : any) => {
        this.selectedInterests = res;
      }, error => {
      console.log(error);
    });
}

openModal() {
  this.showModal = true;
}

closeModal() {
  this.showModal = false;
}

toggleInterest(event: any, interestId: number) {
  const checked = event.target.checked;

  if (checked) {
    this.addUserInterests(interestId);
  } else {
    this.deleteUserInterests(interestId);
  }
}

isInterestSelected(interest: any): boolean {
  return this.selectedInterests.find((x: { id: any; }) => x.id == interest.id);
}

addUserInterests(id: number) {
  let inter = new UserInterest();
  inter.interestId = id;
  inter.userName = this.userName;

  this.interestService.addUserInterest(inter).subscribe(
    (res: any) => {
    if (res.succeeded) {
      this.toastr.success('Interest added!', 'New user interest added!');
    } else {
      this.toastr.error('Something went wrong', 'Request to add user ineterst failed.');
    }
  },
  err => {
    console.log(err);
  });
}

deleteUserInterests(id: number) {
  let inter = new UserInterest();
  inter.interestId = id;
  inter.userName = this.userName;

  this.interestService.deleteInterest(this.userName, id).subscribe(
    (res: any) => {
      if (res.succeeded) {
        this.toastr.success('Interest deleted!', 'Interest deleted!');
        this.getUserInterests();
      } else {
        this.toastr.error('Something went wrong', 'Request to delete user ineterst failed.');
      }
    },
    err => {
      console.log(err);
    }
  );
}

}
