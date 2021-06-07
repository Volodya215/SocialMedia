import { Component, OnInit } from '@angular/core';
import { UploadImageService } from 'src/app/shared/upload-image.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
  providers:[UploadImageService]
})
export class EditProfileComponent implements OnInit {
  imageUrl: string = "https://bootdey.com/img/Content/avatar/avatar7.png";
  fileToUpload: any;

  constructor(private imageService : UploadImageService) { }

  ngOnInit(): void {
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

  OnSubmit(Caption: any, Image: any){
   this.imageService.postFile(Caption.value,this.fileToUpload).subscribe(
     data =>{
       console.log('done');
       Caption.value = null;
       Image.value = null;
       this.imageUrl = "/assets/img/default-image.png";
     }
   );
  }
}
