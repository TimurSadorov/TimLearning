import { Api } from '@shared';
import { restore } from 'effector';
import { createGate } from 'effector-react';
import { getCodeReviewNotesWithComments } from './effects';

export const NotesWithCommentsGate = createGate<string>();
export const $notesWithComments = restore<Api.Services.CodeReviewNoteWithCommentResponse[]>(
    getCodeReviewNotesWithComments,
    null,
).reset(NotesWithCommentsGate.close);
