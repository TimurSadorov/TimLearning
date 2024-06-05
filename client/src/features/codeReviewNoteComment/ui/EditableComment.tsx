import React, { useCallback, useState } from 'react';
import { MoreOutlined } from '@ant-design/icons';
import { CodeReviewNoteCommentEntity, UserEntity } from '@entities';
import { Api } from '@shared';
import styled from 'styled-components';
import { Button, Form, Input, Menu } from 'antd';
import { UpdatedComment, useUpdateComment } from '../model';

type Props = {
    comment: Api.Services.CodeReviewNoteCommentResponse;
    canEdit: boolean;
    className?: string;
};

export const EditableComment = ({ comment, canEdit, className }: Props) => {
    const { user } = UserEntity.Model.useUser();

    const [inEditingState, setInEditingState] = useState(false);
    const { form, onCancel, onOk, update, loading, submittable } = useUpdateComment(comment.id);

    const onClickCancel = useCallback(() => {
        setInEditingState(false);
        onCancel();
    }, [setInEditingState, onCancel]);

    const onClickOk = useCallback(() => {
        setInEditingState(false);
        onOk();
    }, [setInEditingState, onCancel]);

    return (
        <CommentContainer className={className}>
            <CommentHeader>
                <CommentInfoContainer>
                    <CommentUserEmail>{comment.authorEmail}</CommentUserEmail>
                    <CommentDate>{new Date(comment.added).toLocaleString()}</CommentDate>
                </CommentInfoContainer>
                {canEdit && user?.email === comment.authorEmail ? (
                    <CommentMenu
                        selectable={false}
                        expandIcon={<MoreOutlined />}
                        items={[
                            {
                                key: 'm',
                                children: [
                                    {
                                        key: 'ed',
                                        label: 'Редактировать',
                                        onClick: () => setInEditingState(true),
                                    },
                                    {
                                        key: 'del',
                                        label: 'Удалить',
                                        onClick: () =>
                                            CodeReviewNoteCommentEntity.Model.deleteCodeReviewNoteCommentFx(comment.id),
                                    },
                                ],
                            },
                        ]}
                    />
                ) : (
                    <></>
                )}
            </CommentHeader>
            <CommentText>
                {inEditingState ? (
                    <EditableText>
                        <StyledForm form={form} onFinish={update} disabled={loading}>
                            <FormItem name="text" initialValue={comment.text}>
                                <Input.TextArea autoSize placeholder="Комментарий" />
                            </FormItem>
                        </StyledForm>
                        <FormButtons>
                            <SaveButton onClick={onClickOk} type="primary" disabled={loading || !submittable}>
                                Сохранить
                            </SaveButton>
                            <CancelButton onClick={onClickCancel} disabled={loading}>
                                Отменить
                            </CancelButton>
                        </FormButtons>
                    </EditableText>
                ) : (
                    comment.text
                )}
            </CommentText>
        </CommentContainer>
    );
};

const CommentContainer = styled.div`
    display: flex;
    flex-direction: column;
`;

const CommentHeader = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
`;

const CommentInfoContainer = styled.div`
    display: flex;
    flex-direction: column;
`;

const CommentUserEmail = styled.div``;

const CommentDate = styled.div`
    font-size: 0.86em;
    color: #808080;
`;

const CommentMenu = styled(Menu)`
    border: none !important;
    margin: -10px !important;
`;

const CommentText = styled.div`
    margin-top: 5px;
    white-space: pre-line;
`;

const EditableText = styled.div`
    display: flex;
    flex-direction: column;
`;

const StyledForm = styled(Form<UpdatedComment>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<UpdatedComment>)`
    width: 100%;
`;

const FormButtons = styled.div`
    display: grid;
    gap: 10px;
    grid-template-columns: repeat(2, 1fr);
    margin-top: -15px;
`;

const SaveButton = styled(Button)``;

const CancelButton = styled(Button)``;
