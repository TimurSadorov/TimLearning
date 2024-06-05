import { Api } from '@shared';
import { restore } from 'effector';
import { createGate } from 'effector-react';
import { WithStudyGroupId, getStudyGroupCodeReviewsFx, getUserSolutionCodeReviewFx } from './effects';

export const CodeReviewsGate = createGate<WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>>();
export const $codeReviews = restore<Api.Services.GetStudyGroupCodeReviewsResponse[]>(
    getStudyGroupCodeReviewsFx,
    null,
).reset(CodeReviewsGate.close);

export const CodeReviewsWithUserSolutionGate = createGate<string>();
export const $codeReviewsWithUserSolution = restore<Api.Services.GetUserSolutionCodeReviewResponse>(
    getUserSolutionCodeReviewFx,
    null,
).reset(CodeReviewsWithUserSolutionGate.close);
