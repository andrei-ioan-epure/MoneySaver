export type UserLogin = {
    email: string;
    password: string;
};

export interface User extends UserLogin{
    userName: string;
    fullName: string;
    isCreator: boolean;
};

export interface ServerUser extends User {
  id: number
};

export interface LoginResponse{
  token: string;
  id: number;
  role: string;
  fullName: string;
};

export type ServerUsers = ServerUser[];
export type Users = User[];