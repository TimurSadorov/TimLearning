import { Api, restoreFail } from '@shared';
import { restore } from 'effector';
import { createGate } from 'effector-react';
import { findStudyGroupFx, findStudyGroupsFx, getLinkToJoinToStudyGroupFx } from './effects';

export const StudyGroupsGate = createGate<Api.Services.FindStudyGroupsQueryParams>();
export const $studyGroups = restore<Api.Services.StudyGroupResponse[]>(findStudyGroupsFx, null).reset(
    StudyGroupsGate.close,
);

export const StudyGroupGate = createGate<string>();
export const $studyGroup = restore(findStudyGroupFx, null).reset(StudyGroupGate.close);
export const $findStudyGroupError = restoreFail(null, findStudyGroupFx).reset(StudyGroupGate.close);

export const LinkToJoinGate = createGate<string>();
export const $linkToJoin = restore(getLinkToJoinToStudyGroupFx, null).reset(LinkToJoinGate.close);
