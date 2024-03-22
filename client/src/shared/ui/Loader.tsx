import React from 'react';
import { Spin } from 'antd';
import styled from 'styled-components';

interface Props {
    className?: string;
}

export const Loader = ({ className }: Props) => (
    <PageContainer className={className}>
        <Spin size="large" />
    </PageContainer>
);

const PageContainer = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
`;
