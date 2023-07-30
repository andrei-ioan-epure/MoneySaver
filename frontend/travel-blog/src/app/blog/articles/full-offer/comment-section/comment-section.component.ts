import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Comments } from 'src/app/blog/model/comment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment-section',
  templateUrl: './comment-section.component.html',
  styleUrls: ['./comment-section.component.scss'],
})
export class CommentSectionComponent implements OnInit {
  articleId!: number;
  comments?: Comments;

  constructor(
    private commentService: CommentService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.articleId = +(this.activatedRoute.snapshot.paramMap.get(
      'id'
    ) as string);
    this.commentService
      .getCommentsFromArticle(this.articleId)
      .subscribe((res) => (this.comments = res));
  }
}
