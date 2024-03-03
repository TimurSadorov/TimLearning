import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { NewUserForm, useRegistrationForm } from './model';
import { emailRules, passwordRules } from '../config/rules';

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
