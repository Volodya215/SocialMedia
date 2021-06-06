import { Component, OnInit } from '@angular/core';
import { SubscribeService } from 'src/app/shared/subscribe.service';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit {
  usersFollowers: any[] = [];
  usersFollowing: any[] = [];
  userName: any;

  constructor(private service: SubscribeService) { }

  ngOnInit(): void {
    this.getAllFollowers();
    this.getAllFollowing();
  }

  getAllFollowers() {
    this.userName = localStorage.getItem('currentUser');
    this.service.getAllFollowers(this.userName).subscribe((result: any) => {
  		console.log('result is ', result);
  		this.usersFollowers = result;
  	});
  }

  getAllFollowing() {
    this.userName = localStorage.getItem('currentUser');
    this.service.getAllFollowing(this.userName).subscribe((result: any) => {
  		console.log('result is ', result);
  		this.usersFollowing = result;
  	});
  }

  showUser() {

  }
  
}
