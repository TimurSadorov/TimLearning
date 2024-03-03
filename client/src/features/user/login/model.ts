import { Form } from 'antd';
import { useCallback } from 'react';
import { UserEntity } from '@entities';
import { Api } from '@shared';

export interface AuthForm {
    email: string;
    password: string;
}

export const useLoginForm = () => {
    const [form] = Form.useForm<AuthForm>();
    const { login, isLogin, errorOnLogin } = UserEntity.Model.useLogin();
    const submit = useCallback(async (form: AuthForm) => {
        login(form);
    }, []);

    Api.Model.useValidationErrorTextNotification(errorOnLogin);
    Api.Model.useRequestToServerErrorNotification(errorOnLogin);

    return {
        form,
        submit,
        isLogin,
    };
};
