import { Api } from '@shared';
import { useGate, useUnit } from 'effector-react';
import { WithStudyGroupId, getStudyGroupCodeReviewsFx } from './effects';
import { $codeReviews, CodeReviewsGate } from './model';

export const useCodeReviews = (request: WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>) => {
    useGate(CodeReviewsGate, request);
    const codeReviews = useUnit($codeReviews);
    const isLoading = useUnit(getStudyGroupCodeReviewsFx.pending);

    return { codeReviews, isLoading };
};
