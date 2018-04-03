import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  constructor(public router: Router, public auth: AuthService) { }

  ngOnInit() {
    if (this.auth.IsLoggedIn()) {
      this.router.navigate(['/dashboard/editProfile']);
    }
    else {
      this.router.navigate(['/signIn']);
    }
  }

}
