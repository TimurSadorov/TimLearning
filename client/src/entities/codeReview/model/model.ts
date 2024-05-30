import { Api } from '@shared';
import { restore } from 'effector';
import { createGate } from 'effector-react';
import { WithStudyGroupId, getStudyGroupCodeReviewsFx } from './effects';

export const CodeReviewsGate = createGate<WithStudyGroupId<Api.Services.GetStudyGroupCodeReviewsParams>>();
export const $codeReviews = restore<Api.Services.GetStudyGroupCodeReviewsResponse[]>(
    getStudyGroupCodeReviewsFx,
    null,
).reset(CodeReviewsGate.close);
