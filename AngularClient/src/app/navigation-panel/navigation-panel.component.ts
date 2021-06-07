import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private router: Router, private service: UserService) { }

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
    this.service.getAllUser().subscribe(
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
    this.router.navigateByUrl('/guest');
  }
}
