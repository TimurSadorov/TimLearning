import { Api } from '@shared';
import {
    $findStudyGroupError,
    $linkToJoin,
    $studyGroup,
    $studyGroups,
    LinkToJoinGate,
    StudyGroupGate,
    StudyGroupsGate,
} from './model';
import { findStudyGroupFx, findStudyGroupsFx, getLinkToJoinToStudyGroupFx } from './effects';
import { useGate, useUnit } from 'effector-react';

export const useStudyGroups = (request: Api.Services.FindStudyGroupsQueryParams) => {
    useGate(StudyGroupsGate, request);
    const studyGroups = useUnit($studyGroups);
    const isLoading = useUnit(findStudyGroupsFx.pending);

    return { studyGroups, isLoading };
};

export const useStudyGroup = (groupId: string) => {
    useGate(StudyGroupGate, groupId);
    const studyGroup = useUnit($studyGroup);
    const findStudyGroupError = useUnit($findStudyGroupError);
    const isLoading = useUnit(findStudyGroupFx.pending);

    return { studyGroup, findStudyGroupError, isLoading };
};

export const useLinkToJoin = (groupId: string) => {
    useGate(LinkToJoinGate, groupId);
    const linkToJoin = useUnit($linkToJoin);
    const isLoading = useUnit(getLinkToJoinToStudyGroupFx.pending);

    return { linkToJoin, isLoading };
};
