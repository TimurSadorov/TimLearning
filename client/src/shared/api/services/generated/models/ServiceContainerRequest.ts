/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ContainerEnvRequest } from './ContainerEnvRequest';

export type ServiceContainerRequest = {
    hostname?: string | null;
    envs?: Array<ContainerEnvRequest> | null;
    healthcheckTest?: Array<string> | null;
};
