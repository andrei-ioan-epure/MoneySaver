export type Comment = {
    message : string;
    posted : Date;
    creatorName : string;
    creatorID: number;
    articleID: number;
    id?:number;
};

export type Comments = Comment[];