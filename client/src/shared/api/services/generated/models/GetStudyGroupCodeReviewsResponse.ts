/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CodeReviewStatus } from './CodeReviewStatus';

export type GetStudyGroupCodeReviewsResponse = {
    id: string;
    status: CodeReviewStatus;
    completed?: string | null;
    userEmail: string;
    moduleName: string;
    lessonName: string;
};
