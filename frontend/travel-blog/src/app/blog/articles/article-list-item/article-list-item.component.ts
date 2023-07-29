import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';
import { OfferService } from 'src/app/services/offer.service';
@Component({
  selector: 'app-article-list-item',
  templateUrl: './article-list-item.component.html',
  styleUrls: ['./article-list-item.component.scss'],
})
export class ArticleListItemComponent {
  @Input() id?: number;
  @Input() title?: string;
  @Input() content?: string;
  @Input() store?: string;
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

  public isLoggedIn: Observable<boolean>;

  @Output() articleChange: EventEmitter<string> = new EventEmitter<string>();
  constructor(
    private offerService: OfferService,
    private readonly httpService: HttpService,
    private readonly authService: AuthService
  ) {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  onClick(): void {
    //console.log(this.code);
    // this.offerService.offer=
    this.offerService.setParameter({
      title: this.title,
      content: this.content,
      city: this.city,
      expiration: this.expiration,
      code: this.code,
      category: this.category,
      author: this.author,
      posted: this.posted,
      store: this.store,
      creatorId: this.creatorId,
    });

    this.articleChange.emit(this.title);
    //this.articleChange.emit(this.content);
  }
  onClickFavorite(index: any, targetId: any): void {
    var favBtn = document.getElementById('heart' + index);
    console.log(favBtn);
    if (favBtn != null) {
      if (favBtn.innerHTML === 'favorite_border') {
        favBtn.innerHTML = 'favorite';
        const body = {
          userId: this.authService.getId(),
          targetId: targetId,
        };
        console.log(body);

        this.httpService.addArticleToFavorites(body).subscribe();
      } else {
        favBtn.innerHTML = 'favorite_border';
      }
    }
  }
  getImagePathByStore(store?: string): string {
    if (!store) {
      // If the store is not provided, use the placeholder image path or any default image path
      return '../../../../assets/images/placeholder.jpg';
    }
    const sanitizedStoreName = store.toLowerCase();
    return `../../../assets/images/${sanitizedStoreName}.jpg`;
  }

  onRemoveFromFavorite(targetId: any): void {
    const queryParams = {
      userId: this.authService.getId(),
      targetId: targetId,
    };

    this.httpService.deleteArticleFromFavorites(queryParams);
    window.location.reload();
  }
}
