import React, { useCallback, useMemo } from 'react';
import { Config } from '@shared';
import { Button } from 'antd';
import { Link, NavLink, Outlet } from 'react-router-dom';
import styled from 'styled-components';
import { UserOutlined } from '@ant-design/icons';
import { useHeader } from './model';

type StyledNavLink = {
    isActive: boolean;
    isPending: boolean;
    isTransitioning: boolean;
};

export const Header = () => {
    const { userIsAuthorized, logout, toAccount, toLogin, isInRole } = useHeader();

    const isMentor = useMemo(() => isInRole('Mentor'), [isInRole]);

    const styleNav = useCallback<(d: StyledNavLink) => React.CSSProperties>(
        ({ isActive }: StyledNavLink) => ({
            color: isActive ? '#1d1d1d' : '',
            cursor: isActive ? 'auto' : 'pointer',
        }),
        [],
    );

    return (
        <>
            <HeaderContainer>
                <NavBarContainer>
                    <MainPageLink to={Config.routes.root.path}>TimLearning</MainPageLink>
                    <NavLinksContainer>
                        <NavBarLink style={styleNav} to={Config.routes.root.path}>
                            Курсы
                        </NavBarLink>
                        {isMentor ? (
                            <NavBarLink style={styleNav} to={Config.routes.studyGroups.path} end>
                                Учебные группы
                            </NavBarLink>
                        ) : (
                            <></>
                        )}
                    </NavLinksContainer>
                </NavBarContainer>

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

const NavBarContainer = styled.div`
    display: flex;
    flex-direction: row;
`;

const NavLinksContainer = styled.div`
    display: flex;
    flex-direction: row;
    margin-left: 50px;
    gap: 0 20px;
`;

const NavBarLink = styled(NavLink)`
    text-decoration: none;
    color: #737373;

    &:hover {
        color: #1d1d1d;
    }
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
