import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PostService } from 'src/app/shared/post.service';
import { Subscribe, SubscribeService } from 'src/app/shared/subscribe.service';
import { UploadImageService } from 'src/app/shared/upload-image.service';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  userDetails: any;
  currentUserName: any;
  pageStatistic: any;
  registerUserName: any;
  isFriends: boolean = true;
  isOnMyPage: boolean = true;
  isImageLoading: boolean = false;
  imageUrl: any;

  constructor(private userService: UserService, private subscribeService: SubscribeService, 
    private postService: PostService, private router: Router, private imageService: UploadImageService) { }

  ngOnInit(): void {
    this.currentUserName = localStorage.getItem('currentUser');
    this.registerUserName = localStorage.getItem('registerUser');
    this.isOnMyPage = this.currentUserName === this.registerUserName;

    this.userService.getUserProfile(this.currentUserName).subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );

      this.getPageStatistic();

    this.subscribeService.isFriend(this.currentUserName, this.registerUserName).subscribe(
      res => {
        this.isFriends = res as unknown as boolean;
      },
      err => {
        console.log(err);
      },
    );

    this.postService.postAdded_Observable.subscribe(res => {
      this.getPageStatistic();
    });

    this.getImageFromService();
    if(!this.isImageLoading) {
      
    }
  }

  getPageStatistic() {
    this.userService.getPageStatistic(this.currentUserName).subscribe(
      res => {
        this.pageStatistic = res;
      },
      err => {
        console.log(err);
      },
    );
  }

  model: Subscribe = new Subscribe();
  onFollow() {
    this.model.bloggerUserName = this.currentUserName;
    this.model.subscriberUserName = this.registerUserName;

    this.subscribeService.follow(this.model).subscribe(
      res => {
        this.getPageStatistic();
        this.isFriends = true;
        this.subscribeService.notifySubscribeAddition();
      },
      err => {
        console.log(err);
      },
    );
  }

  onUnsubscribe() {
    this.subscribeService.unsubscribe(this.currentUserName, this.registerUserName).subscribe(
      res => {
        this.getPageStatistic();
        this.isFriends = false;
        this.subscribeService.notifySubscribeAddition();
      },
      err => {
        console.log(err);
      },
    );

  }

  onEditProfile() {
    this.router.navigateByUrl('/editProfile');
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
this.imageService.downloadFile(this.currentUserName).subscribe(data => {
 switch (data.type) {
   case HttpEventType.DownloadProgress:
     break;
     case HttpEventType.Response:
       const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });
       this.createImageFromBlob(downloadedFile);
       this.isImageLoading = true;
       break;
}
}, error => {
 this.isImageLoading = false;
 console.log(error);
});
}


}
