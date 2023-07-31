import { Component, Input } from '@angular/core';
import { CommentService } from 'src/app/services/comment.service';

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
  @Input() index?:number;
  @Input() id?:number;

  constructor(
    private commentService:CommentService
  ){}

  onDelete(id?:number):void {
    this.commentService.deleteComment(id as number).subscribe();
  }
}
