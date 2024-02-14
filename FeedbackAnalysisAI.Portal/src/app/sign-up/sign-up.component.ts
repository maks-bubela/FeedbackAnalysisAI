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
  isAuthenticated: boolean = false;

  constructor(private apiService: ApiService, private router: Router) { }
  ngOnInit() {
    this.apiService.checkAuthAndFetchUserInfo().subscribe(
      user => {
        if (user) {
          this.isAuthenticated = true;
          // Перенаправить пользователя на другую страницу, если он уже аутентифицирован
          this.router.navigate(['/user-panel']); // Предположим, что '/dashboard' - это путь к панели управления пользователями
        } else {
          this.isAuthenticated = false;
        }
      },
      error => {
        console.error('Error checking authentication:', error);
        this.isAuthenticated = false;
      }
    );
  }
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
