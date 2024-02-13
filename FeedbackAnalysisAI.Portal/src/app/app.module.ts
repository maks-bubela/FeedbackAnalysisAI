import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { FormsModule } from '@angular/forms';
import { ApiService } from './services/api.authService';
import { FeedbackService } from './services/api.feedback.service';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { FeedbackCreateComponent } from './feedback-create/feedback-create.component';
import { FeedbackChartComponent } from './feedback-chart/feedback-chart.component'; // импортируйте ApiService

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SignInComponent,
    SignUpComponent,
    UserPanelComponent,
    FeedbackCreateComponent,
    FeedbackChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ApiService, FeedbackService],
  bootstrap: [AppComponent]
})
export class AppModule { }
