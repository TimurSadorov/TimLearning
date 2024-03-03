import { UserEntity } from '@entities';
import { Button } from 'antd';
import React from 'react';

export const TestPage = () => {
    return (
        <Button
            onClick={() => {
                UserEntity.Model.loadUserFx();
            }}
        >
            Вошел
        </Button>
    );
};
