import { Component, OnInit } from '@angular/core';
import { CookieService } from 'angular2-cookie/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    name: string = '';
    constructor(private _cookieService: CookieService) { }

  ngOnInit() {
      
          this.name = this._cookieService.get('name');
          
     
  }

}
