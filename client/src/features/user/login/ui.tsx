import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { AuthForm, useLoginForm } from './model';
import { emailRules, passwordRules } from '../config/rules';
import { Link } from 'react-router-dom';
import { Config } from '@shared';

export const LoginForm = () => {
    const { form, submit, loginPending } = useLoginForm();

    return (
        <StyledForm form={form} onFinish={submit} disabled={loginPending}>
            <FormItem validateDebounce={1000} rules={emailRules} name="email">
                <Input placeholder="Почта" />
            </FormItem>
            <FormItem validateDebounce={1000} rules={passwordRules} name="password">
                <Input.Password placeholder="Пароль" />
            </FormItem>
            <InfoContainer>
                <Link to={Config.routes.registration.path}>Регистрация</Link>
                <Link to={Config.routes.passwordRecovery.path}>Забыли пароль?</Link>
            </InfoContainer>
            <SubmitButton htmlType="submit">Войти</SubmitButton>
        </StyledForm>
    );
};

const StyledForm = styled(Form<AuthForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 15%;
`;

const FormItem = styled(Form.Item<AuthForm>)`
    width: 100%;
`;

const SubmitButton = styled(Button)`
    width: 50%;
`;

const InfoContainer = styled.div`
    display: flex;
    width: 100%;
    justify-content: space-between;
    margin-bottom: 5px;
    font-size: 0.98em;
`;
