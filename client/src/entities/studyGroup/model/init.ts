import { sample } from 'effector';
import { StudyGroupsGate } from './model';
import { createStudyGroupsFx, findStudyGroupsFx } from './effects';

sample({
    clock: [StudyGroupsGate.state, createStudyGroupsFx.done],
    source: StudyGroupsGate.state,
    filter: (r) => !!r,
    target: findStudyGroupsFx,
});
