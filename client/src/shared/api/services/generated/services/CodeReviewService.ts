/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CodeReviewStatus } from '../models/CodeReviewStatus';
import type { CompleteCodeReviewRequest } from '../models/CompleteCodeReviewRequest';
import type { GetStudyGroupCodeReviewsResponse } from '../models/GetStudyGroupCodeReviewsResponse';
import type { GetUserSolutionCodeReviewResponse } from '../models/GetUserSolutionCodeReviewResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CodeReviewService {

    /**
     * @param studyGroupId 
     * @param statuses 
     * @returns GetStudyGroupCodeReviewsResponse Success
     * @throws ApiError
     */
    public static getStudyGroupCodeReviews(
studyGroupId: string,
statuses?: Array<CodeReviewStatus>,
): CancelablePromise<Array<GetStudyGroupCodeReviewsResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/study-groups/{studyGroupId}/code-reviews',
            path: {
                'studyGroupId': studyGroupId,
            },
            query: {
                'Statuses': statuses,
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
     * @param codeReviewId 
     * @returns GetUserSolutionCodeReviewResponse Success
     * @throws ApiError
     */
    public static getUserSolutionCodeReview(
codeReviewId: string,
): CancelablePromise<GetUserSolutionCodeReviewResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/code-reviews/{codeReviewId}',
            path: {
                'codeReviewId': codeReviewId,
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
     * @param codeReviewId 
     * @returns any Success
     * @throws ApiError
     */
    public static startCodeReview(
codeReviewId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/code-reviews/{codeReviewId}/start',
            path: {
                'codeReviewId': codeReviewId,
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
     * @param codeReviewId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static completeCodeReview(
codeReviewId: string,
requestBody: CompleteCodeReviewRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/code-reviews/{codeReviewId}/complete',
            path: {
                'codeReviewId': codeReviewId,
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
