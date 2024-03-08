import React from 'react';
import { useEmailConfirmationForm } from './model';
import { PageLoader } from 'shared/ui';

export const EmailConfirmation = () => {
    useEmailConfirmationForm();

    return <PageLoader />;
};
