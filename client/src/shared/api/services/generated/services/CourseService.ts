/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateCourseRequest } from '../models/CreateCourseRequest';
import type { FindCoursesResponse } from '../models/FindCoursesResponse';
import type { GetUserCoursesResponse } from '../models/GetUserCoursesResponse';
import type { UpdateCourseRequest } from '../models/UpdateCourseRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CourseService {

    /**
     * @param courseId 
     * @returns GetUserCoursesResponse Success
     * @throws ApiError
     */
    public static getUserCourses(
courseId?: string,
): CancelablePromise<Array<GetUserCoursesResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/courses/for-user',
            query: {
                'courseId': courseId,
            },
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param id 
     * @param searchName 
     * @param isDraft 
     * @param isDeleted 
     * @returns FindCoursesResponse Success
     * @throws ApiError
     */
    public static findCourses(
id?: string,
searchName?: string,
isDraft?: boolean,
isDeleted?: boolean,
): CancelablePromise<Array<FindCoursesResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/courses/all-info',
            query: {
                'Id': id,
                'SearchName': searchName,
                'IsDraft': isDraft,
                'IsDeleted': isDeleted,
            },
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
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

}
