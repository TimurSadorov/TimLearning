/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateCourseRequest } from '../models/CreateCourseRequest';
import type { FindCoursesRequest } from '../models/FindCoursesRequest';
import type { FindCoursesResponse } from '../models/FindCoursesResponse';
import type { GetAllUserCoursesResponse } from '../models/GetAllUserCoursesResponse';
import type { GetUserCourseResponse } from '../models/GetUserCourseResponse';
import type { UpdateCourseRequest } from '../models/UpdateCourseRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CourseService {

    /**
     * @param courseId 
     * @returns GetUserCourseResponse Success
     * @throws ApiError
     */
    public static getUserCourse(
courseId: string,
): CancelablePromise<GetUserCourseResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/courses/{courseId}',
            path: {
                'courseId': courseId,
            },
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param courseId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static updateCourse(
courseId: string,
requestBody: UpdateCourseRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/courses/{courseId}',
            path: {
                'courseId': courseId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @returns GetAllUserCoursesResponse Success
     * @throws ApiError
     */
    public static getAllUserCourses(): CancelablePromise<Array<GetAllUserCoursesResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/courses/all',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns FindCoursesResponse Success
     * @throws ApiError
     */
    public static findCourses(
requestBody: FindCoursesRequest,
): CancelablePromise<Array<FindCoursesResponse>> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/courses/find',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static createCourse(
requestBody: CreateCourseRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/courses',
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
