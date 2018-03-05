import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { user } from '../Models/user';
import { Router} from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']

})
export class SignInComponent implements OnInit {
    
    errorMessage: String;
    email: String;
    password: String;
    user = new user();
    constructor(private auth: AuthService, private router: Router) {
}

  ngOnInit() {
    }
  signIn(): void
  {
      this.auth.SignIn(this.email, this.password).subscribe
          (user => this.user = user,
          error => this.errorMessage = <any>error);
      this.router.navigate(['dashboard']);
  }

}
