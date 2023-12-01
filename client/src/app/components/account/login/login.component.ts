import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginPlayer } from '../../../models/login-player.model';
import { Player } from '../../../models/player.model';
import { RegisterPlayerService } from '../../../services/register-player.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,
    MatFormFieldModule, MatInputModule, MatButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  registerPlayerService = inject(RegisterPlayerService);
  fb = inject(FormBuilder);

  apiErrorMessage: string | undefined;

  loginFg: FormGroup = this.fb.group({
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(20)]]
  })

  get EmailCtrl(): FormControl {
    return this.loginFg.get('emailCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.loginFg.get('passwordCtrl') as FormControl;
  }

  login(): void {
    this.apiErrorMessage = undefined;

    let player: LoginPlayer = {
      email: this.EmailCtrl.value,
      password: this.PasswordCtrl.value
    }

    this.registerPlayerService.loginPlayer(player).subscribe({
      next: (player: Player | null) => {
        console.log(player);
      },
      error: err => this.apiErrorMessage = err.error
    })
  }

  getState(): void {
    console.log(this.loginFg);
  }
}
