/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { LessonUserSolutionResponse } from './LessonUserSolutionResponse';

export type UserLessonResponse = {
    id: string;
    name: string;
    text: string;
    courseId: string;
    userSolution?: LessonUserSolutionResponse;
};
