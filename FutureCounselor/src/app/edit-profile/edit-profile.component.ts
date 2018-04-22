import { Component, OnInit, Inject } from '@angular/core';
import { AuthService } from '../auth.service';
import { signup } from '../Models/signup';
import { option } from '../Models/signup';
import { UserService } from '../user.service';
import { CookieService } from 'angular2-cookie/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material/dialog';
import { MatAccordion } from '@angular/material';



import { Router } from '@angular/router';
@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  constructor(public router: Router, public dialog: MatDialog, public auth: AuthService, public service: UserService, private _cookieService: CookieService) { }
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
  
    if ((this.userInfo.percentage <= 100) && this.userInfo.percentage >= 40) {
      this.userInfo.location = this.selectedLocation.id;
      this.userInfo.qualification = this.selectedDegree.id;
      this.userInfo.percentage = this.userInfo.percentage / 100;
      this.service.editUserProfile(this.email, this.userInfo).subscribe
        (any => this.userInfo = any,
        error => alert(<any>error),
        () => {
          alert('Edit Sucessfully!');
          this.userInfo.percentage = this.userInfo.percentage * 100;
        });
      
    }
    else {
      
      alert('invalid pecentage! should between(40-100)')
    }

  }
  getLocations(): void {
    this.auth.AllLocation()
      .subscribe(
      resultArray => this.locations = resultArray,
      error => console.log("Error :: " + error),()=> this.selectedLocation = this.locations[0]
      )
   
  }
  getPreDegree(): void {
    this.auth.AllPreDegree()
      .subscribe(
      resultArray => this.predegree = resultArray,
      error => console.log("Error :: " + error), () => this.selectedDegree = this.predegree[0]
      )
   
  }

  getuserinfo(): void {
    this.service.getUserProfile(this.email).subscribe
      (any => this.userInfo = any,
      error => this.errorMessage = <any>error, () => this.userInfo.percentage = this.userInfo.percentage * 100);



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



  openHobbyDialog(): void {




    let dialogRef = this.dialog.open(HDialog, {



      data: { result: this.hobbyUserList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getuserhobbylist();
    
    });
  }

  openSkillDialog(): void {
    let dialogRef = this.dialog.open(SDialog, {

      data: { result: this.skillUserList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getuserskilllist();
     
    });
  }
  openInterestDialog(): void {
    let dialogRef = this.dialog.open(IDialog, {

      data: { result: this.interestUserList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      
      this.getuserintersetlist();
    });
  }


}

@Component({

  templateUrl: './hDialog.html',
})
export class HDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = '';
  constructor(private service: UserService,
    public dialogRef: MatDialogRef<HDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any, private _cookieService: CookieService) { }

  AddHobby(input: any): void {
    this.email = this._cookieService.get('email');
    this.service.deleteHobby(this.email, input).subscribe(
      error => this.errorMessage = <any>error);
    this.deleteMsg(input);


  }
  deleteMsg(msg: any) {
    const index: number = this.data.result.indexOf(msg);
    if (index !== -1) {
      this.data.result.splice(index, 1);
    }
  }
  onNoClick(): void {

    this.dialogRef.close();
  }
  ngOnInit() {
  }
}


@Component({

  templateUrl: './sDialog.html',
})
export class SDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = ''; 





  constructor(private service: UserService,
    public dialogRef: MatDialogRef<SDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any, private _cookieService: CookieService) { }


  AddSkill(input: any): void {
    this.email = this._cookieService.get('email');
    this.service.deleteSkill(this.email, input.name).subscribe(
      error => this.errorMessage = <any>error);
    this.deleteMsg(input);


  }
  deleteMsg(msg: any) {
    const index: number = this.data.result.indexOf(msg);
    if (index !== -1) {
      this.data.result.splice(index, 1);
    }
  }
  onNoClick(): void {

    this.dialogRef.close();
  }
  ngOnInit() {
  }
}


@Component({

  templateUrl: './iDialog.html',
})
export class IDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = ''; 
 

  constructor(
    private service: UserService,
    public dialogRef: MatDialogRef<IDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any, private _cookieService: CookieService) { }


  AddInterest(input: any): void {
    this.email = this._cookieService.get('email');

    this.service.deleteInterest(this.email, input.name).subscribe(
      error => this.errorMessage = <any>error);
    this.deleteMsg(input);


  }
  deleteMsg(msg: any) {
    const index: number = this.data.result.indexOf(msg);
    if (index !== -1) {
      this.data.result.splice(index, 1);
    }
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
  }
}
