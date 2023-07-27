import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { OfferService } from 'src/app/services/offer.service';
@Component({
  selector: 'app-article-list-item',
  templateUrl: './article-list-item.component.html',
  styleUrls: ['./article-list-item.component.scss'],
})
export class ArticleListItemComponent {
  @Input() title?: string;
  @Input() content?: string;
  @Input() posted?: Date;
  @Input() city?: string;
  @Input() code?: string;
  @Input() category?: string;
  @Input() expiration?: Date;
  @Input() author?: string;
  @Input() showDeleteBtn?: boolean = false;
  @Input() hideFavoriteBtn?: boolean = false;
  @Input() index?: number;
  @Input() articleId?: number;
  @Input() creatorId?: number;
  @Output() articleChange: EventEmitter<string> = new EventEmitter<string>();

  constructor(
    private offerService: OfferService,
    private readonly httpService: HttpService
  ) {}

  onClick(): void {
    this.offerService.setParameter({
      title: this.title,
      posted: this.posted,
      city: this.city,
      expiration: this.expiration,
      category: this.category,
      code: this.code,
      author: this.author,
    });

    this.articleChange.emit(this.title);
  }
  onClickFavorite(index: any, creatorId: any, articleId: any): void {
    var favBtn = document.getElementById('heart' + index);
    console.log(favBtn);
    if (favBtn != null) {
      if (favBtn.innerHTML === 'favorite_border') {
        favBtn.innerHTML = 'favorite';
        const body = {
          userId: creatorId,
          articleId: articleId,
        };
        console.log(body);

        this.httpService.addArticleToFavorites(body).subscribe();
      } else {
        favBtn.innerHTML = 'favorite_border';
      }
    }
  }

  onRemoveFromFavorite(creatorId: any, articleId: any): void {
    const queryParams = {
      userId: creatorId,
      articleId: articleId,
    };

    this.httpService.deleteArticleFromFavorites(queryParams);
    window.location.reload();
  }
}
