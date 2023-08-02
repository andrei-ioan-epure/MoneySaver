import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleComponent } from './articles/articles.component';
import { FullOfferComponent } from './articles/full-offer/full-offer.component';
import { authGuard } from '../guards/auth.guard';
import { EditOfferComponent } from './articles/full-offer/edit-offer/edit-offer.component';
import { adminGuard } from '../guards/admin.guard';

const routes: Routes = [
  {
     path: 'article-list',
    component: ArticleComponent
  },
  {
    path: 'favourites',
   component: ArticleComponent,
    canActivate: [authGuard]
 },
 {
  path:'article-list/full-offer/:id',
  component:FullOfferComponent
 },
 {
  path:'article-list/full-offer/:id/edit-offer',
  component:EditOfferComponent,
  canActivate:[adminGuard]
 },
  {
    path: '**',
    redirectTo: 'article-list'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
