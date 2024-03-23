import { SharedUI } from '@shared';
import {
    ApiError,
    ModelValidationErrorResponse,
    ValidationErrorResponse,
    ValidationErrorTextResponse,
} from './services/generated';

export const isApiError = (error: Error): error is ApiError => error instanceof ApiError;

export const isNotApiError = (error: Error): boolean => !isApiError(error);

export const hasValidationErrorResponse = (error: ApiError) => error.status === 422;

export const isModelValidationError = (response: ValidationErrorResponse): response is ModelValidationErrorResponse =>
    (response as ModelValidationErrorResponse).propertiesErrors !== undefined;

export const isValidationErrorText = (response: ValidationErrorResponse): response is ValidationErrorTextResponse =>
    (response as ValidationErrorTextResponse).message !== undefined;

export const notifyIfValidationErrorText = (error: Error) => {
    if (isApiError(error) && hasValidationErrorResponse(error) && isValidationErrorText(error.body)) {
        SharedUI.Model.Notification.notifyErrorFx((error.body as ValidationErrorTextResponse).message);
    }
};
