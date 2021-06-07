import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { windowTime } from 'rxjs/operators';
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
  @ViewChild('closeBtnFollowing') closeBtnFollowing: ElementRef | undefined;
  @ViewChild('closeBtnFollower') closeBtnFollower: ElementRef | undefined;
  public searchString!: string;

  constructor(private service: SubscribeService, private router: Router) { }

  ngOnInit(): void {
    this.getAllFollowers();
    this.getAllFollowing();

    this.service.subscribeAdded_Observable.subscribe(res => {
      this.getAllFollowers();
      this.getAllFollowing();
    });
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

  showUser(userName: string) {
    localStorage.setItem('currentUser', userName);

    this.closeBtnFollowing?.nativeElement.click();
    this.closeBtnFollower?.nativeElement.click();
    if(localStorage.getItem('currentUser') === localStorage.getItem('registerUser')) {
      this.router.navigateByUrl('/home');
      return;
    }

    this.router.navigateByUrl('/guest');
    this.service.notifyGuestPageChange();
  }
  
}
