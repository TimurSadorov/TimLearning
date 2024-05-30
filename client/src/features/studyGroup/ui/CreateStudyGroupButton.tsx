import { Button, Form, Input, Modal } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { Config } from '@shared';
import { NewStudyGroup, useCreateStudyGroup } from '../model';
import { CourseEntity } from '@entities';

interface Props {
    className?: string;
}

export const CreateStudyGroupButton = ({ className }: Props) => {
    const { create, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useCreateStudyGroup();

    return (
        <div className={className}>
            <Button onClick={onShowModal}>Создать учебную группу</Button>
            <Modal
                title="Создание учебной группы"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={create} disabled={loading}>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name">
                        <Input placeholder="Название" />
                    </FormItem>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="courseId">
                        <CourseEntity.UI.CreateStudyGroup />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<NewStudyGroup>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<NewStudyGroup>)`
    width: 100%;
`;
