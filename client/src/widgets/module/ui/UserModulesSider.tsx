import { CourseEntity } from '@entities';
import { Layout, Skeleton } from 'antd';
import React from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { UserModuleRecord } from './UserModuleRecord';

type Props = {
    courseShortName?: CourseEntity.Type.UserCoursAllData['shortName'];
    courseCompletionPercentage?: CourseEntity.Type.UserCoursAllData['completionPercentage'];
    modules?: Pick<CourseEntity.Type.UserCoursAllData['modules'][0], 'id' | 'name' | 'completionPercentage'>[];
    selectedModuleId?: string;
    onClickModuleRecord?: (moduleId: string) => void;
};

export const UserModulesSider = ({
    courseShortName,
    courseCompletionPercentage,
    modules,
    selectedModuleId,
    onClickModuleRecord,
}: Props) => {
    return (
        <SiderContainer theme="light" width="350px">
            <LayoutSider>
                <SiderHeader>
                    <CoursesLink to={Config.routes.root.path}>Вернуться к курсам</CoursesLink>
                    {!courseShortName ? (
                        <Skeleton.Input size="small" active />
                    ) : (
                        <HeaderText>{courseShortName}</HeaderText>
                    )}
                    {!courseCompletionPercentage == undefined ? (
                        <Skeleton.Input active size="small" />
                    ) : (
                        <SharedUI.ProgressBar percent={courseCompletionPercentage} />
                    )}
                </SiderHeader>
                {!!modules && !!selectedModuleId ? (
                    <ModulesList>
                        {modules.map((module) => (
                            <UserModuleRecord
                                key={module.id}
                                name={module.name}
                                completionPercentage={module.completionPercentage}
                                isActive={selectedModuleId === module.id}
                                onClick={() => onClickModuleRecord?.(module.id)}
                            />
                        ))}
                    </ModulesList>
                ) : (
                    <SharedUI.Loader />
                )}
            </LayoutSider>
        </SiderContainer>
    );
};

const SiderContainer = styled(Layout.Sider)`
    position: sticky !important;
    overflow: auto;
    height: calc(100vh - 51px);
    left: 0;
    top: 51px;
`;

const LayoutSider = styled(Layout)`
    background-color: #ffffff;
    height: 100%;
`;

const SiderHeader = styled.div`
    border-bottom: 2px solid #bfdcfe;
    padding: 20px 10px 15px 30px;
    flex-direction: column;
    align-items: start;
    display: flex;
    row-gap: 10px;
`;

const CoursesLink = styled(Link)`
    font-size: 1em;
`;

const HeaderText = styled.div`
    font-size: 1.8em;
    font-weight: 600;
`;

const ModulesList = styled.div`
    background-color: #ffffff;
    display: flex;
    flex-direction: column;
`;
