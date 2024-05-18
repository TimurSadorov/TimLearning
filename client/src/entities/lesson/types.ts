import { Api, SharedTypes } from '@shared';

export type LessonSystemData = SharedTypes.Clone<Api.Services.LessonSystemDataResponse> & { isDeleted: boolean };

export type UserLesson = SharedTypes.Clone<Api.Services.UserLessonResponse>;

export type UserLessonExerciseTesting = Api.Services.ExerciseTestingRequest & { lessonId: string };

export type UserExerciseTestingResult = SharedTypes.Clone<Api.Services.UserExerciseTestingResponse>;
