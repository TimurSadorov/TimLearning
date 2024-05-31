import { Api, restoreFail } from '@shared';
import { createStore, restore } from 'effector';
import { createGate } from 'effector-react';
import { findStudyGroupFx, findStudyGroupsFx, getLinkToJoinToStudyGroupFx, joinToStudyGroupFx } from './effects';
import { reset } from 'patronum';

export const StudyGroupsGate = createGate<Api.Services.FindStudyGroupsQueryParams>();
export const $studyGroups = restore<Api.Services.StudyGroupResponse[]>(findStudyGroupsFx, null).reset(
    StudyGroupsGate.close,
);

export const StudyGroupGate = createGate<string>();
export const $studyGroup = restore(findStudyGroupFx, null).reset(StudyGroupGate.close);
export const $findStudyGroupError = restoreFail(null, findStudyGroupFx).reset(StudyGroupGate.close);

export const LinkToJoinGate = createGate<string>();
export const $linkToJoin = restore(getLinkToJoinToStudyGroupFx, null).reset(LinkToJoinGate.close);

export const JoiningGroupGate = createGate();
export const $isSuccessJoiningGroup = createStore(false).on(joinToStudyGroupFx.done, () => true);
export const $errorOnJoiningGroup = restoreFail(null, joinToStudyGroupFx);
reset({
    clock: JoiningGroupGate.close,
    target: [$isSuccessJoiningGroup, $errorOnJoiningGroup],
});
