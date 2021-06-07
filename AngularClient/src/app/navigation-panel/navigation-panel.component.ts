import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SubscribeService } from '../shared/subscribe.service';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-navigation-panel',
  templateUrl: './navigation-panel.component.html',
  styleUrls: ['./navigation-panel.component.css']
})
export class NavigationPanelComponent implements OnInit {
  registerUserName: any;
  @ViewChild('closeBtn') closeBtn: ElementRef | undefined;
  allUsers: any[] = [];
  public searchString!: string;

  constructor(private router: Router, private userService: UserService, private subscribeService: SubscribeService) { }

  ngOnInit(): void {
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

  toHome() {
    this.registerUserName = localStorage.getItem('registerUser');
    localStorage.setItem('currentUser', this.registerUserName);
    this.router.navigate(['/home']);
  }

  getAllUser() {
    this.userService.getAllUser().subscribe(
      (result: any) => {
        console.log('result is ', result);
  		  this.allUsers = result;
      },
      err => {
        console.log(err);
      },
    )
  }

  showUser(userName: string) {
    localStorage.setItem('currentUser', userName);

    this.closeBtn?.nativeElement.click();
    if(localStorage.getItem('currentUser') === localStorage.getItem('registerUser')) {
      this.router.navigateByUrl('/home');
      return;
    }

    this.router.navigateByUrl('/guest');
    this.subscribeService.notifyGuestPageChange();
  }
}
