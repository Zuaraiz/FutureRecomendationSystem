import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material/dialog';
import { UserService } from '../user.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Options } from '../Models/user';
import { CookieService } from 'angular2-cookie/core';
import { MatAccordion } from '@angular/material';



@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  errorMessage: string = '';
  email: string = '';
 

  userInfo: any;
  hobbyList: any[] = [];
  skillList: any[] = [];
  tempSkillList: Options[] = [];
  interestList: any[] = [];
  hobbyUserList: any[] = [];
  skillUserList: any[] = [];
  interestUserList: any[] = [];


  constructor(private auth: AuthService, private service: UserService, public dialog: MatDialog, public router: Router, private _cookieService: CookieService) { }


  openHobbyDialog(): void {
   

    

    let dialogRef = this.dialog.open(HobbyDialog, {
    
     

      data: { result: this.hobbyList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getuserhobbylist();
      this.gethobbylist();
    });
  }

  openSkillDialog(): void {
    let dialogRef = this.dialog.open(SkillDialog, {

      data: { result: this.skillList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getuserskilllist();
      this.getskilllist();
    });
  }
  openInterestDialog(): void {
    let dialogRef = this.dialog.open(InterestDialog, {

      data: { result: this.interestList }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getintersetlist();
      this.getuserintersetlist();
    });
  }

  getuserinfo(): void {
    this.service.getUserProfile(this.email).subscribe
      (any => this.userInfo = any,
      error => this.errorMessage = <any>error);

  }


  gethobbylist(): void {
    this.service.getAllHobbies(this.email).subscribe
      (any => this.hobbyList = any,
      error => this.errorMessage = <any>error);

  }
  getuserhobbylist(): void {
    this.service.getUserHobbies(this.email).subscribe
      (any => this.hobbyUserList = any,
      error => this.errorMessage = <any>error);

  }


  getintersetlist(): void {
    this.service.getAllInterests(this.email).subscribe
      (any => this.interestList = any,
      error => this.errorMessage = <any>error);

  }
  getuserintersetlist(): void {
    this.service.getUserInterests(this.email).subscribe
      (any => this.interestUserList = any,
      error => this.errorMessage = <any>error);

  }


  getskilllist(): void {
    this.service.getAllSkills(this.email).subscribe
      (any => this.skillList = any,
      error => this.errorMessage = <any>error );
    //this.skillList.forEach(function (value) {
    //  let temp: Options;
    //  temp.id = value.id;
    //  temp.name = value.name;
    //  temp.rating = 20;
    //  temp.ratinglabel = 'very low';
    //  this.tempSkillList.push(temp);
    //});

  }
  getuserskilllist(): void {
    this.service.getUserSkills(this.email).subscribe
      (any => this.skillUserList = any,
      error => this.errorMessage = <any>error);

  }



  getListData(): void {
 
    

  }


  

  ngOnInit() {
    
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['/dashboard/profile']);
      this.email = this._cookieService.get('email');
    }
    else {
      this.router.navigate(['/signIn']);
    }
    this.getuserinfo();
    this.getskilllist();
    this.getuserhobbylist();

    this.gethobbylist();
    this.getuserintersetlist();
    this.getuserskilllist();
    this.getintersetlist();
    this.getuserintersetlist();
  }

}




@Component({

  templateUrl: './hobbyDialog.html',
})
export class HobbyDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = this._cookieService.get('email');
  constructor(private service: UserService,
    public dialogRef: MatDialogRef<HobbyDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,private _cookieService: CookieService) { }

  AddHobby(input: any): void {
    this.service.addhobby(this.email, input.id).subscribe(
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

  templateUrl: './skillDialog.html',
})
export class SkillDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = this._cookieService.get('email');
  rating: any = [
    { value: 20, viewValue: 'Very Low' },
    { value: 40, viewValue: 'Low' },
    { value: 60, viewValue: 'Moderate' },
    { value: 80, viewValue: 'High' },
    { value: 100, viewValue: 'Very High' }
  ];



  constructor(private service: UserService,
    public dialogRef: MatDialogRef<SkillDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any, private _cookieService: CookieService) { }


  AddSkill(input: any): void {
    const selectedValue: number = 20;
    this.service.addSkill(this.email, input.id, input.rating).subscribe(
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

  templateUrl: './interestDialog.html',
})
export class InterestDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = this._cookieService.get('email');
  rating: any = [
    { value: 20, viewValue: 'Very Low' },
    { value: 40, viewValue: 'Low' },
    { value: 60, viewValue: 'Moderate' },
    { value: 80, viewValue: 'High' },
    { value: 100, viewValue: 'Very High' }
  ];

  constructor(
    private service: UserService,
    public dialogRef: MatDialogRef<SkillDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any, private _cookieService: CookieService) { }


  AddInterest(input: any): void {
    const selectedValue: number = 20;
    this.service.addInterest(this.email, input.id, input.rating).subscribe(
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
