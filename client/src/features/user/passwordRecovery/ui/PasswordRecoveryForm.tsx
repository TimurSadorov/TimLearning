import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { RecoveringForm, usePasswordRecoveryForm } from '../model';
import { emailRules } from '../../config/rules';
import { Link } from 'react-router-dom';
import { Config } from '@shared';

export const PasswordRecoveryForm = () => {
    const { form, submit, recoverPending } = usePasswordRecoveryForm();

    return (
        <StyledForm form={form} onFinish={submit} disabled={recoverPending}>
            <FormItem validateDebounce={1000} rules={emailRules} name="userEmail">
                <Input placeholder="Почта аккаунта" />
            </FormItem>
            <SubmitButton htmlType="submit">Восстановить</SubmitButton>
            <LoginLink to={Config.routes.login.path}>Вход</LoginLink>
        </StyledForm>
    );
};

const StyledForm = styled(Form<RecoveringForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 15%;
`;

const FormItem = styled(Form.Item<RecoveringForm>)`
    width: 100%;
`;

const SubmitButton = styled(Button)`
    width: 50%;
`;

const LoginLink = styled(Link)`
    margin-top: 5px;
    font-size: 0.98em;
`;
