import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './services/api.authService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
title : string = 'FeedbackAnalysisAI'
  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.apiService.checkAuthAndFetchUserInfo().subscribe(
      userInfo => {
        if (userInfo) {
          // Если информация о пользователе успешно получена, перенаправляем пользователя на страницу user-panel
          this.router.navigate(['/user-panel']);
        } else {
          // Если информация о пользователе не получена, пользователь остается на текущей странице
        }
      },
      error => {
        console.error('Failed to check auth and fetch user info:', error);
      }
    );
  }
}
