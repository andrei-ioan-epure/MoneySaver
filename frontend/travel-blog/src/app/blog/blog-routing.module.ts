import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticleComponent } from './articles/articles.component';

const routes: Routes = [
  {
     path: 'article-list',
    component: ArticleComponent
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
