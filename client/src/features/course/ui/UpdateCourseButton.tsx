import { Button, Form, Input, Modal, Tooltip } from 'antd';
import React from 'react';
import { CourseUpdating, useUpdateCourseModal } from '../model';
import styled from 'styled-components';
import { Config } from '@shared';
import { FormOutlined } from '@ant-design/icons';
import { CourseEntity } from '@entities';

interface Props {
    className?: string;
    course: Pick<CourseEntity.Type.EditableCours, 'id' | 'name' | 'shortName' | 'description'>;
}

export const UpdateCourseButton = ({ course, className }: Props) => {
    const { update, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useUpdateCourseModal(course.id);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Редактировать'}>
                <Button onClick={onShowModal} icon={<FormOutlined />} />
            </Tooltip>
            <Modal
                title="Создание курса"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={update} disabled={loading} initialValues={course} layout="vertical">
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name" label="Название">
                        <Input placeholder="Название" />
                    </FormItem>
                    <FormItem
                        validateDebounce={1000}
                        rules={[Config.RuleForm.required]}
                        name="shortName"
                        label="Кароткое название"
                    >
                        <Input placeholder="Кароткое название" />
                    </FormItem>
                    <FormItem
                        validateDebounce={1000}
                        rules={[Config.RuleForm.required]}
                        name="description"
                        label="Описание"
                    >
                        <Input.TextArea autoSize={{ minRows: 5 }} placeholder="Описание" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<CourseUpdating>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<CourseUpdating>)`
    width: 100%;
`;
