import { sample } from 'effector';
import { CodeReviewsGate } from './model';
import { getStudyGroupCodeReviewsFx } from './effects';

sample({
    clock: [CodeReviewsGate.state],
    filter: (r) => !!r.studyGroupId,
    target: getStudyGroupCodeReviewsFx,
});
