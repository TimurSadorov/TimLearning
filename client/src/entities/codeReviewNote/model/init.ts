import { sample } from 'effector';
import { NotesWithCommentsGate } from './model';
import { createCodeReviewNoteFx, getCodeReviewNotesWithComments } from './effects';
import { CodeReviewNoteCommentEntity } from '@entities';

sample({
    clock: [
        NotesWithCommentsGate.state,
        createCodeReviewNoteFx.done,
        CodeReviewNoteCommentEntity.Model.createCodeReviewNoteCommentFx.done,
        CodeReviewNoteCommentEntity.Model.deleteCodeReviewNoteCommentFx.done,
        CodeReviewNoteCommentEntity.Model.updateCodeReviewNoteCommentFx.done,
    ],
    source: NotesWithCommentsGate.state,
    filter: (r) => typeof r === 'string',
    target: getCodeReviewNotesWithComments,
});
