import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent {
  @Input() author?: number;
  @Input() posted?: Date;
  @Input() message?: string;
  @Input() creatorName?: string;


}
