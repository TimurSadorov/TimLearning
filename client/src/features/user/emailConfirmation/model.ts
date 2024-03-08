import { UserEntity } from '@entities';
import { Api, Config, SharedUI } from '@shared';
import { useCallback, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';

export const useEmailConfirmationForm = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();

    const navigateOnRoot = () => navigate(Config.routes.root.path, { replace: true });

    const onSuccess = useCallback(() => {
        SharedUI.Model.Notification.notifySuccessFx('Почта успешно подтверждена ❤️');
        navigateOnRoot();
    }, []);

    const onFail = useCallback((error: Error) => {
        Api.Utils.notifyIfValidationErrorText(error);

        if (!Api.Utils.isApiError(error)) {
            SharedUI.Model.Notification.notifyErrorFx('Что то пошло не так, попробуйте позже снова 😔');
        }

        navigateOnRoot();
    }, []);

    const { confirm } = UserEntity.Model.useEmailConfirmation(onSuccess, onFail);

    useEffect(() => {
        confirm({ email: searchParams.get('email') ?? 's', signature: searchParams.get('signature') ?? 's' });
    }, [searchParams]);
};
