/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { MainAppContainerResponse } from './MainAppContainerResponse';
import type { ServiceContainerImageResponse } from './ServiceContainerImageResponse';

export type ExerciseResponse = {
    appArchiveId: string;
    appContainer: MainAppContainerResponse;
    relativePathToDockerfile: string;
    relativePathToInsertCode: Array<string>;
    insertableCode: string;
    serviceApps: Array<ServiceContainerImageResponse>;
};
