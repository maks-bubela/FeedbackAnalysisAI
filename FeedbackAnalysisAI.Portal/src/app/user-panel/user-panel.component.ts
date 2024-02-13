import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.authService';
import { Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {
  userInfo: any; // You can explicitly define the type of userInfo

  constructor(@Inject(ApiService) private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.apiService.checkAuthAndFetchUserInfo().subscribe(
      (userInfo: any) => { // Define the type of userInfo
        // Handle successful response - save user information
        this.userInfo = userInfo;
      },
      (error: any) => { // Define the type of error
        // Handle error fetching user information
        console.error('Failed to fetch user info:', error);
      }
    );
  }
    goToAddFeedbackForm(): void {
    this.router.navigate(['/feedback-create']);
  }
    goToFeedbackChart(): void { // Добавленный метод для навигации к feedback-chart
    this.router.navigate(['/feedback-chart']);
  }
}
