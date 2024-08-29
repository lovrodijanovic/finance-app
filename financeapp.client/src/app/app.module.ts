import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { FinancialFormService } from './services/financial-form.service';
import { RegisterComponent } from './pages/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './pages/login/login.component';
import { HeadersInterceptor } from './helpers/headers.interceptor';
import { SharedModule } from './shared/shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    HeaderComponent,
    FooterComponent,
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [FinancialFormService, {
    provide: HTTP_INTERCEPTORS,
    useClass: HeadersInterceptor,
    multi: true,
  },],
  bootstrap: [AppComponent]
})
export class AppModule { }
