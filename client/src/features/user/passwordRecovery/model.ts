import { UserEntity } from '@entities';
import { Api, Config, SharedUI } from '@shared';
import { Form } from 'antd';
import { useCallback } from 'react';
import { useNavigate } from 'react-router-dom';

export interface RecoveringForm {
    userEmail: string;
}

export const usePasswordRecoveryForm = () => {
    const navigate = useNavigate();

    const onSuccesRecovering = useCallback(() => {
        SharedUI.Model.Notification.notifySuccessFx('На почту отправлено письмо для восстановления 📧');
        navigate(Config.routes.login.path);
    }, [navigate]);

    const { recover, recoverPending, errorOnPasswordRecovery } =
        UserEntity.Model.usePasswordRecovery(onSuccesRecovering);

    const [form] = Form.useForm<RecoveringForm>();
    const submit = useCallback(async (form: RecoveringForm) => {
        recover(form);
    }, []);

    Api.Model.useValidationErrorTextNotification(errorOnPasswordRecovery);
    Api.Model.useModelValidationErrorForForm(errorOnPasswordRecovery, form);
    Api.Model.useRequestToServerErrorNotification(errorOnPasswordRecovery);

    return {
        form,
        submit,
        recoverPending,
    };
};
