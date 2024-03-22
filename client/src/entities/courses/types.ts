import { Api, SharedTypes } from '@shared';

export type UserCours = SharedTypes.Clone<Api.Services.GetUserCoursesResponse>;

export type EditableCours = SharedTypes.Clone<Api.Services.FindCoursesResponse>;
