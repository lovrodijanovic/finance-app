import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [BrowserModule, HttpClientModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() isLoggedIn: boolean = false;

  constructor(private userService: UserService) { }

  ngOnInit() { }

  logOut() {
    this.userService.logOut().subscribe();
    localStorage.removeItem('auth_token');
    sessionStorage.clear();
    window.location.href = '/';
  }
}
