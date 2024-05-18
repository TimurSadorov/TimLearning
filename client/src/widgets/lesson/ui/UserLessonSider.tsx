import { Layout, Skeleton } from 'antd';
import React, { useCallback } from 'react';
import styled from 'styled-components';
import { useNavigate } from 'react-router-dom';
import { Api, Config, SharedTypes, SharedUI } from '@shared';
import { UserLessonRecord } from './UserLessonRecord';

type Props = {
    moduleName?: string;
    moduleCompletionPercentage?: number;
    lessons?: SharedTypes.Clone<Api.Services.UserProgressInLessonResponse>[];
    selectedLessonId?: string;
    onClickModuleBack: () => void;
};

export const UserLessonSider = ({
    moduleName,
    moduleCompletionPercentage,
    lessons,
    selectedLessonId,
    onClickModuleBack,
}: Props) => {
    const navigate = useNavigate();
    const toUserLesson = useCallback(
        (lessonId: string) => navigate(Config.routes.userLesson.getLink(lessonId)),
        [navigate],
    );

    return (
        <SiderContainer theme="light" width="350px">
            <LayoutSider>
                <SiderHeader>
                    <ModulesLink onClick={onClickModuleBack}>Вернуться к модулю</ModulesLink>
                    {!moduleName ? <Skeleton.Input size="small" active /> : <HeaderText>{moduleName}</HeaderText>}
                    {moduleCompletionPercentage == undefined ? (
                        <Skeleton.Input active size="small" />
                    ) : (
                        <SharedUI.ProgressBar percent={moduleCompletionPercentage} />
                    )}
                </SiderHeader>
                {!!lessons && !!selectedLessonId ? (
                    <ModulesList>
                        {lessons.map((lesson, index) => (
                            <UserLessonRecord
                                key={lesson.id}
                                isActive={selectedLessonId === lesson.id}
                                onClick={() => toUserLesson(lesson.id)}
                                isFirst={index === 0}
                                isLast={index + 1 === lessons.length}
                                previousLessonHasProgress={
                                    index === 0 ? false : lessons[index - 1].userProgress != undefined
                                }
                                {...lesson}
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

const ModulesLink = styled.a`
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
