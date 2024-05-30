import { Api } from '@shared';
import { $studyGroups, StudyGroupsGate } from './model';
import { findStudyGroupsFx } from './effects';
import { useGate, useUnit } from 'effector-react';

export const useStudyGroups = (request: Api.Services.FindStudyGroupsQueryParams) => {
    useGate(StudyGroupsGate, request);
    const studyGroups = useUnit($studyGroups);
    const isLoading = useUnit(findStudyGroupsFx.pending);

    return { studyGroups, isLoading };
};
