import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { AuthForm, useLoginForm } from './model';
import { emailRules, passwordRules } from '../config/rules';

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
