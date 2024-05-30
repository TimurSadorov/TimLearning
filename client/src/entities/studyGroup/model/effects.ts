import { Api, NotFoundError } from '@shared';
import { createEffect } from 'effector';

export const findStudyGroupsFx = createEffect(async (request: Api.Services.FindStudyGroupsQueryParams) => {
    return await Api.Services.StudyGroupService.findStudyGroups(request.ids, request.searchName, request.isActive);
});

export const createStudyGroupsFx = createEffect(async (request: Api.Services.CreateStudyGroupRequest) => {
    return await Api.Services.StudyGroupService.createStudyGroup(request);
});

export const findStudyGroupFx = createEffect(async (groupId: string) => {
    const groups = await Api.Services.StudyGroupService.findStudyGroups([groupId]);
    if (groups.length === 0) {
        throw new NotFoundError();
    }

    return groups[0];
});

export type WithStudyGroupId<T> = T & { id: string };

export const updateStudyGroupFx = createEffect(
    async (request: WithStudyGroupId<Api.Services.UpdateStudyGroupRequest>) => {
        return await Api.Services.StudyGroupService.updateStudyGroup(request.id, { ...request });
    },
);

export const getLinkToJoinToStudyGroupFx = createEffect(async (id: string) => {
    return await Api.Services.StudyGroupService.getLinkToJoinToStudyGroup(id);
});
