import { CommentOutlined } from '@ant-design/icons';
import { Dropdown, Form, Input, Modal } from 'antd';
import { MenuProps } from 'antd/lib';
import React, { useMemo } from 'react';
import { Position } from '../types';
import { NewComment, useCreateCodeReviewNote } from '../model';
import styled from 'styled-components';
import { Config } from '@shared';

type Props = {
    reviewId: string;
    start: Position;
    end: Position;
    children: JSX.Element;
    buttonIsDisabled: boolean;
    isDisabled: boolean;
    onCreateNewNoteWithComment: (noteId: string) => void;
};

export const CreateNoteWithCommentContextMenu = ({
    reviewId,
    start,
    end,
    buttonIsDisabled,
    isDisabled,
    onCreateNewNoteWithComment,
    children,
}: Props) => {
    const { create, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useCreateCodeReviewNote(
        reviewId,
        start,
        end,
        onCreateNewNoteWithComment,
    );

    const isCodeHighlighted = !(start.row === end.row && start.column === end.column);

    const items = useMemo<MenuProps['items']>(
        () => [
            {
                key: 'com',
                label: 'Оставить комментарий',
                icon: <CommentOutlined />,
                disabled: !isCodeHighlighted || buttonIsDisabled,
                onClick: onShowModal,
            },
        ],
        [onShowModal, isCodeHighlighted, buttonIsDisabled],
    );

    return !isDisabled ? (
        <>
            <Dropdown menu={{ items: items }} trigger={['contextMenu']}>
                {children}
            </Dropdown>
            <Modal
                title="Новый комментарий"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={create} disabled={loading}>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="text">
                        <Input.TextArea autoSize={{ minRows: 2 }} placeholder="Текст комментария" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </>
    ) : (
        children
    );
};

const StyledForm = styled(Form<NewComment>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<NewComment>)`
    width: 100%;
`;
