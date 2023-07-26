import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { ResetPassComponent } from './signin/reset-pass/reset-pass.component';
import { ContactComponent } from './contact/contact.component';
import { ContactResponseComponent } from './contact/contact-response/contact-response.component';
import { SignUpComponent } from './sign-up/sign-up.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path:'signin',
    component:SigninComponent
  },
  {
    path:'reset-pass',
    component:ResetPassComponent
  },
  {
    path:'contact',
    component:ContactComponent
  },
  {
    path:'contact-response',
    component:ContactResponseComponent
  },
  {
    path: 'about',
    component: AboutComponent
  },
  {
    path: 'blog',
    loadChildren: () => import('./blog/blog.module').then(m => m.BlogModule)
  },
  {
    path:'sign-up',
    component: SignUpComponent
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
