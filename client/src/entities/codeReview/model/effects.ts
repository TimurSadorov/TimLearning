import { Api } from '@shared';
import { createEffect } from 'effector';

export type WithStudyGroupId<T> = T & { studyGroupId: string };

export type WithReviewId<T> = T & { reviewId: string };

export const getStudyGroupCodeReviewsFx = createEffect(
    async (request: WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>) => {
        return await Api.Services.CodeReviewService.getStudyGroupCodeReviews(request.studyGroupId, request.statuses);
    },
);

export const getUserSolutionCodeReviewFx = createEffect(async (reviewId: string) => {
    return await Api.Services.CodeReviewService.getUserSolutionCodeReview(reviewId);
});

export const startCodeReviewFx = createEffect(async (reviewId: string) => {
    return await Api.Services.CodeReviewService.startCodeReview(reviewId);
});

export const completeCodeReviewFx = createEffect(
    async (request: WithReviewId<Api.Services.CompleteCodeReviewRequest>) => {
        return await Api.Services.CodeReviewService.completeCodeReview(request.reviewId, request);
    },
);
