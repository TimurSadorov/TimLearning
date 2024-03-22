import React from 'react';
import { useEmailConfirmationForm } from './model';
import { Loader } from 'shared/ui';

export const EmailConfirmation = () => {
    useEmailConfirmationForm();

    return <Loader />;
};
