/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CodeReviewLessonResponse } from './CodeReviewLessonResponse';
import type { CodeReviewStatus } from './CodeReviewStatus';
import type { UserSolutionResponse } from './UserSolutionResponse';

export type GetUserSolutionCodeReviewResponse = {
    status: CodeReviewStatus;
    userEmail: string;
    lesson: CodeReviewLessonResponse;
    solution: UserSolutionResponse;
    standardCode: string;
};
