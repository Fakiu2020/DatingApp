import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseApiUrl = environment.apiUrl + 'auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
currentUser: User;
photoUrl = new BehaviorSubject<string>('../../assets/user.png');
currentPhotoUrl = this.photoUrl.asObservable();

constructor(private http: HttpClient) { }



changeMemberPhoto(photoUrl: string) {
  this.photoUrl.next(photoUrl);
}


  login(userLogin: any) {
    return this.http.post(this.baseApiUrl + 'login', userLogin)
                    .pipe(
                        map((response: any) => {
                          const user = response;
                          if (user) {
                            localStorage.setItem('token', user.token);
                            this.decodedToken = this.jwtHelper.decodeToken(user.token);
                          }
                        })
                    );
  }

  register(model: any) {
    return this.http.post(this.baseApiUrl + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  initToken() {
    const token = localStorage.getItem('token');
    if (token) {
      this.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
