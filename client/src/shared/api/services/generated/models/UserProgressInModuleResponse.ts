/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { UserProgressInLessonResponse } from './UserProgressInLessonResponse';

export type UserProgressInModuleResponse = {
    id: string;
    name: string;
    completionPercentage: number;
    lessons: Array<UserProgressInLessonResponse>;
};
