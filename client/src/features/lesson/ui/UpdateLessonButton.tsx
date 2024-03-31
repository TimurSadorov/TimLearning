import React from 'react';
import { Button, Form, Input, Modal, Tooltip } from 'antd';
import styled from 'styled-components';
import { Config } from '@shared';
import { FormOutlined } from '@ant-design/icons';
import { LessonEntity } from '@entities';
import { LessonUpdating, useUpdateLessonModal } from '../model';

interface Props {
    lesson: Pick<LessonEntity.Type.LessonSystemData, 'id' | 'name'>;
    className?: string;
}

export const UpdateLessonButton = ({ lesson, className }: Props) => {
    const { update, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useUpdateLessonModal(lesson.id);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Редактирование урока'}>
                <Button onClick={onShowModal} icon={<FormOutlined />} />
            </Tooltip>
            <Modal
                title="Редактирование"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={update} disabled={loading} initialValues={lesson} layout="vertical">
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name" label="Название">
                        <Input placeholder="Название" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<LessonUpdating>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<LessonUpdating>)`
    width: 100%;
`;
