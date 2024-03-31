import { SendOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { usePublishModule } from '../model';

interface Props {
    moduleId: string;
    className?: string;
}

export const PublishModuleButton = ({ moduleId, className }: Props) => {
    const { publishModule, loading } = usePublishModule(moduleId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Опубликовать'}>
                <Button icon={<SendOutlined />} disabled={loading} onClick={publishModule} />
            </Tooltip>
        </div>
    );
};
