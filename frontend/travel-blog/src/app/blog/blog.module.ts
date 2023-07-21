import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { ArticleComponent } from './articles/articles.component';
import { ArticleListItemComponent } from './articles/article-list-item/article-list-item.component';
import { FilterComponent } from './filter/filter.component';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    ArticleComponent,
    ArticleListItemComponent,
    FilterComponent
  ],
  imports: [
    CommonModule,
    BlogRoutingModule,MatIconModule
  ],
    exports: [
    ArticleComponent,
    ArticleListItemComponent,
  ],
})
export class BlogModule { }
