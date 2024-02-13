// feedback-chart.component.ts
import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { FeedbackService } from '../services/api.feedback.service';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-feedback-chart',
  templateUrl: './feedback-chart.component.html',
  styleUrls: ['./feedback-chart.component.css']
})
export class FeedbackChartComponent implements OnInit, AfterViewInit {
  feedbackData: any[] = [];
  @ViewChild('feedbackChart') chartRef!: ElementRef;
  chartRendered: boolean = false;

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.feedbackService.getFeedbacksListInfo().subscribe(
      (data: any) => {
        console.log('Received data:', data); // Log data to console
        this.feedbackData = data.feedbacksListInfo;
        if (this.chartRendered && this.feedbackData.length > 0) {
          this.renderChart();
        }
      },
      (error: any) => {
        console.error('Failed to fetch feedbacks list info:', error);
      }
    );
  }

  ngAfterViewInit(): void {
    this.chartRendered = true;
    if (this.feedbackData.length > 0) {
      this.renderChart();
    }
  }

  renderChart(): void {
    const ctx = this.chartRef.nativeElement.getContext('2d');
    const labels = ['Positive', 'Negative', 'Neutral', 'Mixed']; // Labels for each possible option
    const data = [0, 0, 0, 0]; // Initial values for the count of feedbacks for each SentimentName

    this.feedbackData.forEach(item => {
      switch (item.sentimentName) {
        case 'Positive':
          data[0] += 1;
          break;
        case 'Negative':
          data[1] += 1;
          break;
        case 'Neutral':
          data[2] += 1;
          break;
        case 'Mixed':
          data[3] += 1;
          break;
      }
    });

    // Colors for each SentimentName
    const backgroundColors = [
      'rgba(75, 192, 192, 0.2)', // Positive
      'rgba(255, 99, 132, 0.2)',  // Negative
      'rgba(255, 206, 86, 0.2)',  // Neutral
      'rgba(54, 162, 235, 0.2)'   // Mixed
    ];

    const borderColors = [
      'rgba(75, 192, 192, 1)', // Positive
      'rgba(255, 99, 132, 1)',  // Negative
      'rgba(255, 206, 86, 1)',  // Neutral
      'rgba(54, 162, 235, 1)'   // Mixed
    ];

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Feedbacks',
          data: data,
          backgroundColor: backgroundColors,
          borderColor: borderColors,
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
}
