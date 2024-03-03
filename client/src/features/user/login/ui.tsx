import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { AuthForm, useLoginForm } from './model';

export const LoginForm = () => {
    const { form, submit, isLogin } = useLoginForm();

    return (
        <StyledForm form={form} onFinish={submit} disabled={isLogin}>
            <FormItem
                validateDebounce={1000}
                rules={[
                    {
                        required: true,
                        message: 'Почта должна быть заполнен.',
                    },
                    {
                        pattern: /^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$/i,
                        message: 'Невалидная почта.',
                    },
                ]}
                name="email"
            >
                <Input placeholder="Почта" />
            </FormItem>
            <FormItem
                validateDebounce={1000}
                rules={[{ required: true, message: 'Пароль должна быть заполнен.' }]}
                name="password"
            >
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
