import { Component, OnInit } from '@angular/core';
import { CookieService } from 'angular2-cookie/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    name: string = '';
  constructor(private _cookieService: CookieService,public router: Router) { }

  ngOnInit() {
      
          this.name = this._cookieService.get('name');
          
     
  }
  signOut(): void
  {
    this._cookieService.removeAll();
    this.router.navigate(['/signIn']);


}

}
