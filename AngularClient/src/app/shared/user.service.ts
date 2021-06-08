import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  readonly BaseURI = 'http://localhost:7609/api';


  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });


  userProfile = this.fb.group({
    Hobby: [''],
    City: [''],
    Work: [''],
    About: ['']
  });

  userData = this.fb.group({
    Email: ['', Validators.email],
    FullName: ['']
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');

    if (confirmPswrdCtrl?.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password')?.value != confirmPswrdCtrl?.value)
        confirmPswrdCtrl?.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl?.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/User/Register', body);
  }

  userUpdate() {
    var body = {
      Email: this.userData.value.Email,
      FullName: this.userData.value.FullName
    }
    return this.http.put(this.BaseURI + "/User/Update", body);
  }

  userProfileUpdate() {
    var body = {
      Hobby: this.userProfile.value.Hobby,
      City: this.userProfile.value.City,
      Work: this.userProfile.value.Work,
      About: this.userProfile.value.About
    }
    return this.http.put(this.BaseURI + "/UserProfile/Update", body);
  }

  login(formData: any) {
    return this.http.post(this.BaseURI + '/User/Login', formData);
  }

  getUserProfile(userName: string) {
    return this.http.get(this.BaseURI + '/UserProfile/' + userName);
  }

  getPageStatistic(userName: string) {
    return this.http.get(this.BaseURI + "/User/" + userName + "/statistic");
  }

  getAllUser() {
    return this.http.get(this.BaseURI + "/User/allUsers");
  }


  roleMatch(allowedRoles: any[]): boolean {
    var isMatch = false;
    var token = localStorage.getItem('token')?.toString();
    if (token == undefined)
      token = '';
    var payLoad = JSON.parse(window.atob(token.split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach((element: any) => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
      return true;
    });
    return isMatch;
  }
}
