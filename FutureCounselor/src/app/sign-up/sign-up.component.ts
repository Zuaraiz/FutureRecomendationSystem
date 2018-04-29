import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { signup } from '../Models/signup';
import { option } from '../Models/signup';

import { Router } from '@angular/router';
import { and } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
    locations: option[] = [];
    predegree: option[] = [];
    selectedLocation: option;
    selectedDegree: option;
    userData= new signup;
    errorMessage: String;
    sucess: number;
    constructor(private auth: AuthService, private router: Router) { }

    ngOnInit(
    ) {
        this.getLocations();
        this.getPreDegree();
      if (this.auth.IsLoggedIn()) {
        this.router.navigate(['/dashboard/home']);
      }
      else {
        this.router.navigate(['/signUp']);
      }
          
  }
    signUp(): void {
        this.userData.location = this.selectedLocation.id;
      this.userData.qualification = this.selectedDegree.id;
      if (!(this.userData.percentage <= 100) && this.userData.percentage >= 40) {
        this.userData.percentage = this.userData.percentage / 100;
        this.auth.SignUp(this.userData).subscribe
          (number => this.sucess = number,
          error => alert(<any>error), () => this.router.navigate(['/signIn']));
       
      
      }
      else {
        alert('invalid pecentage! should between(40-100)')
      }
      
  }
  getLocations(): void {
    this.auth.AllLocation()
      .subscribe(
      resultArray => this.locations = resultArray,
      error => console.log("Error :: " + error)
      , () => this.selectedLocation = this.locations[0]);
   
  }
  getPreDegree(): void {
    this.auth.AllPreDegree()
      .subscribe(
      resultArray => this.predegree = resultArray,
      error => console.log("Error :: " + error),
      () => this.selectedDegree = this.predegree[0]);
    
  }
    
}
