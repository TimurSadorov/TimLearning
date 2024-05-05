/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ContainerEnvResponse } from './ContainerEnvResponse';

export type ServiceContainerResponse = {
    hostname?: string | null;
    envs?: Array<ContainerEnvResponse> | null;
    healthcheckTest?: Array<string> | null;
};
