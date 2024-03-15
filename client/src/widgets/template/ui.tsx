import React from 'react';
import { Config } from '@shared';
import { Button } from 'antd';
import { Link, Outlet } from 'react-router-dom';
import styled from 'styled-components';
import { UserOutlined } from '@ant-design/icons';
import { useHeader } from './model';

export const Header = () => {
    const { userIsAuthorized, logout, toAccount, toLogin } = useHeader();

    return (
        <>
            <HeaderContainer>
                <MainPageLink to={Config.routes.root.path}>TimLearning</MainPageLink>
                {userIsAuthorized ? (
                    <AccountBlock>
                        <Button onClick={logout}>Выйти</Button>
                        <AccountIcon onClick={toAccount} />
                    </AccountBlock>
                ) : (
                    <Button onClick={toLogin}>Войти</Button>
                )}
            </HeaderContainer>
            <Outlet />
        </>
    );
};

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
