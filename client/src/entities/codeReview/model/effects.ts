import { Api } from '@shared';
import { createEffect } from 'effector';

export type WithStudyGroupId<T> = T & { studyGroupId: string };

export const getStudyGroupCodeReviewsFx = createEffect(
    async (request: WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>) => {
        return await Api.Services.CodeReviewService.getStudyGroupCodeReviews(request.studyGroupId, request.statuses);
    },
);
