import { Component } from '@angular/core';
import { UserRegistrationModel } from '../models/UserRegistrationModel';
import { ApiService } from '../services/api.authService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  userRegistrationModel: UserRegistrationModel = new UserRegistrationModel();
  errorText: string = '';

  constructor(private apiService: ApiService, private router: Router) { }

  onSubmit() {
    this.errorText = ''; 

    this.apiService.register(this.userRegistrationModel).subscribe(
      response => {
        console.log('Registration successful:', response);
        this.errorText = 'Registration successful! Please, login for have access to your user panel';
        this.router.navigate(['/sign-in']);
      },
      error => {
        console.error('Registration error:', error);
        this.errorText = 'Registration failed!';
      }
    );
  }
}
