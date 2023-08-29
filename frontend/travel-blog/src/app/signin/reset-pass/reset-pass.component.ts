import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset-pass',
  templateUrl: './reset-pass.component.html',
  styleUrls: ['./reset-pass.component.scss'],
})
export class ResetPassComponent {
  formGroup: FormGroup = new FormGroup({});
  email: FormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  isSent: boolean = false;
  timer: any;

  constructor(private router: Router) {}

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: this.email,
    });
  }

  submitForm_reset() {
    if (this.formGroup.valid) {
      console.log(this.formGroup.value);

      // Show "Sent" text on the button
      this.isSent = true;

      // Reset the button after 5 seconds
      this.timer = setTimeout(() => {
        this.isSent = false;
      }, 5000);
    }
  }
}
