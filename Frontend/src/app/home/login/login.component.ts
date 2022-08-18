import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = {} as FormGroup;
  formSubmitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.createLoginForm()
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group({
			userName: ['', Validators.required],
      passWord: ['', Validators.required]
		});
  }

  onLoginSubmit() {
    this.formSubmitted = true;
    if(this.loginForm.invalid) return;

		let data = JSON.stringify(this.loginForm.value);
		console.log('-----Login in JSON Format-----');
		console.log(data);
    // APIs

    this.resetTeamForm();
	}
	resetTeamForm() {
		this.loginForm.reset();
	}
}
