import { Component } from '@angular/core';
import { FeedbackService } from '../services/api.feedback.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-feedback-create',
  templateUrl: './feedback-create.component.html',
  styleUrls: ['./feedback-create.component.css']
})
export class FeedbackCreateComponent {
  feedbackText: string = '';
  feedbackAdded: boolean = false; 
  errorText: string = '';

  constructor(private feedbackService: FeedbackService, private router: Router) {}

  onSubmit(): void {
    this.errorText = '';
    if (this.feedbackText.length >= 20 && this.feedbackText.length <= 2000) {
      this.feedbackService.addFeedback(this.feedbackText).subscribe(
        (response: any) => {
          console.log('Feedback ID:', response.FeedbackId);
        this.errorText = 'Feedback successfully added!';
        this.router.navigate(['/user-panel']);
        },
        (error: any) => {
          console.error('Failed to add feedback:', error);
          this.errorText = 'Failed to add feedback. Please try again.';
        }
      );
    } else {
      console.error('Invalid feedback text length');
      this.errorText = 'Failed to add feedback. Please try again.';
    }
  }
}
