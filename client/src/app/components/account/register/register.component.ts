import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterPlayer } from '../../../models/register-player.model';
import { RegisterPlayerService } from '../../../services/register-player.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,
    MatFormFieldModule, MatInputModule, MatButtonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',

})
export class RegisterComponent {
  registerPlayerService = inject(RegisterPlayerService);
  fb = inject(FormBuilder);

  apiErrorMessage: string | undefined;

  registerFg = this.fb.group({
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    nameCtrl: ['', [Validators.required]],
    lastNameCtrl: ['', [Validators.required]],
    nationalCodeCtrl: ['',[Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
    heightCtrl: ['',[Validators.required]],
    ageCtrl: ['',[Validators.required]],
    passwordCtrl: ['',[Validators.required, Validators.minLength(7), Validators.maxLength(20)]]
  })

  get EmailCtrl(): FormControl {
    return this.registerFg.get('emailCtrl') as FormControl;
  }

  get NameCtrl(): FormControl {
    return this.registerFg.get('nameCtrl') as FormControl;
  }

  get LastNameCtrl(): FormControl {
    return this.registerFg.get('lastNameCtrl') as FormControl;
  }

  get NationalCodeCtrl(): FormControl {
    return this.registerFg.get('nationalCodeCtrl') as FormControl;
  }

  get HeightCtrl(): FormControl {
    return this.registerFg.get('heightCtrl') as FormControl;
  }

  get AgeCtrl(): FormControl {
    return this.registerFg.get('ageCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.registerFg.get('passwordCtrl') as FormControl;
  }

  register(): void {
    this.apiErrorMessage = undefined;

    let player: RegisterPlayer = {
      email: this.EmailCtrl.value,
      name: this.NameCtrl.value,
      lastName: this.LastNameCtrl.value,
      nationalCode: this.NationalCodeCtrl.value,
      height: this.HeightCtrl.value,
      age: this.AgeCtrl.value,
      password: this.PasswordCtrl.value
    }

    this.registerPlayerService.registerPlayer(player).subscribe({
      next: player => console.log(player),
      error: err => this.apiErrorMessage = err.error
    })
  }

  getState(): void {
    console.log(this.registerFg);
  }
}
