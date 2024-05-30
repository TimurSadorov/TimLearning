import { Api } from '@shared';
import { restore } from 'effector';
import { createGate } from 'effector-react';
import { findStudyGroupsFx } from './effects';

export const StudyGroupsGate = createGate<Api.Services.FindStudyGroupsQueryParams>();
export const $studyGroups = restore<Api.Services.StudyGroupResponse[]>(findStudyGroupsFx, null).reset(
    StudyGroupsGate.close,
);
