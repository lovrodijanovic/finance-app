import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { LandingComponent } from './pages/landing/landing.component';
import { CreatePlanComponent } from './pages/create-plan/create-plan.component';
import { PlanComponent } from './pages/plan/plan.component';
import { HistoryComponent } from './pages/history/history.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'create-plan',
    component: CreatePlanComponent
  },
  {
    path: 'plan',
    component: PlanComponent
  },
  {
    path: 'history',
    component: HistoryComponent
  },
  {
    path: '',
    component: LandingComponent,
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'top'
  }),
  HistoryComponent
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
