import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class HeadersInterceptor implements HttpInterceptor {
  constructor(private router: Router) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('auth_token');
    if (token != null) {
      const clonedreq = request.clone({
        headers: request.headers.set('Authorization', 'Bearer ' + token),
      });
      return next.handle(clonedreq).pipe(
        tap(
          (succ) => { },
          (err) => {
            if (err.status === 401) {
              localStorage.removeItem('auth_token');
              this.router.navigateByUrl('/login');
            }
          }
        )
      );
    } else {
      return next.handle(request);
    }
  }
}
