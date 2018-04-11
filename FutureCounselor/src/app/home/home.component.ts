import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';
import { CookieService } from 'angular2-cookie/core';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  name: string = '';
  errorMessage: string = '';
  email: string = '';
  show: boolean = true;

  recommendations: any[] = [];

  constructor(public auth: AuthService, public service: UserService, public router: Router, private _cookieService: CookieService) { }

  ngOnInit() {
      if (this.auth.IsLoggedIn()) {
        this.name = this._cookieService.get('name');
        this.email = this._cookieService.get('email');
        this.router.navigate(['/dashboard/home']);
        this.getRecommendations();

    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

  getRecommendations(): void {
    this.service.getRecommendation(this.email).subscribe
      (any => this.recommendations = any,
      error => this.errorMessage = <any>error);

  }


}
