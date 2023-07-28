export type User = {
    userName: string;
    fullName: string;
    email: string;
    password: string;
    isCreator: boolean;
};

export interface ServerUser extends User {
  id: number
};

export type ServerUsers = ServerUser[];
export type Users = User[];
