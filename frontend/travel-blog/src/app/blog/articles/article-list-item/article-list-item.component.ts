import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
@Component({
  selector: 'app-article-list-item',
  templateUrl: './article-list-item.component.html',
  styleUrls: ['./article-list-item.component.scss']
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
  @Input() showDeleteBtn?: boolean=false;
  @Input() hideFavoriteBtn?: boolean=false;
  
  @Output() articleChange: EventEmitter<string> = new EventEmitter<string>();

  constructor(private offerService :OfferService){}
  
  onClick(): void {
   // this.offerService.offer=
   this.offerService.setParameter({
      title:this.title,
      content:this.content,
      city:this.city,
      expiration:this.expiration,
      code:this.code,
      category:this.category,
      author:this.author,
      posted:this.posted,
    });
    
    this.articleChange.emit(this.title);
    //this.articleChange.emit(this.content);
  }
  onClickFavorite():void{
    
    var favBtn=document.getElementById("heart");
    console.log(favBtn?.innerHTML);
    if(favBtn!=null){
      if( favBtn.innerHTML==="favorite_border" )
      {
        favBtn.innerHTML="favorite";
      }
      else{
         favBtn.innerHTML="favorite_border";
      }
  }
  }
}
