import { CodeReviewNoteCommentEntity } from '@entities';
import { useUnit } from 'effector-react';
import { useState } from 'react';

export const useCreateComment = (noteId: string) => {
    const [text, setText] = useState('');
    const canCreate = !!text;

    const loading = useUnit(CodeReviewNoteCommentEntity.Model.createCodeReviewNoteCommentFx.pending);
    const create = async () => {
        await CodeReviewNoteCommentEntity.Model.createCodeReviewNoteCommentFx({ noteId: noteId, text: text });
        setText('');
    };

    return {
        loading,
        create,
        canCreate,
        setText,
        text,
    };
};
