import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { ArticleComponent } from './articles/articles.component';
import { ArticleListItemComponent } from './articles/article-list-item/article-list-item.component';
import { FilterComponent } from './filter/filter.component';
import {MatIconModule} from '@angular/material/icon';
import { FullOfferComponent } from './articles/full-offer/full-offer.component';
import { FormsModule, ReactiveFormsModule,FormGroup } from '@angular/forms';
import { EditOfferComponent } from './articles/full-offer/edit-offer/edit-offer.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';


@NgModule({
  declarations: [
    ArticleComponent,
    ArticleListItemComponent,
    FilterComponent,
    FullOfferComponent,
    EditOfferComponent,
  ],
  imports: [
    CommonModule,
    BlogRoutingModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatDatepickerModule
  ],
    exports: [
    ArticleComponent,
    ArticleListItemComponent,
  ],
})
export class BlogModule { }
