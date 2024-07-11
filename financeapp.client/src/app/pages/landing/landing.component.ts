import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared/shared.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css'
})
export class LandingComponent {

  constructor(public router: Router) {}
  navigateToPlan(){
    this.router.navigateByUrl('/create-plan');
  }
}
