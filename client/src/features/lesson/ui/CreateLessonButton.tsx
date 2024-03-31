import React from 'react';
import { Button, Form, Input, Modal, Tooltip } from 'antd';
import styled from 'styled-components';
import { Config } from '@shared';
import { PlusSquareOutlined } from '@ant-design/icons';
import { NewLesson, useCreateLessonModal } from '../model';

interface Props {
    moduleId: string;
    className?: string;
}

export const CreateLessonButton = ({ moduleId, className }: Props) => {
    const { create, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useCreateLessonModal(moduleId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Добавить урок'}>
                <Button icon={<PlusSquareOutlined />} disabled={loading} onClick={onShowModal} />
            </Tooltip>
            <Modal
                title="Создание урока"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={create} disabled={loading}>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name">
                        <Input placeholder="Название" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<NewLesson>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<NewLesson>)`
    width: 100%;
`;
