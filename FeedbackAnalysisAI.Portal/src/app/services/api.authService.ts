import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserRegistrationModel } from '../models/UserRegistrationModel';
import { UserLoginModel } from '../models/UserLoginModel';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:44369/api/authentication'; // Замените на URL вашего .NET API
  private apiUrlUser = 'https://localhost:44369/api/user'; // Замените на URL вашего .NET API
  constructor(private http: HttpClient) { }

  // Метод для проверки токена и получения информации о пользователе
  checkAuthAndFetchUserInfo(): Observable<any> {
    const token = localStorage.getItem('accessToken');

    if (!token) {
      // Если токен отсутствует, возвращаем пустое значение
      return of(null);
    }

    // Если токен присутствует, отправляем запрос на получение информации о пользователе с заголовком Authorization
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<any>(`${this.apiUrlUser}/info`, { headers: headers }).pipe(
      map(response => {
        // Обработка успешного ответа
        return response;
      }),
      catchError(error => {
        // Обработка ошибки (например, если токен недействителен или произошла ошибка на сервере)
        console.error('Failed to fetch user info:', error);
        return of(null);
      })
    );
  }


  login(userLoginModel: UserLoginModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/token`, userLoginModel)
      .pipe(
        catchError(this.handleError),
        tap(response => {
          if (response && response.access_token) {
            localStorage.setItem('accessToken', response.access_token);
          }
        })
      );
  }

  register(userRegistrationModel: UserRegistrationModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/registration`, userRegistrationModel)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
