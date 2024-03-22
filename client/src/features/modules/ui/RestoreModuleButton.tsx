import { ToTopOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { useRestoreModule } from '../model';

interface Props {
    moduleId: string;
    className?: string;
}

export const RestoreModuleButton = ({ moduleId, className }: Props) => {
    const { restoreModule, loading } = useRestoreModule(moduleId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Восстановить'}>
                <RecoverButton icon={<ToTopOutlined />} disabled={loading} onClick={restoreModule} />
            </Tooltip>
        </div>
    );
};

const RecoverButton = styled(Button)`
    color: #4dbc4d;
`;
