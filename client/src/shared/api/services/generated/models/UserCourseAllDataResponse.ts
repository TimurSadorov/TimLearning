/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { UserProgressInModuleResponse } from './UserProgressInModuleResponse';

export type UserCourseAllDataResponse = {
    shortName: string;
    completionPercentage: number;
    modules: Array<UserProgressInModuleResponse>;
};
