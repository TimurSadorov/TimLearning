import { Effect, createStore, sample } from 'effector';
import {
    ApiError,
    CodeReviewStatus,
    ValidationErrorResponse,
    ValidationErrorTextResponse,
} from '../services/generated';
import { hasValidationErrorResponse, isApiError } from '../utils';
import { Api } from '@shared';

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

export const AllReviewStatuses = [
    CodeReviewStatus.PENDING,
    CodeReviewStatus.STARTED,
    CodeReviewStatus.COMPLETED,
    CodeReviewStatus.REJECTED,
];

export const getReviewStatusName = (status: Api.Services.CodeReviewStatus) => {
    switch (status) {
        case Api.Services.CodeReviewStatus.PENDING:
            return 'Ожидает проверки';
        case Api.Services.CodeReviewStatus.STARTED:
            return 'Проверка начата';
        case Api.Services.CodeReviewStatus.COMPLETED:
            return 'Пройдено';
        case Api.Services.CodeReviewStatus.REJECTED:
            return 'Откланено';
        default:
            const exhaustiveCheck: never = status;
            throw new Error(exhaustiveCheck);
    }
};
