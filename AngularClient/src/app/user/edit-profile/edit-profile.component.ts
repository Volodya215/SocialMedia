import { HttpEventType } from '@angular/common/http';
import { UrlResolver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { UploadImageService } from 'src/app/shared/upload-image.service';

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
  isImageLoading: boolean = true;

  constructor(private imageService : UploadImageService) { }

  ngOnInit(): void {
    this.userName = localStorage.getItem('registerUser');

    this.getImageFromService();
    if(!this.isImageLoading) {
      this.imageUrl = "https://bootdey.com/img/Content/avatar/avatar7.png";
    }
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

  OnSubmit(Image: any){
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
            this.imageUrl = reader.result;
         }, false);
  
         if (image) {
            reader.readAsDataURL(image);
         }
  }

  getImageFromService() {
    this.isImageLoading = true;
    this.imageService.downloadFile(this.userName).subscribe(data => {

      switch (data.type) {
        case HttpEventType.DownloadProgress:
          break;
          case HttpEventType.Response:
            const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });

            this.createImageFromBlob(downloadedFile);
            this.isImageLoading = false;
            break;
    }
    }, error => {
      this.isImageLoading = false;
      console.log(error);
    });
}

}
