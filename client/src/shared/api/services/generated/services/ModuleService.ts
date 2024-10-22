/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ChangeModuleOrderRequest } from '../models/ChangeModuleOrderRequest';
import type { CreateModuleRequest } from '../models/CreateModuleRequest';
import type { FindOrderedModulesResponse } from '../models/FindOrderedModulesResponse';
import type { ModuleAllDataResponse } from '../models/ModuleAllDataResponse';
import type { UpdateModuleRequest } from '../models/UpdateModuleRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class ModuleService {

    /**
     * @param courseId 
     * @param isDeleted 
     * @param isDraft 
     * @returns FindOrderedModulesResponse Success
     * @throws ApiError
     */
    public static findOrderedModules(
courseId: string,
isDeleted: boolean,
isDraft?: boolean,
): CancelablePromise<Array<FindOrderedModulesResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/courses/{courseId}/modules/ordered/find',
            path: {
                'courseId': courseId,
            },
            query: {
                'IsDeleted': isDeleted,
                'IsDraft': isDraft,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @returns ModuleAllDataResponse Success
     * @throws ApiError
     */
    public static getModuleAllData(
moduleId: string,
): CancelablePromise<ModuleAllDataResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/modules/{moduleId}',
            path: {
                'moduleId': moduleId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static updateModule(
moduleId: string,
requestBody: UpdateModuleRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/modules/{moduleId}',
            path: {
                'moduleId': moduleId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param courseId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static createModule(
courseId: string,
requestBody: CreateModuleRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/courses/{courseId}/modules',
            path: {
                'courseId': courseId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static changeModuleOrder(
moduleId: string,
requestBody: ChangeModuleOrderRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/modules/{moduleId}/order',
            path: {
                'moduleId': moduleId,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @returns any Success
     * @throws ApiError
     */
    public static deleteModule(
moduleId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/modules/{moduleId}/delete',
            path: {
                'moduleId': moduleId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param moduleId 
     * @returns any Success
     * @throws ApiError
     */
    public static restoreModule(
moduleId: string,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/modules/{moduleId}/restore',
            path: {
                'moduleId': moduleId,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                404: `Not Found`,
                422: `Request validation error.`,
            },
        });
    }

}
