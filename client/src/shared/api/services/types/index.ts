import { Api } from '@shared';

export type FindCoursesQueryParams = { id?: string; searchName?: string; isDraft?: boolean; isDeleted?: boolean };

export type FindOrderedModulesQueryParams = { isDeleted: boolean; isDraft?: boolean };

export type FindStudyGroupsQueryParams = {
    ids?: string[];
    searchName?: string;
    isActive?: boolean;
};

export type GetStudyGroupCodeReviewsParams = {
    statuses?: Api.Services.CodeReviewStatus[];
};
