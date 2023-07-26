import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleComponent } from './articles/articles.component';
import { FullOfferComponent } from './articles/full-offer/full-offer.component';

const routes: Routes = [
  {
     path: 'article-list',
    component: ArticleComponent
  },
  {
    path: 'favourites',
   component: ArticleComponent
 },
 {
  path:'article-list/full-offer/:id',
  component:FullOfferComponent
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
