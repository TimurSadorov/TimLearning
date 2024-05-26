/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CodeReviewNoteWithCommentResponse } from '../models/CodeReviewNoteWithCommentResponse';
import type { CreateCodeReviewNoteRequest } from '../models/CreateCodeReviewNoteRequest';
import type { CreateCodeReviewNoteResponse } from '../models/CreateCodeReviewNoteResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CodeReviewNoteService {

    /**
     * @param codeReviewId 
     * @returns CodeReviewNoteWithCommentResponse Success
     * @throws ApiError
     */
    public static getCodeReviewNotesWithComments(
codeReviewId: string,
): CancelablePromise<Array<CodeReviewNoteWithCommentResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/code-reviews/{codeReviewId}/notes-with-comments',
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
     * @returns CreateCodeReviewNoteResponse Success
     * @throws ApiError
     */
    public static createCodeReviewNote(
codeReviewId: string,
requestBody: CreateCodeReviewNoteRequest,
): CancelablePromise<CreateCodeReviewNoteResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/code-reviews/{codeReviewId}/notes',
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

    /**
     * @param noteId 
     * @returns any Success
     * @throws ApiError
     */
    public static deleteCodeReviewNote(
noteId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/code-reviews/notes/{noteId}',
            path: {
                'noteId': noteId,
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
