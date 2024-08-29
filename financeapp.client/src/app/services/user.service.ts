import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { User } from '../shared/models/user.model';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) { }
  public authorizedSubject = new Subject<boolean>();

  public register(user: User) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<User>(
      this.baseUrl + `user/register`,
      user,
      httpOptions
    );
  }

  public login(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'account/login', data);
  }

  public logOut(): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'user/log-out', null);
  }

  public getUserId(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'user/get-user-id', data);
  }
}
