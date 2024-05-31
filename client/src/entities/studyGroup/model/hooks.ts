import { Api } from '@shared';
import {
    $errorOnJoiningGroup,
    $findStudyGroupError,
    $isSuccessJoiningGroup,
    $linkToJoin,
    $studyGroup,
    $studyGroups,
    JoiningGroupGate,
    LinkToJoinGate,
    StudyGroupGate,
    StudyGroupsGate,
} from './model';
import {
    WithStudyGroupId,
    findStudyGroupFx,
    findStudyGroupsFx,
    getLinkToJoinToStudyGroupFx,
    joinToStudyGroupFx,
} from './effects';
import { useGate, useUnit } from 'effector-react';
import { useEffect } from 'react';

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

export const useJoinToStudyGroup = (onSuccess = () => {}, onFail: (error: Error) => void = () => {}) => {
    useGate(JoiningGroupGate);

    const join = (request: WithStudyGroupId<Api.Services.JoinToStudyGroupRequest>) => joinToStudyGroupFx(request);
    const isSuccessJoiningGroup = useUnit($isSuccessJoiningGroup);
    const errorOnJoiningGroup = useUnit($errorOnJoiningGroup);

    useEffect(() => {
        if (isSuccessJoiningGroup) {
            onSuccess();
        }
        if (!!errorOnJoiningGroup) {
            onFail(errorOnJoiningGroup);
        }
    }, [isSuccessJoiningGroup, errorOnJoiningGroup, onSuccess, onFail]);

    return { join, errorOnJoiningGroup };
};
