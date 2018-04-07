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

    constructor(public auth: AuthService, public router: Router, private _cookieService: CookieService) { }

  ngOnInit() {
      if (this.auth.IsLoggedIn()) {
          this.name = this._cookieService.get('name');
      this.router.navigate(['/dashboard/home']);
    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

}
