import { Form } from 'antd';
import { useCallback } from 'react';
import { UserEntity } from '@entities';
import { Api, Config, SharedUI } from '@shared';
import { useNavigate } from 'react-router-dom';

export interface NewUserForm {
    email: string;
    password: string;
    repeatedPassword: string;
}

export const useRegistrationForm = () => {
    const navigate = useNavigate();

    const onSuccesRegistratoion = useCallback(() => {
        SharedUI.Model.Notification.notifySuccessFx('Вы успешно зарегистрировались 🎉');
        navigate(Config.routes.login.path);
    }, [navigate]);
    const { register, registrationPending, errorOnRegistration } =
        UserEntity.Model.useRegistration(onSuccesRegistratoion);

    const [form] = Form.useForm<NewUserForm>();
    const submit = useCallback(async (form: NewUserForm) => {
        register(form);
    }, []);

    Api.Model.useValidationErrorTextNotification(errorOnRegistration);
    Api.Model.useModelValidationErrorForForm(errorOnRegistration, form);
    Api.Model.useRequestToServerErrorNotification(errorOnRegistration);

    return {
        form,
        submit,
        registrationPending,
    };
};
