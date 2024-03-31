/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateLessonRequest } from '../models/CreateLessonRequest';
import type { LessonSystemDataResponse } from '../models/LessonSystemDataResponse';
import type { MoveLessonRequest } from '../models/MoveLessonRequest';
import type { UpdateLessonRequest } from '../models/UpdateLessonRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class LessonService {

    /**
     * @param moduleId 
     * @returns LessonSystemDataResponse Success
     * @throws ApiError
     */
    public static getOrderedLessons(
moduleId: string,
): CancelablePromise<Array<LessonSystemDataResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/modules/{moduleId}/lessons/ordered',
            path: {
                'moduleId': moduleId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @returns LessonSystemDataResponse Success
     * @throws ApiError
     */
    public static getDeletedLessons(
moduleId: string,
): CancelablePromise<Array<LessonSystemDataResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/modules/{moduleId}/lessons/deleted',
            path: {
                'moduleId': moduleId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static createLesson(
moduleId: string,
requestBody: CreateLessonRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/modules/{moduleId}/lessons',
            path: {
                'moduleId': moduleId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param lessonId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static updateLesson(
lessonId: string,
requestBody: UpdateLessonRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/lessons/{lessonId}',
            path: {
                'lessonId': lessonId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param lessonId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static moveLesson(
lessonId: string,
requestBody: MoveLessonRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/lessons/{lessonId}/move',
            path: {
                'lessonId': lessonId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param lessonId 
     * @returns any Success
     * @throws ApiError
     */
    public static deleteLesson(
lessonId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/lessons/{lessonId}/delete',
            path: {
                'lessonId': lessonId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param lessonId 
     * @returns any Success
     * @throws ApiError
     */
    public static restoreLesson(
lessonId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/lessons/{lessonId}/restore',
            path: {
                'lessonId': lessonId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

}
