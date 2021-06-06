import { Component, OnInit } from '@angular/core';
import { Post, PostService } from '../shared/post.service';

@Component({
  selector: 'app-show-post',
  templateUrl: './show-post.component.html',
  styleUrls: []
})
export class ShowPostComponent implements OnInit {
  posts: any[] = [];
  userName: any;
  
  constructor(private service: PostService) { }

  ngOnInit(): void {
    this.getAllPost();

    this.service.postAdded_Observable.subscribe(res => {
      this.getAllPost();
    });
  }

  getAllPost() {
    this.userName = localStorage.getItem('currentUser');
  	this.service.getAllPost(this.userName).subscribe((result: any) => {
  		console.log('result is ', result);
  		this.posts = result;
  	});
  }

}
