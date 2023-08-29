import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, catchError, map, tap } from 'rxjs';
import { Comment, Comments } from '../blog/model/comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  public commentsObserver: BehaviorSubject<Comments> =
    new BehaviorSubject<Comments>([]);

  private readonly endpoint: string = 'https://localhost:7207/api';

  constructor(private readonly httpClient: HttpClient) {}

  getCommentsFromArticle(id: number): Observable<Comments> {
    let finalEndpoint = `${this.endpoint}/Comment/getCommentsFromArticle/${id}`;
    return this.httpClient.get<Comments>(finalEndpoint).pipe(
      tap((res) => {
        this.commentsObserver.next(res);
      })
    );
  }

  putMessageComment(comment: Comment): Observable<Comment> {
    let finalEndpoint = `${this.endpoint}/Comment/update/${comment.id}`;

    return this.httpClient.put<Comment>(finalEndpoint, comment).pipe(
      tap((res) => {
        this.commentsObserver.next([
          ...this.commentsObserver
            .getValue()
            .filter((item) => item.id !== comment.id),
          res,
        ]);
      })
    );
  }

  putLikeComment(userId: number, targetId: number): Observable<Comment> {
    let finalEndpoint = `${this.endpoint}/Comment/addLike`;
    const body = {
      userId: userId,
      targetId: targetId,
    };
    return this.httpClient.put<Comment>(finalEndpoint, body).pipe(
      tap((res) => {
        this.commentsObserver.next([
          ...this.commentsObserver
            .getValue()
            .filter((item) => item.id !== targetId),
          res,
        ]);
      })
    );
  }

  removeLikeComment(userId: number, targetId: number): Observable<Comment> {
    let finalEndpoint = `${this.endpoint}/Comment/removeLike`;
    const body = {
      userId: userId,
      targetId: targetId,
    };
    return this.httpClient.put<Comment>(finalEndpoint, body).pipe(
      tap((res) => {
        console.log(this.commentsObserver.getValue());
        this.commentsObserver.next([
          ...this.commentsObserver
            .getValue()
            .filter((item) => item.id !== targetId),
          res,
        ]);
      }),
      catchError(() => {
        console.log('failed');
        return EMPTY;
      })
    );
  }

  postCommentInArticle(comment: Comment): Observable<Comment> {
    let finalEndpoint = `${this.endpoint}/Comment/add`;
    return this.httpClient.post<Comment>(finalEndpoint, comment).pipe(
      tap((response) => {
        this.commentsObserver.next([
          ...this.commentsObserver.getValue(),
          response,
        ]);
      }),
      catchError(() => {
        console.log('failed');
        return EMPTY;
      })
    );
  }

  deleteComment(id: number): Observable<Comment> {
    let finalEndpoint = `${this.endpoint}/Comment/delete/${id}`;
    const comments = this.commentsObserver.getValue();
    return this.httpClient.delete<Comment>(finalEndpoint).pipe(
      tap(() => {
        this.commentsObserver.next([
          ...comments.filter((item) => item.id !== id),
        ]);
      }),
      catchError(() => {
        console.log('failed');
        return EMPTY;
      })
    );
  }
}
