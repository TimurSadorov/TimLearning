import React from 'react';
import { Spin } from 'antd';
import styled from 'styled-components';
import { LoadingOutlined } from '@ant-design/icons';

interface Props {
    className?: string;
}

export const SmalLoader = ({ className }: Props) => (
    <PageContainer className={className}>
        <Spin indicator={<LoadingOutlined style={{ fontSize: 24 }} spin />} />
    </PageContainer>
);

const PageContainer = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
`;
