import { Component } from '@angular/core';
import { UserLoginModel } from '../models/UserLoginModel';
import { ApiService } from '../services/api.authService';
import { Router } from '@angular/router';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  userLoginModel: UserLoginModel = new UserLoginModel();
  errorText: string ='';
  constructor(private apiService: ApiService, private router: Router) { }

  onSubmit() {
    this.apiService.login(this.userLoginModel).subscribe(
      response => {
        console.log('Login successful:', response);
      this.router.navigate(['/user-panel']);
      },
      error => {
        console.error('Login error:', error);
        this.errorText = 'Incorrect login or password';
      }
    );
  }
}
