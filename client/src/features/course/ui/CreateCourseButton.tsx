import { Button, Form, Input, Modal } from 'antd';
import React from 'react';
import { NewCourse, useCreateCourseModal } from '../model';
import styled from 'styled-components';
import { Config } from '@shared';

interface Props {
    className?: string;
}

export const CreateCourseButton = ({ className }: Props) => {
    const { create, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useCreateCourseModal();

    return (
        <div className={className}>
            <Button onClick={onShowModal}>Создать курс</Button>
            <Modal
                title="Создание курса"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={create} disabled={loading}>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name">
                        <Input placeholder="Название" />
                    </FormItem>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="shortName">
                        <Input placeholder="Кароткое название" />
                    </FormItem>
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="description">
                        <Input.TextArea autoSize={{ minRows: 5 }} placeholder="Описание" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<NewCourse>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<NewCourse>)`
    width: 100%;
`;
