import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private apiUrl = 'https://localhost:44369/api/feedback'; // Замените на URL вашего API

  constructor(private http: HttpClient) {}

  getFeedbacksListInfo() {
    const token = localStorage.getItem('accessToken');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
    return this.http.get<any>(`${this.apiUrl}/list/info`, httpOptions);
  }

  addFeedback(feedbackText: string): Observable<any> {
    // Получаем токен из localStorage
    const token = localStorage.getItem('accessToken');
    
    // Подготавливаем заголовки запроса с токеном
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };

    // Отправляем POST запрос с заголовками и телом запроса
    return this.http.post<any>(`${this.apiUrl}/add`, { Text: feedbackText }, httpOptions);
  }
}
