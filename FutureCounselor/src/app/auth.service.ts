import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { user } from './Models/user'
import { option } from './Models/signup'
import { signup } from './Models/signup'


@Injectable()
export class AuthService {

  
   
    private Url = 'http://localhost:50175/';  // URL to web api
 
  constructor(
    private http: Http) { }

  SignIn(email: String, password: String): Observable<user> {
      let headers = new Headers({ 'Content-Type': 'application/json'});
      let options = new RequestOptions({ headers: headers });
      let data = { email: email, password: password };
      return this.http.post(this.Url +'api/Signin', data, options).map(this.extractData)
          .catch(this.handleErrorObservable);;
  }

  SignUp(newUser: signup): Observable<number>{
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this.http.put(this.Url + 'api/Signup', newUser, options).map(this.extractData)
          .catch(this.handleErrorObservable);;
  }
  AllLocation(): Observable<option[]> {
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this.http.get(this.Url + 'api/get/locations', options).map((response: Response) => {
          return <option[]>response.json();
      })
          .catch(this.handleErrorObservable);
  }
  AllPreDegree(): Observable<option[]> {
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this.http.get(this.Url + 'api/get/qualification', options).map((response: Response) => {
          return <option[]>response.json();
      })
          .catch(this.handleErrorObservable);
  }
  private extractData(res: Response) {
      let body = res.json();
      return body || {};
  }
  private handleErrorObservable(error: Response | any) {
      console.error(error.message || error);
      return Observable.throw(error.message || error);
	  
	 
  }
  

}
