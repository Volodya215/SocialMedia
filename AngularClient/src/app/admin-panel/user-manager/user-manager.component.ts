import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-manager',
  templateUrl: './user-manager.component.html',
  styles: [
  ]
})
export class UserManagerComponent implements OnInit {
  allUsers: any[] = [];

  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAllUser();
  }

  getAllUser() {
    this.userService.getByAdminAllUser().subscribe(
      (result: any) => {
  		  this.allUsers = result;
      },
      err => {
        console.log(err);
      },
    );
  }

  deleteUser(id: string) {
    this.userService.deleteUser(id).subscribe(
      (result: any) => {
  		  this.toastr.success('User deleted!', 'User successfully deleted');
        this.getAllUser();
      },
      err => {
        this.toastr.error('Error to delete!', 'Something happened on the server');
        console.log(err);
      }
    );
  }

}
