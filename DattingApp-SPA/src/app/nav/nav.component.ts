import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  userLogin: any = {};
  constructor(private authService: AuthService, private alertService: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.userLogin).subscribe(next => {
      this.alertService.success('Logged in successfully');
    }, error => {
      this.alertService.error(error);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
  }

}
