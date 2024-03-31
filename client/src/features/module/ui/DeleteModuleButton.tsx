import { DeleteOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { useDeleteModule } from '../model';

interface Props {
    moduleId: string;
    className?: string;
}

export const DeleteModuleButton = ({ moduleId, className }: Props) => {
    const { deleteModule, loading } = useDeleteModule(moduleId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Удалить'}>
                <Button danger icon={<DeleteOutlined />} disabled={loading} onClick={deleteModule} />
            </Tooltip>
        </div>
    );
};
