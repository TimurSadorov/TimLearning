import React from 'react';
import { useJoinToStudyGroup } from '../model';
import { SharedUI } from '@shared';

export const JoiningGroup = () => {
    useJoinToStudyGroup();

    return <SharedUI.Loader />;
};
