import React from 'react';
import { Spin } from 'antd';
import styled from 'styled-components';

export const PageLoader = () => (
    <PageContainer>
        <Spin size="large" />
    </PageContainer>
);

const PageContainer = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
`;