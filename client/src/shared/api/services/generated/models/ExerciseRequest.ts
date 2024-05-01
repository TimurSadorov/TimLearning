/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { MainAppContainerRequest } from './MainAppContainerRequest';
import type { ServiceContainerImageRequest } from './ServiceContainerImageRequest';

export type ExerciseRequest = {
    appArchiveId: string;
    appContainer: MainAppContainerRequest;
    relativePathToDockerfile: string;
    relativePathToInsertCode: Array<string>;
    insertableCode: string;
    serviceApps: Array<ServiceContainerImageRequest>;
};
