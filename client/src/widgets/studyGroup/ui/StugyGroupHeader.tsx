import { StudyGroupEntity } from '@entities';
import { StudyGroupFeature } from '@features';
import { Config, SharedUI } from '@shared';
import React from 'react';
import { Navigate } from 'react-router-dom';
import styled from 'styled-components';

type Props = {
    studyGroupId: string;
};

export const StugyGroupHeader = ({ studyGroupId }: Props) => {
    const { isLoading, findStudyGroupError, studyGroup } = StudyGroupEntity.Model.useStudyGroup(studyGroupId);

    if (!!findStudyGroupError) {
        return <Navigate to={Config.routes.root.getLink()} />;
    }

    return (
        <HeaderContainer>
            {isLoading || !studyGroup ? (
                <Loader />
            ) : (
                <>
                    <NameContainer>
                        <NameTextContainer>
                            <NameDescriptor>Название группы: </NameDescriptor>
                            <NameText>{studyGroup.name}</NameText>
                        </NameTextContainer>
                        <UpdateNameButton studyGroupId={studyGroup.id} name={studyGroup.name} />
                    </NameContainer>
                    <ActionsButtons>
                        <ActiveButton isActive={studyGroup.isActive} studyGroupId={studyGroup.id} />
                        {studyGroup.isActive ? <CopyLinkToJoinButton studyGroupId={studyGroup.id} /> : <></>}
                    </ActionsButtons>
                </>
            )}
        </HeaderContainer>
    );
};

const Loader = styled(SharedUI.Loader)`
    flex: 1;
`;

const HeaderContainer = styled.div`
    margin-top: 20px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: end;
`;

const NameContainer = styled.div`
    margin-top: 20px;
    display: flex;
    flex-direction: row;
    align-items: end;
    max-width: 60%;
`;

const NameTextContainer = styled.div``;

const NameDescriptor = styled.span`
    font-size: 1.2em;
`;

const NameText = styled.span`
    font-size: 1.4em;
    font-weight: 600;
`;

const UpdateNameButton = styled(StudyGroupFeature.UI.UpdateNameButton)`
    margin-left: 10px;
    margin-bottom: 3px;
`;

const ActionsButtons = styled.div`
    display: flex;
    flex-direction: row;
    gap: 0 20px;
`;

const ActiveButton = styled(StudyGroupFeature.UI.ActiveButton)``;

const CopyLinkToJoinButton = styled(StudyGroupFeature.UI.CopyLinkToJoinButton)``;
