import { Component, OnInit } from '@angular/core';
import { Post, PostService } from '../../shared/post.service';

@Component({
  selector: 'main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  posts: any[] = [];
  userName: any;
  
  constructor(private service: PostService) { }

  ngOnInit(): void {
    this.getAllFriendsPost();

    this.service.postAdded_Observable.subscribe(res => {
      this.getAllFriendsPost();
    });
  }

  getAllFriendsPost() {
    this.userName = localStorage.getItem('currentUser');
  	this.service.getFriendsPosts(this.userName).subscribe((result: any) => {
  		console.log('result is ', result);
  		this.posts = result;
  	});
  }

}
