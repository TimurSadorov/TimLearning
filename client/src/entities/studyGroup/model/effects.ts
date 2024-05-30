import { Api } from '@shared';
import { createEffect } from 'effector';

export const findStudyGroupsFx = createEffect(async (request: Api.Services.FindStudyGroupsQueryParams) => {
    return await Api.Services.StudyGroupService.findStudyGroups(request.ids, request.searchName, request.isActive);
});

export const createStudyGroupsFx = createEffect(async (request: Api.Services.CreateStudyGroupRequest) => {
    return await Api.Services.StudyGroupService.createStudyGroup(request);
});
