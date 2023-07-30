export type Comment = {
    message : string;
    posted : Date;
    creatorName : string;
    creatorID: number;
    articleID: number;
};

export interface ServerComment {
    id:number;
};

export type Comments = Comment[];
export type ServerComments = ServerComment[];