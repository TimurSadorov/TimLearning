import { useGate, useUnit } from 'effector-react';
import { $notesWithComments, NotesWithCommentsGate } from './model';
import { getCodeReviewNotesWithComments } from './effects';

export const useNoteWithComments = (reviewId: string) => {
    useGate(NotesWithCommentsGate, reviewId);
    const notesWithComments = useUnit($notesWithComments);
    const isLoading = useUnit(getCodeReviewNotesWithComments.pending);

    return { notesWithComments, isLoading };
};
