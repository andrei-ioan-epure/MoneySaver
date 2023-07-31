import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { Article } from '../../model/article';
import { CommentService } from 'src/app/services/comment.service';
import { Comment } from 'src/app/blog/model/comment';
import { AuthService } from 'src/app/services/auth.service';

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

  constructor(
    private activatedRoute: ActivatedRoute,
    private httpService: HttpService,
    private router: Router,
    private commentService: CommentService,
    private authService: AuthService
  ) {}


  ngOnInit(): void {
      this.id = +(this.activatedRoute.snapshot.paramMap.get("id") as string);
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

  addComment():void{
    let input = document.getElementById('message') as HTMLInputElement;
    let comment : Comment={
      message : input.value,
      posted : new Date(),
      creatorName : this.authService.getFullName() as string,
      creatorID: this.authService.getId() as number,
      articleID: this.id
    };
    this.commentService.postCommentInArticle(comment).subscribe();
    window.location.reload();
  }
}
