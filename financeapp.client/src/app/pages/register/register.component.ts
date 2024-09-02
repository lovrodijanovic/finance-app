import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../shared/models/user.model';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registerForm: FormGroup;
  public errorMessage: string | null = null;


  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    this.errorMessage = null;

    if (this.registerForm.valid) {
      const formValues = this.registerForm.value;
      const user = new User(formValues);

      this.userService.register(user).subscribe(
        response => {
          alert("Račun uspješno kreiran!");
          this.router.navigate(['/login']);
        },
        (error) => {
          if (error && error.error && error.error[0].code === 'DuplicateUserName') {
            this.errorMessage = "Korisnik s unesenim emailom već postoji.";
          } else {
            this.errorMessage = "An unexpected error occurred.";
          }
        }
      );
    } else {
      this.errorMessage = "Forma nepravilna.";
    }
  }
}
