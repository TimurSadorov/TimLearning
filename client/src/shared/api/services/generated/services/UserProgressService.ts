/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { VisitLessonRequest } from '../models/VisitLessonRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UserProgressService {

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static visitLesson(
requestBody: VisitLessonRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user-progress/lesson-visiting',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

}
