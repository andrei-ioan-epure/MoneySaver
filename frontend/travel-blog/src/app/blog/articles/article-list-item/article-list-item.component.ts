import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-article-list-item',
  templateUrl: './article-list-item.component.html',
  styleUrls: ['./article-list-item.component.scss']
})
export class ArticleListItemComponent {
  @Input() title?: string;
  @Input() content?: string;
  @Input() publishDate?: Date;

  @Output() articleChange: EventEmitter<string> = new EventEmitter<string>();

  onClick(): void {
    this.articleChange.emit(this.title);
  }
}
