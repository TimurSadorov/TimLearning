import { CopyOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React, { useCallback } from 'react';
import { StudyGroupEntity } from '@entities';
import { SharedUI } from '@shared';

interface Props {
    studyGroupId: string;
    className?: string;
}

export const CopyLinkToJoinButton = ({ studyGroupId, className }: Props) => {
    const { linkToJoin, isLoading } = StudyGroupEntity.Model.useLinkToJoin(studyGroupId);
    const copyLink = useCallback(() => {
        navigator.clipboard.writeText(linkToJoin?.linkToJoin ?? '');
        SharedUI.Model.Notification.notifySuccessFx('Ссылка успешно скопирована');
    }, [linkToJoin]);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Скопировать'}>
                <Button icon={<CopyOutlined />} disabled={isLoading} onClick={copyLink}>
                    Ссылка на присоединение
                </Button>
            </Tooltip>
        </div>
    );
};
