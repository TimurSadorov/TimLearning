import { useEffect } from 'react';
import { ValidationErrorResponse, ModelValidationErrorResponse } from '../services/generated';
import {
    hasValidationErrorResponse,
    isApiError,
    isNotApiError,
    isValidationErrorText,
    isModelValidationError,
    notifyIfValidationErrorText,
    isNotFoundApiError,
} from '../utils';
import { SharedUI } from '@shared';
import { FormInstance } from 'antd';
import { useNavigate } from 'react-router-dom';

export const useNotFoundEntity = (error: Error | null, textOnNotFound: string, navigationPath: string) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (!error) {
            return;
        }

        if (isNotFoundApiError(error)) {
            SharedUI.Model.Notification.notifyErrorFx(textOnNotFound);
            navigate(navigationPath);
        }
    }, [error, textOnNotFound, navigationPath, navigate]);
};

export const useRequestToServerErrorNotification = (
    error: Error | null,
    textOnError = 'При запросе на сервер произошла ошибка, пожалуйста, попробуйте снова позже.',
) => {
    useEffect(() => {
        if (error !== null && isNotApiError(error)) {
            SharedUI.Model.Notification.notifyErrorFx(textOnError);
        }
    }, [error, textOnError]);
};

export const useValidationErrorTextNotification = (error: Error | ValidationErrorResponse | null) => {
    useEffect(() => {
        if (error === null) {
            return;
        }

        if (error instanceof Error) {
            notifyIfValidationErrorText(error);
        } else {
            if (isValidationErrorText(error)) {
                SharedUI.Model.Notification.notifyErrorFx(error.message);
            }
        }
    }, [error]);
};

export const useModelValidationErrorForForm = <FormValue>(
    error: Error | ValidationErrorResponse | null,
    form: FormInstance<FormValue>,
) => {
    useEffect(() => {
        if (error === null) {
            return;
        }

        if (error instanceof Error) {
            if (isApiError(error) && hasValidationErrorResponse(error) && isModelValidationError(error.body)) {
                setErrorsForForm(form, error.body.propertiesErrors);
            }
        } else {
            if (isModelValidationError(error)) {
                setErrorsForForm(form, error.propertiesErrors);
            }
        }
    }, [error, form]);
};

const setErrorsForForm = <FormValue>(
    form: FormInstance<FormValue>,
    propertiesErrors: ModelValidationErrorResponse['propertiesErrors'],
) => {
    form.setFields(
        Object.keys(propertiesErrors).map((propertyName) => ({
            name: propertyName,
            errors: propertiesErrors[propertyName],
        })),
    );
};
