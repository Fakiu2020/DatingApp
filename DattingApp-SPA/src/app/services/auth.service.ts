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
                            localStorage.setItem('user', JSON.stringify(user.user));
                            this.decodedToken = this.jwtHelper.decodeToken(user.token);
                            this.currentUser = user.user;
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
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.currentUser = user;
    }
  }
}
