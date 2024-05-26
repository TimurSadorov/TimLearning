/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateStudyGroupRequest } from '../models/CreateStudyGroupRequest';
import type { CreateStudyGroupResponse } from '../models/CreateStudyGroupResponse';
import type { GetLinkToJoinToStudyGroupResponse } from '../models/GetLinkToJoinToStudyGroupResponse';
import type { JoinToStudyGroupRequest } from '../models/JoinToStudyGroupRequest';
import type { StudyGroupResponse } from '../models/StudyGroupResponse';
import type { UpdateStudyGroupRequest } from '../models/UpdateStudyGroupRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class StudyGroupService {

    /**
     * @param ids 
     * @param searchName 
     * @param isActive 
     * @returns StudyGroupResponse Success
     * @throws ApiError
     */
    public static findStudyGroups(
ids?: Array<string>,
searchName?: string,
isActive?: boolean,
): CancelablePromise<Array<StudyGroupResponse>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/study-groups/find',
            query: {
                'Ids': ids,
                'SearchName': searchName,
                'IsActive': isActive,
            },
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param studyGroupId 
     * @returns GetLinkToJoinToStudyGroupResponse Success
     * @throws ApiError
     */
    public static getLinkToJoinToStudyGroup(
studyGroupId: string,
): CancelablePromise<GetLinkToJoinToStudyGroupResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/study-groups/${studyGroupId}/link-to-join',
            path: {
                'studyGroupId': studyGroupId,
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
     * @param requestBody 
     * @returns CreateStudyGroupResponse Success
     * @throws ApiError
     */
    public static createStudyGroup(
requestBody: CreateStudyGroupRequest,
): CancelablePromise<CreateStudyGroupResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/study-groups',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
                403: `Forbidden`,
                422: `Request validation error.`,
            },
        });
    }

    /**
     * @param studyGroupId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static updateStudyGroup(
studyGroupId: string,
requestBody: UpdateStudyGroupRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/study-groups/${studyGroupId}',
            path: {
                'studyGroupId': studyGroupId,
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
     * @param studyGroupId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static joinToStudyGroup(
studyGroupId: string,
requestBody: JoinToStudyGroupRequest,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/study-groups/${studyGroupId}/join',
            path: {
                'studyGroupId': studyGroupId,
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

}
