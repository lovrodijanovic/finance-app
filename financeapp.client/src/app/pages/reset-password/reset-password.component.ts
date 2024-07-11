import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared/shared.module';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  public isEmailSent: boolean = false;

  constructor() { }

  sendEmail() {
    this.isEmailSent = true;
  }
}
