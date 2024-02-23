/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ContainerEnvRequest } from '../models/ContainerEnvRequest';
import type { ImageSettingsRequest } from '../models/ImageSettingsRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class PracticeExerciseService {

    /**
     * @param formData 
     * @returns any Success
     * @throws ApiError
     */
    public static create(
formData?: {
'NewApp.App': Blob;
'NewApp.PathToDockerfile': string;
'NewApp.ContainerSettings.Hostname'?: string;
'NewApp.ContainerSettings.HealthcheckTest'?: Array<string>;
'NewApp.ContainerSettings.Envs'?: Array<ContainerEnvRequest>;
Code: string;
PathToRewriteFile: string;
Images?: Array<ImageSettingsRequest>;
},
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/exercise',
            formData: formData,
            mediaType: 'multipart/form-data',
            errors: {
                422: `Request validation error.`,
            },
        });
    }

}
