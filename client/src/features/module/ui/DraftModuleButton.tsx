import { FileSearchOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { useDraftModule } from '../model';

interface Props {
    moduleId: string;
    className?: string;
}

export const DraftModuleButton = ({ moduleId, className }: Props) => {
    const { draftModule, loading } = useDraftModule(moduleId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Перенести в черновик'}>
                <Button icon={<FileSearchOutlined />} disabled={loading} onClick={draftModule} />
            </Tooltip>
        </div>
    );
};
