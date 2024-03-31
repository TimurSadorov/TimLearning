import { Api, SharedTypes } from '@shared';

export type LessonSystemData = SharedTypes.Clone<Api.Services.LessonSystemDataResponse> & { isDeleted: boolean };
