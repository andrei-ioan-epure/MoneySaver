export type Article = {
  id?:number
  title: string;
  posted: Date;
  city:string;
  expiration: Date;
  category:string;
  code:string
  store:string
  author:string;
  content: string;
  creatorId:number ;
};

export interface ServerArticle extends Article {
  id: number
};

export type ServerArticles = ServerArticle[];
export type Articles = Article[];
