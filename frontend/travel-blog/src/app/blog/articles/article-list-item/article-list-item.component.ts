import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';
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
  @Input() isFavorite?: boolean;

  public isLoggedIn: Observable<boolean>;

  @Output() articleChange: EventEmitter<string> = new EventEmitter<string>();
  constructor(
    private readonly httpService: HttpService,
    private readonly authService: AuthService
  ) {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  onClickFavorite(index: any, targetId: any): void {
    var favBtn = document.getElementById('heart' + index);
    if (favBtn != null) {
      const body = {
        userId: this.authService.getId(),
        targetId: targetId,
      };
      if (favBtn.innerHTML === 'favorite_border') {
        favBtn.innerHTML = 'favorite';

        console.log(body);

        this.httpService
          .addArticleToFavorites(
            this.authService.getId() as number,
            targetId as number
          )
          .subscribe();
      } else {
        favBtn.innerHTML = 'favorite_border';
        this.httpService
          .deleteArticleFromFavorites(
            this.authService.getId() as number,
            targetId as number
          )
          .subscribe();
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
    this.httpService
      .deleteArticleFromFavorites(
        this.authService.getId() as number,
        targetId as number
      )
      .subscribe();
  }
}
