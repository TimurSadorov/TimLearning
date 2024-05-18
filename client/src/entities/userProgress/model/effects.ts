import { Api } from '@shared';
import { createEffect } from 'effector';

export const visitLesson = createEffect(
    async (lessonId: string) => await Api.Services.UserProgressService.visitLesson({ lessonId }),
);
