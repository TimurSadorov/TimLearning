import { Api } from '@shared';
import { useGate, useUnit } from 'effector-react';
import { WithStudyGroupId, getStudyGroupCodeReviewsFx, getUserSolutionCodeReviewFx } from './effects';
import { $codeReviews, $codeReviewsWithUserSolution, CodeReviewsGate, CodeReviewsWithUserSolutionGate } from './model';

export const useCodeReviews = (request: WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>) => {
    useGate(CodeReviewsGate, request);
    const codeReviews = useUnit($codeReviews);
    const isLoading = useUnit(getStudyGroupCodeReviewsFx.pending);

    return { codeReviews, isLoading };
};

export const useCodeReviewsWithUserSolution = (reviewId: string) => {
    useGate(CodeReviewsWithUserSolutionGate, reviewId);
    const codeReviewsWithUserSolution = useUnit($codeReviewsWithUserSolution);
    const isLoading = useUnit(getUserSolutionCodeReviewFx.pending);

    return { codeReviewsWithUserSolution, isLoading };
};
