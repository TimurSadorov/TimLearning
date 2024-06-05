import { Api } from '@shared';
import { createEffect } from 'effector';

export type WithNoteId<T> = T & { noteId: string };

export type WithCommentId<T> = T & { commentId: string };

export const createCodeReviewNoteCommentFx = createEffect(
    async (request: WithNoteId<Api.Services.CreateCodeReviewNoteCommentRequest>) => {
        return await Api.Services.CodeReviewNoteCommentService.createCodeReviewNoteComment(request.noteId, {
            text: request.text,
        });
    },
);

export const deleteCodeReviewNoteCommentFx = createEffect(async (commentId: string) => {
    return await Api.Services.CodeReviewNoteCommentService.deleteCodeReviewNoteComment(commentId);
});

export const updateCodeReviewNoteCommentFx = createEffect(
    async (request: WithCommentId<Api.Services.UpdateCodeReviewNoteCommentRequest>) => {
        return await Api.Services.CodeReviewNoteCommentService.updateCodeReviewNoteComment(request.commentId, {
            text: request.text,
        });
    },
);
