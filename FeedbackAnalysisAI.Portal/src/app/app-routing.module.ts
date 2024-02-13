import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { HeaderComponent } from './header/header.component';
import { FeedbackChartComponent } from './feedback-chart/feedback-chart.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { FeedbackCreateComponent } from './feedback-create/feedback-create.component';

const routes: Routes = [
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'user-panel', component: UserPanelComponent },
  { path: 'feedback-create', component: FeedbackCreateComponent },
  { path: 'feedback-chart', component: FeedbackChartComponent },
  { path: '**', component: HeaderComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
