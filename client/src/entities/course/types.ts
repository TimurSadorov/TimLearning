import { Api, SharedTypes } from '@shared';

export type UserCours = SharedTypes.Clone<Api.Services.GetUserCoursesResponse>;

export type UserCoursAllData = SharedTypes.Clone<Api.Services.UserCourseAllDataResponse>;

export type EditableCours = SharedTypes.Clone<Api.Services.FindCoursesResponse>;
