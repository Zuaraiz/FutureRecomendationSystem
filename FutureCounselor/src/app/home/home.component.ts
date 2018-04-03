import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public auth: AuthService, public router: Router) { }

  ngOnInit() {
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['/dashboard/home']);
    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

}
