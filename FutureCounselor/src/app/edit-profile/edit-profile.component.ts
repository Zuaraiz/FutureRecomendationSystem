import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { signup } from '../Models/signup';
import { option } from '../Models/signup';
import { UserService } from '../user.service';
import { CookieService } from 'angular2-cookie/core';

import { Router } from '@angular/router';
@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  constructor(public router: Router, public auth: AuthService, public service: UserService, private _cookieService: CookieService) { }
  locations: option[] = [];
  predegree: option[] = [];
  hobbyUserList: any[] = [];
  skillUserList: any[] = [];
  interestUserList: any[] = [];
  errorMessage: string = '';
  email: string = '';
  userInfo: any;
  userData = new signup;
  selectedLocation: option;
  selectedDegree: option;
  ngOnInit() {
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['/dashboard/editProfile']);
      this.email = this._cookieService.get('email');
      this.getuserinfo();
      this.getLocations();
      this.getPreDegree();
      this.getuserhobbylist();
      this.getuserintersetlist();
      this.getuserskilllist();

    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

  editProfile(): void {
 
    this.service.editUserProfile(this.email,this.userInfo).subscribe
      (any => this.userInfo = any,
      error => this.errorMessage = <any>error);
    this.router.navigate(['/signIn']);

  }
  getLocations(): void {
    this.auth.AllLocation()
      .subscribe(
      resultArray => this.locations = resultArray,
      error => console.log("Error :: " + error)
      )
    this.selectedLocation = this.locations[0];
  }
  getPreDegree(): void {
    this.auth.AllPreDegree()
      .subscribe(
      resultArray => this.predegree = resultArray,
      error => console.log("Error :: " + error)
      )
    this.selectedDegree = this.predegree[0];
  }

  getuserinfo(): void {
    this.service.getUserProfile(this.email).subscribe
      (any => this.userInfo = any,
      error => this.errorMessage = <any>error);

  }
  getuserskilllist(): void {
    this.service.getUserSkills(this.email).subscribe
      (any => this.skillUserList = any,
      error => this.errorMessage = <any>error);

  }
  getuserintersetlist(): void {
    this.service.getUserInterests(this.email).subscribe
      (any => this.interestUserList = any,
      error => this.errorMessage = <any>error);

  }

  getuserhobbylist(): void {
    this.service.getUserHobbies(this.email).subscribe
      (any => this.hobbyUserList = any,
      error => this.errorMessage = <any>error);

  }

}
