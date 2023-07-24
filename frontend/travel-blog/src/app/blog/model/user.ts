export type User = {
    userName : string;
    fullNaMe : string;
    email : string;
    password : string;
};

export interface ServerUser extends User{
    id:number;
}

export type Users = User[];
export type ServerUsers = ServerUser[];