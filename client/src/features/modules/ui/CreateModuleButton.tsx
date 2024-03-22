import { Button, Form, Input, Modal, Tooltip } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { Config } from '@shared';
import { NewModule, useCreateModuleModal } from '../model';
import { PlusSquareOutlined } from '@ant-design/icons';

interface Props {
    courseId: string;
    className?: string;
}

export const CreateModuleButton = ({ courseId, className }: Props) => {
    const { create, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useCreateModuleModal(courseId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Добавить модуль'}>
                <Button icon={<PlusSquareOutlined />} disabled={loading} onClick={onShowModal} />
            </Tooltip>
            <Modal
                title="Создание модуля"
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

const StyledForm = styled(Form<NewModule>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<NewModule>)`
    width: 100%;
`;
