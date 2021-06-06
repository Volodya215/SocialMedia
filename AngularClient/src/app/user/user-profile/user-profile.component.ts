import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  userDetails: any;
  userName: any;
  pageStatistic: any;

  constructor(private service: UserService) { }

  ngOnInit(): void {
    this.userName = localStorage.getItem('currentUser');
    this.service.getUserProfile(this.userName).subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );

    this.service.getPageStatistic(this.userName).subscribe(
      res => {
        this.pageStatistic = res;
      },
      err => {
        console.log(err);
      },
    );
  }

}
