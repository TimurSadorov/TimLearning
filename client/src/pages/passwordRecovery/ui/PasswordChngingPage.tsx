import React from 'react';
import styled from 'styled-components';
import { UserFeature } from '@features';

export const PasswordChngingPage = () => {
    return (
        <PageContainer>
            <FormBock>
                <Header>Изменение пароля</Header>
                <UserFeature.PasswordRecovery.UI.PasswordChangingForm />
            </FormBock>
        </PageContainer>
    );
};

const PageContainer = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
`;

const FormBock = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
`;

const Header = styled.header`
    font-size: 1.7em;
    font-weight: 500;
    margin: 30px 0;
`;
