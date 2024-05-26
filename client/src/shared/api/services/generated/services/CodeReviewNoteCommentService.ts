/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateCodeReviewNoteCommentRequest } from '../models/CreateCodeReviewNoteCommentRequest';
import type { UpdateCodeReviewNoteCommentRequest } from '../models/UpdateCodeReviewNoteCommentRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CodeReviewNoteCommentService {

    /**
     * @param noteId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static createCodeReviewNoteComment(
noteId: string,
requestBody: CreateCodeReviewNoteCommentRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/code-reviews/notes/{noteId}/comments',
            path: {
                'noteId': noteId,
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
     * @param commentId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static updateCodeReviewNoteComment(
commentId: string,
requestBody: UpdateCodeReviewNoteCommentRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/code-reviews/notes/comments/{commentId}',
            path: {
                'commentId': commentId,
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
     * @param commentId 
     * @returns any Success
     * @throws ApiError
     */
    public static deleteCodeReviewNoteComment(
commentId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/code-reviews/notes/comments/{commentId}',
            path: {
                'commentId': commentId,
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
