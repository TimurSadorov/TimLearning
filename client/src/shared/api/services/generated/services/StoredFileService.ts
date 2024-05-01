/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class StoredFileService {

    /**
     * @param formData 
     * @returns string Success
     * @throws ApiError
     */
    public static saveExerciseAppFile(
formData?: {
File: Blob;
},
): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/stored-files/exercise-app',
            formData: formData,
            mediaType: 'multipart/form-data',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

}
