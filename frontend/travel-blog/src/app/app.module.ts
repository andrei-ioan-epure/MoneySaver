import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { HttpClientModule } from  '@angular/common/http';
import { HeaderComponent } from './common-components/header/header.component';
import { FooterComponent } from './common-components/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SigninComponent } from './signin/signin.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { ResetPassComponent } from './signin/reset-pass/reset-pass.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    SigninComponent,
    ResetPassComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
      ReactiveFormsModule,
      FormsModule,
      BrowserAnimationsModule,
      MatButtonModule,
      MatInputModule,
      MatCheckboxModule,
      MatFormFieldModule,
      MatIconModule
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
