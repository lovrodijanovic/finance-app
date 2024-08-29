import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuard } from './helpers/auth.guard';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  public isLoggedIn = false;
  constructor(private router: Router, private authGuard: AuthGuard) {
    this.isLoggedIn = this.authGuard.isLoggedIn();
  }

  ngOnInit() {  
  }
  title = 'Finance Assistant';
}
