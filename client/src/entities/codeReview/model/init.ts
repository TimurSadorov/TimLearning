import { sample } from 'effector';
import { CodeReviewsGate, CodeReviewsWithUserSolutionGate } from './model';
import {
    completeCodeReviewFx,
    getStudyGroupCodeReviewsFx,
    getUserSolutionCodeReviewFx,
    startCodeReviewFx,
} from './effects';

sample({
    clock: [CodeReviewsGate.state],
    filter: (r) => !!r.studyGroupId,
    target: getStudyGroupCodeReviewsFx,
});

sample({
    clock: [CodeReviewsWithUserSolutionGate.state, startCodeReviewFx.done, completeCodeReviewFx.done],
    source: CodeReviewsWithUserSolutionGate.state,
    filter: (r) => typeof r === 'string',
    target: getUserSolutionCodeReviewFx,
});
