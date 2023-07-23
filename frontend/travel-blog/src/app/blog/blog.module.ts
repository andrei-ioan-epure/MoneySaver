import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { ArticleComponent } from './articles/articles.component';
import { ArticleListItemComponent } from './articles/article-list-item/article-list-item.component';
import { FilterComponent } from './filter/filter.component';
import {MatIconModule} from '@angular/material/icon';
import { FullOfferComponent } from './articles/full-offer/full-offer.component';

@NgModule({
  declarations: [
    ArticleComponent,
    ArticleListItemComponent,
    FilterComponent,
    FullOfferComponent,
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
