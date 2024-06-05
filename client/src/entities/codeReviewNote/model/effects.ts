import { Api } from '@shared';
import { createEffect } from 'effector';

export type WithStudyGroupId<T> = T & { studyGroupId: string };

export type WithReviewId<T> = T & { reviewId: string };

export const getCodeReviewNotesWithComments = createEffect(async (reviewId: string) => {
    return await Api.Services.CodeReviewNoteService.getCodeReviewNotesWithComments(reviewId);
});

export const createCodeReviewNoteFx = createEffect(
    (request: WithReviewId<Api.Services.CreateCodeReviewNoteRequest>) => {
        return Api.Services.CodeReviewNoteService.createCodeReviewNote(request.reviewId, { ...request });
    },
);
