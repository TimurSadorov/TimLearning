import { sample } from 'effector';
import { LinkToJoinGate, StudyGroupGate, StudyGroupsGate } from './model';
import {
    createStudyGroupsFx,
    findStudyGroupFx,
    findStudyGroupsFx,
    getLinkToJoinToStudyGroupFx,
    updateStudyGroupFx,
} from './effects';

sample({
    clock: [StudyGroupsGate.state, createStudyGroupsFx.done],
    source: StudyGroupsGate.state,
    filter: (r) => !!r,
    target: findStudyGroupsFx,
});

sample({
    clock: [StudyGroupGate.state, updateStudyGroupFx.done],
    source: StudyGroupGate.state,
    filter: (r) => !!r,
    target: findStudyGroupFx,
});

sample({
    clock: [LinkToJoinGate.state],
    filter: (r) => typeof r === 'string',
    target: getLinkToJoinToStudyGroupFx,
});
