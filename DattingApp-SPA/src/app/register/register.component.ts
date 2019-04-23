import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userRegister: any = {};
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  constructor(private authService: AuthService, private alertService: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl('', Validators.required)
    }, this.passwordMatchValidator);
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true};
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
