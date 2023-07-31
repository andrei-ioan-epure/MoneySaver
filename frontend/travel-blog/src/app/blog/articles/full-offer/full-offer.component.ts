import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { Article } from '../../model/article';
import { CommentService } from 'src/app/services/comment.service';
import { Comment } from 'src/app/blog/model/comment';
import { AuthService } from 'src/app/services/auth.service';
import { EMPTY, catchError, tap } from 'rxjs';

@Component({
  selector: 'app-full-offer',
  templateUrl: './full-offer.component.html',
  styleUrls: ['./full-offer.component.scss'],
})
export class FullOfferComponent implements OnInit {
  id!: number;
  image?: string;
  title?: string;
  city?: string;
  expiration?: Date;
  code?: string;
  category?: string;
  author?: string;
  posted?: Date;
  content?: string;
  store?: string;
  creatorId?: number;
  copied = false;
  public comment: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private httpService: HttpService,
    private router: Router,
    private commentService: CommentService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.id = +(this.activatedRoute.snapshot.paramMap.get('id') as string);
    this.httpService.getArticle(this.id).subscribe((article) => {
      this.setArticle(article);
    });

    console.log(this.id);
  }

  private setArticle(offer: Article) {
    this.title = offer.title;
    this.content = offer.content;
    this.city = offer.city;
    this.expiration = offer.expiration;
    this.code = offer.code;
    this.category = offer.category;
    this.author = offer.author;
    this.posted = offer.posted;
    this.store = offer.store;
    this.creatorId = offer.creatorId;
  }

  toEditOffer() {
    this.router.navigate([
      'blog/article-list/full-offer/' + this.id + '/edit-offer',
    ]);
  }
  deleteOffer() {
    console.log('Delete article');
    console.log(this.id);
    this.httpService.deleteArticle(this.id);
    setTimeout(() => {
      this.router.navigate(['blog']);
    }, 100);
  }

  getImagePathByStore(store?: string): string {
    if (!store) {
      // If the store is not provided, use the placeholder image path or any default image path
      return '../../../../assets/images/placeholder.jpg';
    }
    const sanitizedStoreName = store.toLowerCase();
    return `../../../assets/images/${sanitizedStoreName}.jpg`;
  }

  addComment(): void {
    let comment: Comment = {
      message: this.comment,
      posted: new Date(),
      creatorName: this.authService.getFullName() as string,
      creatorId: this.authService.getId() as number,
      articleId: this.id,
    };
    this.commentService
      .postCommentInArticle(comment)
      .subscribe();
  }

  goToWebsite() {
    if (this.store) {
      // Mapare între numele magazinului și link-ul corespunzător
      switch (this.store.toLowerCase()) {
        case 'fashion-days':
          window.open('https://www.fashiondays.ro/', '_blank'); //deschide intr-o fereastra noua
          break;
        case 'cupio':
          window.open('https://www.cupio.ro/', '_blank');
          break;
        case 'glovo':
          window.open('https://glovoapp.com/', '_blank');
          break;
        case 'answear':
          window.open('https://answear.ro/', '_blank');
          break;
        case 'douglas':
          window.open('https://www.douglas.ro/', '_blank');
          break;
        case 'netflix':
          window.open('https://www.netflix.com/ro-en/', '_blank');
          break;
        case 'spotify':
          window.open('https://open.spotify.com/', '_blank');
          break;
        case 'stradivarius':
          window.open('https://www.stradivarius.com/ro/', '_blank');
          break;
        case 'tazz':
          window.open('https://tazz.ro/', '_blank');
          break;
        // Dacă nu se potrivește cu niciun magazin cunoscut, poți adăuga un comportament alternativ sau un mesaj de eroare.
        default:
          console.error('No website link available for this store.');
          break;
      }
    } else {
      console.error('No store name available for this offer.');
    }
  }
  copyCode() {
    if (this.code) {
      try {
        // Copiază textul în clipboard
        this.copyToClipboard(this.code);
        this.copied = true; // Marchează că textul a fost copiat cu succes

        // Resetează starea "copied" după 3 secunde
        setTimeout(() => {
          this.copied = false;
        }, 3000);
      } catch (err) {
        console.error('Copy failed: ', err);
      }
    }
  }

  copyToClipboard(text: string) {
    const textArea = document.createElement('textarea');
    textArea.value = text;
    textArea.style.position = 'fixed'; // Asigură-te că nu afectează aspectul paginii
    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
      document.execCommand('copy');
    } catch (err) {
      console.error('Copy failed: ', err);
    }

    document.body.removeChild(textArea);
  }
}
