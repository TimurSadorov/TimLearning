import React, { useCallback } from 'react';
import { Config } from '@shared';
import { Button } from 'antd';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import { UserOutlined } from '@ant-design/icons';

interface HeaderProps {
    isLoged: boolean;
    logout: () => void;
}

export const Header = ({ isLoged, logout }: HeaderProps) => {
    const navigate = useNavigate();
    const login = useCallback(() => navigate(Config.routes.login.path), [navigate]);
    const toAccount = useCallback(() => navigate(Config.routes.login.path), [navigate]);

    return (
        <PageContainer>
            <HeaderContainer>
                <MainPageLink to={Config.routes.root.path}>TimLearning</MainPageLink>
                {isLoged ? (
                    <AccountBlock>
                        <Button onClick={logout}>Выйти</Button>
                        <AccountIcon onClick={toAccount} />
                    </AccountBlock>
                ) : (
                    <Button onClick={login}>Войти</Button>
                )}
            </HeaderContainer>
            <Outlet />
        </PageContainer>
    );
};

const PageContainer = styled.div`
    display: flex;
    width: 100vw;
    height: 100vh;
    flex-direction: column;
`;

const HeaderContainer = styled.header`
    position: sticky;
    top: 0;
    background-color: white;
    border-bottom: 1px solid #dedede;
    z-index: 100;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 30px;
    height: 50px;
`;

const MainPageLink = styled(Link)`
    text-decoration: none;
    font-weight: 600;
    color: black;
`;

const AccountBlock = styled.div`
    display: flex;
`;

const AccountIcon = styled(UserOutlined)`
    margin-left: 20px;
    font-size: 20px;
    transition: all 150ms linear;
    &:hover {
        color: #5bd1ff;
    }
`;
