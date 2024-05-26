/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CodeReviewNoteCommentResponse } from './CodeReviewNoteCommentResponse';
import type { CodeReviewNotePositionResponse } from './CodeReviewNotePositionResponse';

export type CodeReviewNoteWithCommentResponse = {
    id: string;
    startPosition: CodeReviewNotePositionResponse;
    endPosition: CodeReviewNotePositionResponse;
    comments: Array<CodeReviewNoteCommentResponse>;
};
