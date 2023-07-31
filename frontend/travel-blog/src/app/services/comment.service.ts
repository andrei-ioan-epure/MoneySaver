import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment,Comments } from '../blog/model/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private readonly endpoint:string='https://localhost:7207/api';

  constructor(private readonly httpClient: HttpClient) { }

  getCommentsFromArticle(id:number):Observable<Comments>{
    let finalEndpoint = `${this.endpoint}/Comment/getCommentsFromArticle/${id}`;
    return this.httpClient.get<Comments>(finalEndpoint);
  }

  postCommentInArticle(comment:Comment):Observable<Comment>{
    let finalEndpoint = `${this.endpoint}/Comment/add`;
    return this.httpClient.post<Comment>(finalEndpoint, comment);
  }

  deleteComment(id:number):Observable<Comment>{
    debugger
    let finalEndpoint = `${this.endpoint}/Comment/delete/${id}`;
    return this.httpClient.delete<Comment>(finalEndpoint); 
  }
}
