import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userRegister: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService: AuthService, private alertService: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.userRegister).subscribe(() => {
      this.alertService.success('registration successful');
    }, error => {
      this.alertService.error(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
