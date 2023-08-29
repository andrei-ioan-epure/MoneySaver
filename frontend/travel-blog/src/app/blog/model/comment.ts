export type Comment = {
  message: string;
  posted: Date;
  creatorName: string;
  creatorId: number;
  articleId: number;
  id?: number;
  numberOfLikes?: number;
  likedByUsers?: number[];
};

export type Comments = Comment[];
