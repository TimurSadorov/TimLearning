/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AuthTokensResponse } from '../models/AuthTokensResponse';
import type { LoginRequest } from '../models/LoginRequest';
import type { NewUserRequest } from '../models/NewUserRequest';
import type { RecoverPasswordRequest } from '../models/RecoverPasswordRequest';
import type { RefreshRequest } from '../models/RefreshRequest';
import type { SendEmailConfirmationRequest } from '../models/SendEmailConfirmationRequest';
import type { SendMailToRecoverPasswordRequest } from '../models/SendMailToRecoverPasswordRequest';
import type { UserEmailConfirmationRequest } from '../models/UserEmailConfirmationRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UserAccountService {

    /**
     * @param requestBody 
     * @returns any Created
     * @throws ApiError
     */
    public static register(
requestBody: NewUserRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/register',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static sendEmailConfirmation(
requestBody: SendEmailConfirmationRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/email/confirmation/send',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static confirmEmail(
requestBody: UserEmailConfirmationRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/email/confirm',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns AuthTokensResponse Success
     * @throws ApiError
     */
    public static login(
requestBody: LoginRequest,
): CancelablePromise<AuthTokensResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/login',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns AuthTokensResponse Success
     * @throws ApiError
     */
    public static refresh(
requestBody: RefreshRequest,
): CancelablePromise<AuthTokensResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/refresh',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static sendMailToRecoverPassword(
requestBody: SendMailToRecoverPasswordRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/password/mail/recover',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static recoverPassword(
requestBody: RecoverPasswordRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/user/account/password/recover',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

}
