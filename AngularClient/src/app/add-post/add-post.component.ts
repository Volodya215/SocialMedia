import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Post, PostService } from '../shared/post.service';

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: []
})
export class AddPostComponent implements OnInit {
  @ViewChild('closeBtn') closeBtn: ElementRef | undefined;
  public post : Post;

  constructor(private service: PostService) { 
    this.post = new Post();
  }

  ngOnInit(): void {
  }

  addPost() {
    if(this.post?.topic && this.post.content) {
      this.service.addPost(this.post).subscribe(
        res => {
          this.closeBtn?.nativeElement.click();
          this.service.notifyPostAddition();
        },
        err => {
          console.log(err);
        },
      )
    }
  }

}
