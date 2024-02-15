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
  userInfo: any; 

  constructor(@Inject(ApiService) private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.apiService.checkAuthAndFetchUserInfo().subscribe(
      (userInfo: any) => { 
        this.userInfo = userInfo;
      },
      (error: any) => { 
        console.error('Failed to fetch user info:', error);
      }
    );
  }
    goToAddFeedbackForm(): void {
    this.router.navigate(['/feedback-create']);
  }
    goToFeedbackChart(): void { 
    this.router.navigate(['/feedback-chart']);
  }
}
