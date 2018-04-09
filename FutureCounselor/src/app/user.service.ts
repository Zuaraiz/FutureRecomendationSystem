import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
//<<<<<<< HEAD
import { signup } from './Models/signup'
//=======
//>>>>>>> c8fd6a85c0068b6ebdb11a1b832a23a20894ada2



@Injectable()
export class UserService {

  
   
    private Url = 'http://localhost:50175/';  // URL to web api
 
//<<<<<<< HEAD
  constructor(private http: Http) { }

  getAllHobbies(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/get/hobbies', data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }
  getAllInterests(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/get/interests',data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }
  getAllSkills(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/get/skills', data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }



  getUserHobbies(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/user/hobbies', data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }
  getUserInterests(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/user/interests', data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }
  getUserSkills(email: String): Observable<any[]> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/user/skills', data, options).map((response: Response) => {
      return <any[]>response.json();
    }).catch(this.handleErrorObservable);
  }

  getUserProfile(email: String): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email }
    return this.http.post(this.Url + 'api/user/profile', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }
  editUserProfile(email:string,userData: any): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email, firstName: userData.firstName, lastName: userData.lastName, percentage: userData.percentage, annualBudget: userData.annualBudget, location: userData.location, qualification:userData.qualification }
    return this.http.post(this.Url + 'api/edit/profile', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }


  addSkill(email: String, id: number, rating: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email , id :id, rating:rating}
    return this.http.post(this.Url + 'api/add/skills', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }
  addhobby(email: String, id: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email, id: id}
    return this.http.post(this.Url + 'api/add/hobbies', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }
  addInterest(email: String, id: number, rating: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { email: email, id: id, rating: rating }
    return this.http.post(this.Url + 'api/add/Interests', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }


  deleteInterest(uid: number, id: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { uid: uid,id :id }
    return this.http.post(this.Url + 'api/delete/interest', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }
  deleteHobby(uid: number, id: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { uid: uid, id: id }
    return this.http.post(this.Url + 'api/delete/Hobby', data, options).map(this.extractData)
      .catch(this.handleErrorObservable);
  }
  deleteSkill(uid: number, id: number): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let data = { uid: uid, id: id }
    return this.http.post(this.Url + 'api/delete/skills', data, options).map(this.extractData)
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
