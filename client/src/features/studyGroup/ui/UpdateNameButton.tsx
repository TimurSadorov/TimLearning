import { Button, Form, Input, Modal, Tooltip } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { Config } from '@shared';
import { UpdatedNameStudyGroup, useUpdateStudyGroup } from '../model';
import { EditOutlined } from '@ant-design/icons';

interface Props {
    studyGroupId: string;
    name: string;
    className?: string;
}

export const UpdateNameButton = ({ studyGroupId, name, className }: Props) => {
    const { update, form, loading, onCancelModal, onOkModal, onShowModal, showModal } =
        useUpdateStudyGroup(studyGroupId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Редактировать'}>
                <Button size="small" onClick={onShowModal} icon={<EditOutlined />} />
            </Tooltip>

            <Modal
                title="Редактировать"
                open={showModal}
                confirmLoading={loading}
                onOk={onOkModal}
                onCancel={onCancelModal}
            >
                <StyledForm form={form} onFinish={update} disabled={loading}>
                    <FormItem
                        validateDebounce={1000}
                        rules={[Config.RuleForm.required]}
                        name="name"
                        initialValue={name}
                    >
                        <Input placeholder="Название" />
                    </FormItem>
                </StyledForm>
            </Modal>
        </div>
    );
};

const StyledForm = styled(Form<UpdatedNameStudyGroup>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const FormItem = styled(Form.Item<UpdatedNameStudyGroup>)`
    width: 100%;
`;
