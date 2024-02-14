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
