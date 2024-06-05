import { CodeReviewNoteCommentEntity, CodeReviewNoteEntity } from '@entities';
import { useForm, useWatch } from 'antd/es/form/Form';
import { useUnit } from 'effector-react';
import { useCallback, useEffect, useState } from 'react';
import { Position } from './types';

export type UpdatedComment = { text: string };

export const useUpdateComment = (commentId: string) => {
    const [form] = useForm<UpdatedComment>();

    const [submittable, setSubmittable] = useState<boolean>(false);

    const values = useWatch([], form);

    useEffect(() => {
        setSubmittable(!!values?.text);
    }, [form, values]);

    const onOk = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancel = useCallback(() => {
        form.resetFields();
    }, [form]);

    const loading = useUnit(CodeReviewNoteCommentEntity.Model.updateCodeReviewNoteCommentFx.pending);
    const update = useCallback(
        async (formData: UpdatedComment) => {
            await CodeReviewNoteCommentEntity.Model.updateCodeReviewNoteCommentFx({ commentId, ...formData });
        },
        [commentId],
    );

    return {
        form,
        update,
        submittable,
        loading,
        onOk,
        onCancel,
    };
};

export type NewComment = {
    text: string;
};

export const useCreateCodeReviewNote = (
    reviewId: string,
    start: Position,
    end: Position,
    onCreateNewNoteWithComment: (noteId: string) => void,
) => {
    const [form] = useForm<NewComment>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(CodeReviewNoteEntity.Model.createCodeReviewNoteFx.pending);
    const create = useCallback(
        async (f: NewComment) => {
            const response = await CodeReviewNoteEntity.Model.createCodeReviewNoteFx({
                reviewId,
                startPosition: start,
                endPosition: end,
                initCommentText: f.text,
            });
            setShowModal(false);
            form.resetFields();

            onCreateNewNoteWithComment(response.id);
        },
        [reviewId, start, end],
    );

    return {
        form,
        loading,
        create,
        showModal,
        onShowModal,
        onOkModal,
        onCancelModal,
    };
};
