import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { NewUserForm, useRegistrationForm } from './model';
import { emailRules, passwordRules } from '../config/rules';
import { Link } from 'react-router-dom';
import { Config } from '@shared';

export const RegistrationForm = () => {
    const { form, submit, registrationPending } = useRegistrationForm();

    return (
        <StyledForm form={form} onFinish={submit} disabled={registrationPending}>
            <FormItem validateDebounce={1000} rules={emailRules} name="email">
                <Input placeholder="Почта" />
            </FormItem>
            <FormItem validateDebounce={1000} rules={passwordRules} name="password">
                <Input placeholder="Пароль" />
            </FormItem>
            <FormItem
                validateDebounce={1000}
                dependencies={['password']}
                rules={[
                    {
                        required: true,
                        message: 'Подтвердите пароль.',
                    },
                    ({ getFieldValue }) => ({ pattern: getFieldValue('password'), message: 'Пароль не совпадает.' }),
                ]}
                name="repeatedPassword"
            >
                <Input placeholder="Подтвердите пароль" />
            </FormItem>
            <SubmitButton htmlType="submit">Регистрация</SubmitButton>
            <LoginLink to={Config.routes.login.path}>Войти</LoginLink>
        </StyledForm>
    );
};

const StyledForm = styled(Form<NewUserForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 15%;
`;

const FormItem = styled(Form.Item<NewUserForm>)`
    width: 100%;
`;

const SubmitButton = styled(Button)`
    width: 50%;
`;

const LoginLink = styled(Link)`
    margin-top: 5px;
    font-size: 0.98em;
`;
