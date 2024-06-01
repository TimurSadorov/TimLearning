import React from 'react';
import { useJoinToStudyGroup } from '../model';
import { Config, SharedUI } from '@shared';
import { UserEntity } from '@entities';
import { Navigate } from 'react-router-dom';

export const JoiningGroup = () => {
    useJoinToStudyGroup();
    const { isAuthorized } = UserEntity.Model.useUser();

    if (!isAuthorized) {
        return <Navigate to={Config.routes.login.getLink()} replace />;
    }

    return <SharedUI.Loader />;
};
