import { SendOutlined } from '@ant-design/icons';
import { Button, Input } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { useCreateComment } from '../model';
import { CodeReviewNoteCommentFeature } from '@features';
import { Api } from '@shared';

type Props = {
    note: Api.Services.CodeReviewNoteWithCommentResponse;
    isSelected: boolean;
    onClick?: () => void;
};

export const NoteWithComments = ({ note, isSelected, onClick }: Props) => {
    const { create, canCreate, loading, text, setText } = useCreateComment(note.id);

    return (
        <NoteContainer onClick={onClick} $isSelected={isSelected}>
            <CommentsList>
                {note.comments.map((c) => (
                    <Comment canEdit={isSelected} comment={c} key={c.id} />
                ))}
            </CommentsList>
            {isSelected ? (
                <CommentSenderContainer>
                    <CommentInput
                        value={text}
                        onChange={(e) => {
                            setText(e.target.value);
                        }}
                        autoSize={{ minRows: 1 }}
                        placeholder="Ответить"
                        size="small"
                    />
                    <SendButton
                        disabled={!canCreate}
                        loading={loading}
                        icon={<SendOutlined />}
                        size="small"
                        onClick={create}
                    />
                </CommentSenderContainer>
            ) : (
                <></>
            )}
        </NoteContainer>
    );
};

const NoteContainer = styled.div<{ $isSelected: boolean }>`
    position: relative;
    right: ${({ $isSelected }) => ($isSelected ? '15px' : 'none')};
    display: flex;
    flex-direction: column;
    cursor: pointer;
    background-color: white;
    border-radius: 10px;
    font-size: 0.9em;
    box-shadow: ${({ $isSelected }) =>
        $isSelected ? '0px 2px 7px 0px rgba(0, 0, 0, 0.3)' : '0px 1px 5px 0px rgba(0, 0, 0, 0.2)'};
    z-index: 100;
`;

const CommentsList = styled.div`
    padding: 10px 20px;
`;

const Comment = styled(CodeReviewNoteCommentFeature.UI.EditableComment)`
    &:not(:last-child) {
        padding-bottom: 10px;
        border-bottom: 1px solid #eaeaea;
    }
    &:not(:first-child) {
        padding-top: 10px;
    }
`;

const CommentSenderContainer = styled.div`
    display: flex;
    padding: 5px 0px;
    border-top: 1px solid #eaeaea;
    align-items: end;
`;

const CommentInput = styled(Input.TextArea)`
    border: none !important;
    box-shadow: none !important;
`;

const SendButton = styled(Button)`
    margin: 0 5px 0 0;
`;
