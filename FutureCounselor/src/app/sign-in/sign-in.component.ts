import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { user } from '../Models/user';
import { Router } from '@angular/router';
import { CookieService } from 'angular2-cookie/core';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']

})
export class SignInComponent implements OnInit {
    
    errorMessage: String;
    email: string;
    password: String;
  user: any;
  constructor(private auth: AuthService, private router: Router, private _cookieService: CookieService) {
}

  ngOnInit() {
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['dashboard/home']);
    }

    }
  signIn(): void
  {
      this.auth.SignIn(this.email, this.password).subscribe
        (user => this.user = user,
        error => alert(<any>error), () => this.SaveCookies(this.email, this.user.firstName +''+ this.user.lastName, this.user.id));

  }
 


  SaveCookies(email: string, firstname: string, id: number): void {
    this._cookieService.put('name', firstname);
    this._cookieService.put('email', email);
    this._cookieService.putObject('id', id);
    this._cookieService.putObject('isSignedIn', true);
    this.router.navigate(['dashboard/home']);
   
  }
}
