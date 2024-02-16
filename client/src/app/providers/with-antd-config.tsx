import React from 'react';
import { FC } from 'react';
import { ConfigProvider } from 'antd';
import ru_RU from 'antd/locale/ru_RU';

// eslint-disable-next-line react/display-name
export const withAntdConfig = (Component: FC) => () => (
    <ConfigProvider locale={ru_RU}>
        <Component />
    </ConfigProvider>
);
