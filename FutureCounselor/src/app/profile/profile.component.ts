import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from '../user.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Options } from '../Models/user';



@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  errorMessage: string='asss';
  email: string = 'usman@gmail.com';
  name: string = 'asss';
  animal: string = 'asss';
  userInfo: any;
  hobbyList: any[] = [];
  skillList: any[] = [];
  tempSkillList: Options[] = [];
  interestList: any[] = [];
  hobbyUserList: any[] = [];
  skillUserList: any[] = [];
  interestUserList: any[] = [];


  constructor(private auth: AuthService, private service: UserService, public dialog: MatDialog, public router: Router) { }


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

      data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.animal = result;
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
      error => this.errorMessage = <any>error, () => this.tempSkillList=this.getListData(this.skillList));
 
  }
  getListData(list:any[]): Options[] {
    let array : Options[] = [];
    list.forEach(function (value) {
      let temp: Options;
      temp.id = value.id;
      temp.name = value.name;
      temp.rating = 20;
      temp.ratinglabel = 'very low';
      array.push(temp);
    });
    return array;

  }


  getuserskilllist(): void {
    this.service.getUserSkills(this.email).subscribe
      (any => this.skillUserList = any,
      error => this.errorMessage = <any>error);

  }

  ngOnInit() {
    this.getskilllist();
    this.getuserhobbylist();
    this.getuserinfo();
    this.gethobbylist();
    this.getuserintersetlist();
    this.getuserskilllist();
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['/dashboard/profile']);
    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

}
@Component({
 
  templateUrl: './hobbyDialog.html',
})
export class HobbyDialog implements OnInit {
  errorMessage: string = 'asss';
  email: string = 'usman@gmail.com';
  constructor(private service: UserService,
    public dialogRef: MatDialogRef<HobbyDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  AddHobby(input:any ): void {
    this.service.addhobby(this.email,input.id).subscribe (
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
  email: string = 'usman@gmail.com';
  rating:any = [
    { value: 20, viewValue: 'Very Low' },
    { value: 40, viewValue: 'Low' },
    { value: 60, viewValue: 'Moderate' },
    { value: 80, viewValue: 'High' },
    { value: 100, viewValue: 'Very High' }
  ];



  constructor(private service: UserService,
    public dialogRef: MatDialogRef<SkillDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  
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

  constructor(
    public dialogRef: MatDialogRef<InterestDialog>) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
  }
}
