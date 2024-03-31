import React from 'react';
import { Button, Form, Input, Modal, Tooltip } from 'antd';
import styled from 'styled-components';
import { Config } from '@shared';
import { FormOutlined } from '@ant-design/icons';
import { ModuleUpdating, useUpdateModuleModal } from '../model';
import { ModuleEntity } from '@entities';

interface Props {
    module: Pick<ModuleEntity.Type.EditableModule, 'id' | 'name'>;
    className?: string;
}

export const UpdateModuleButton = ({ module, className }: Props) => {
    const { update, form, loading, onCancelModal, onOkModal, onShowModal, showModal } = useUpdateModuleModal(module.id);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Редактирование модуля'}>
                <Button onClick={onShowModal} icon={<FormOutlined />} />
            </Tooltip>
            <Modal
                title="Редактирование"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={update} disabled={loading} initialValues={module} layout="vertical">
                    <FormItem validateDebounce={1000} rules={[Config.RuleForm.required]} name="name" label="Название">
                        <Input placeholder="Название" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<ModuleUpdating>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<ModuleUpdating>)`
    width: 100%;
`;
