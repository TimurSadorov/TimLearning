import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';
import { ChangingForm, usePasswordChangingForm } from '../model';
import { passwordRules } from '../../config/rules';
import { useSearchParams } from 'react-router-dom';

export const PasswordChangingForm = () => {
    const [searchParams] = useSearchParams();
    const email = searchParams.get('email') ?? 's';
    const signature = searchParams.get('signature') ?? 's';

    const { form, submit, changePending } = usePasswordChangingForm(email, signature);

    return (
        <StyledForm form={form} onFinish={submit} disabled={changePending}>
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
            <SubmitButton htmlType="submit">Изменить</SubmitButton>
        </StyledForm>
    );
};

const StyledForm = styled(Form<ChangingForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 15%;
`;

const FormItem = styled(Form.Item<ChangingForm>)`
    width: 100%;
`;

const SubmitButton = styled(Button)`
    width: 50%;
`;
