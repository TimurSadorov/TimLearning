import { UserEntity } from 'entities';
import React, { useEffect } from 'react';
import {
    ApiError,
    OpenAPI,
    UserAccountService,
    ValidationErrorResponse,
    ModelValidationErrorResponse,
    ValidationErrorTextResponse,
} from 'shared/api';

const isModel = (resp: ValidationErrorResponse): resp is ModelValidationErrorResponse => 'propertiesErrors' in resp;
const isSimple = (resp: ValidationErrorResponse): resp is ValidationErrorTextResponse => 'message' in resp;

export const TestPage = () => {
    UserEntity.Model.$user;
    const recover = async () => {
        try {
            const resp = await UserAccountService.recoverPassword({
                newPassword: '24321432432',
                signature: 'sdas',
                userEmail: 'sdsada',
            });
        } catch (e) {
            if (e instanceof ApiError) {
                const errorResp = e.body as ValidationErrorResponse;
                if (isSimple(errorResp)) {
                    console.log(errorResp.message);
                }
            } else {
                console.log('net');
            }
        }
    };

    useEffect(() => {
        recover();
    }, []);
    return <div>Test page</div>;
};
