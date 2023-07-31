import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {
  @Input() creatorId?: number;
  @Input() posted?: Date;
  @Input() message?: string;
  @Input() creatorName?: string;
  @Input() index?:number;
  @Input() id?:number;
  
  public myComment:boolean = false;

  constructor(
    private commentService:CommentService,
    private authService:AuthService
  ){}

  ngOnInit(): void {
    if(this.authService.getId() === this.creatorId || this.authService.getRole() === "admin")
      this.myComment=true;
    else
      this.myComment=false;
      
  }

  onDelete(id?:number):void {
    this.commentService.deleteComment(id as number).subscribe();
  }
}
