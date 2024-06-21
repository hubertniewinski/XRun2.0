import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;

  constructor(public authService: AuthService,
    private fb: FormBuilder,
    private messageService: MessageService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      login: this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required)
    });
  }

  login() {
    if(this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.authService.login(this.formGroup.value.login, this.formGroup.value.password).subscribe(
      (data) => {
        this.authService.setUserData(data);
        this.router.navigate(['/']);
      },
      (error) => {
        this.messageService.add({severity:'error', summary:'Error', detail:error.error});
      }
    );
  }
}
