import { Component } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { UserService } from "src/app/services/user.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  public errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    });
  }

  onSubmit() {
    this.errorMessage = null;

    if (this.loginForm.invalid) {
      return;
    }

    const formData = this.loginForm.getRawValue();

    this.userService.getUserId(formData).subscribe(
      (userId: string) => {
        this.userService.login(formData).subscribe(
          (result) => {
            localStorage.setItem("auth_token", result.accessToken);
            sessionStorage.setItem('userId', userId);
            this.userService.authorizedSubject.next(true);
            this.router.navigateByUrl('/').then(() => {
              window.location.reload();
            });
          },
          (error) => {
            if (error.status === 401) {
              this.errorMessage = "Neispravan email ili lozinka";
            } else {
              this.errorMessage = "An unexpected error occurred during login.";
            }
          }
        );
      },
      (error) => {
        this.errorMessage = "Nepoznat korisnik, pokušajte ponovno.";
      }
    );
  }
}
