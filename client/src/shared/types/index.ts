export interface User {
    id: string;
    email: string;
    roles: UserRole[];
}

export type UserRole = 'User' | 'Mentor' | 'ContentCreator' | 'Admin';
