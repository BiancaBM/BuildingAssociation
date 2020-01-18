export interface User {
    uniqueId: number;
    name: string;
    isAdmin: boolean;
    email: string;
    password: string;
    membersCount: number;
    dateCreated: string;
}