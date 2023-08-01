import { Component, Input, OnInit } from '@angular/core';
import { Comment } from 'src/app/blog/model/comment';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {
  @Input() creatorId?: number;
  @Input() posted?: Date;
  @Input() message?: string;
  @Input() creatorName?: string;
  @Input() index?: number;
  @Input() id?: number;
  @Input() articleId?: number;
  @Input() numberOfLikes?: number;
  @Input() likedByUsers?: number[];
  commentInEditMode: boolean = false;
  likeClicked: boolean = false;

  public myComment: boolean = false;

  constructor(
    private commentService: CommentService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.verifyLike();
    console.log(this.likeClicked);
    if (
      this.authService.getId() === this.creatorId ||
      this.authService.getRole() === 'admin'
    )
      this.myComment = true;
    else this.myComment = false;
  }

  verifyLike() {
    if (this.likedByUsers?.includes(this.authService.getId() as number)) {
      this.likeClicked = true;
    } else {
      this.likeClicked = false;
    }
  }
  saveEdit() {
    this.commentInEditMode = false;
    const comment: Comment = {
      message: this.message as string,
      posted: this.posted!,
      creatorName: this.creatorName!,
      creatorId: this.creatorId!,
      articleId: this.articleId!,
      id: this.id!,
      numberOfLikes: this.numberOfLikes,
      likedByUsers: this.likedByUsers == null? []: this.likedByUsers,
    };
    this.commentService.putMessageComment(comment).subscribe();
  }

  onEdit() {
    this.commentInEditMode = true;
  }

  addLike(id?: number) {
    var userId = this.authService.getId();
    this.commentService
      .putLikeComment(userId as number, id as number)
      .subscribe();
  }

  removeLike() {
    var userId = this.authService.getId();
    // this.commentService
    //   .putLikeComment(userId as number, id as number)
    //   .subscribe();
  }
  
  onDelete(id?: number): void {
    this.commentService.deleteComment(id as number).subscribe();
  }
}
