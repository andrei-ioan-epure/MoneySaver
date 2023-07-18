export type Article = {
  title: string;
  content: string;
  publishDate: Date;
  // expirationDate: Date;
  // author:string;
  // city:string;
  // category:string;
};

export interface ServerArticle extends Article {
  id: number
};

export type ServerArticles = ServerArticle[];
export type Articles = Article[];
