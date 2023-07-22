import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { HttpClientModule } from  '@angular/common/http';
import { HeaderComponent } from './common-components/header/header.component';
import { FooterComponent } from './common-components/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { SigninComponent } from './signin/signin.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { ResetPassComponent } from './signin/reset-pass/reset-pass.component';
import { CardComponent } from './about/card/card.component';
import { ContactComponent } from './contact/contact.component';
import { ContactResponseComponent } from './contact/contact-response/contact-response.component';
import { OfertsComponent } from './oferts/oferts.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
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
    ResetPassComponent,
    CardComponent,
    ContactComponent,
    ContactResponseComponent,
    OfertsComponent,
    SignUpComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
