import { Effect, createStore, sample } from 'effector';
import { ApiError, ValidationErrorResponse, ValidationErrorTextResponse } from '../generated';
import { hasValidationErrorResponse, isApiError } from '../utils';

export const createValidationErrorResponseStore = (effect: Effect<any, any, Error>) => {
    const $validationError = createStore<ValidationErrorResponse | null>(null);

    sample({
        clock: effect.failData,
        filter: (error: Error) => isApiError(error) && hasValidationErrorResponse(error),
        fn: (error: Error) => (error as ApiError).body as ValidationErrorTextResponse,
        target: $validationError,
    });

    return $validationError;
};
