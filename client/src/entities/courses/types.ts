export interface UserCours {
    id: string;
    name: string;
    description: string;
}

export interface EditableCours {
    id: string;
    name: string;
    shortName: string;
    description: string;
    isDraft: boolean;
    isDeleted: boolean;
}
