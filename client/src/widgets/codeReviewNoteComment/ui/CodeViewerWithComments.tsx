import React, { useCallback, useMemo, useState } from 'react';
import { SharedUI } from '@shared';
import styled from 'styled-components';
import { CodeReviewNoteEntity } from '@entities';
import { CodeReviewNoteCommentFeature, CodeReviewNoteFeature } from '@features';

type Props = {
    code: string;
    reviewId: string;
    canStartNewComments: boolean;
    fontSize?: string;
};

type SelectedCode = {
    start: CodeReviewNoteCommentFeature.Types.Position;
    end: CodeReviewNoteCommentFeature.Types.Position;
};

export const CodeViewerWithComments = ({ code, reviewId, canStartNewComments, fontSize }: Props) => {
    const { notesWithComments, isLoading: isLoadingNotesWithComments } =
        CodeReviewNoteEntity.Model.useNoteWithComments(reviewId);

    const [selectedNoteId, setSelectedNoteId] = useState<string | undefined>(undefined);

    const [selectedCode, setSelectedCode] = useState<SelectedCode>({
        start: { row: 0, column: 0 },
        end: { row: 0, column: 0 },
    });

    const onSelectionChange = useCallback(
        (selection: SharedUI.CodeSelection) => {
            if (notesWithComments) {
                const cursorPosition = selection.cursor;
                const selectedNote = notesWithComments.find(
                    (n) =>
                        (n.startPosition.row < cursorPosition.row ||
                            (n.startPosition.row === cursorPosition.row &&
                                n.startPosition.column <= cursorPosition.column)) &&
                        (n.endPosition.row > cursorPosition.row ||
                            (n.endPosition.row === cursorPosition.row &&
                                n.endPosition.column >= cursorPosition.column)),
                );
                setSelectedNoteId(selectedNote?.id);
            }

            const { lead, anchor } = selection;
            if (lead.row > anchor.row || (lead.row === anchor.row && lead.column >= anchor.column)) {
                setSelectedCode({
                    start: { column: anchor.column, row: anchor.row },
                    end: { column: lead.column, row: lead.row },
                });
            } else {
                setSelectedCode({
                    start: { column: lead.column, row: lead.row },
                    end: { column: anchor.column, row: anchor.row },
                });
            }
        },
        [setSelectedNoteId, notesWithComments],
    );

    const onCreateNewNoteWithComment = useCallback((noteId: string) => setSelectedNoteId(noteId), [setSelectedNoteId]);

    const markers = useMemo(
        () =>
            notesWithComments?.map<SharedUI.NoteMarker>((n) => ({
                startRow: n.startPosition.row,
                startCol: n.startPosition.column,
                endRow: n.endPosition.row,
                endCol: n.endPosition.column,
                isSelected: selectedNoteId === n.id,
            })),
        [notesWithComments, selectedNoteId],
    );

    return !!notesWithComments ? (
        <Container>
            <CodeReviewNoteCommentFeature.UI.CreateNoteWithCommentContextMenu
                reviewId={reviewId}
                start={selectedCode.start}
                end={selectedCode.end}
                isDisabled={canStartNewComments}
                buttonIsDisabled={isLoadingNotesWithComments}
                onCreateNewNoteWithComment={onCreateNewNoteWithComment}
            >
                <CodeEditorContainer $width={notesWithComments.length !== 0 ? 75 : 100}>
                    <SharedUI.CodeEditor
                        value={code}
                        readonly
                        readonlyNoteMarkers={markers}
                        fontSize={fontSize}
                        width={'100%'}
                        onSelectionChange={onSelectionChange}
                    />
                </CodeEditorContainer>
            </CodeReviewNoteCommentFeature.UI.CreateNoteWithCommentContextMenu>
            {notesWithComments.length !== 0 ? (
                <NotesContainer>
                    {notesWithComments.map((n) => (
                        <CodeReviewNoteFeature.UI.NoteWithComments
                            key={n.id}
                            note={n}
                            isSelected={n.id === selectedNoteId}
                            onClick={() => setSelectedNoteId(n.id)}
                        />
                    ))}
                </NotesContainer>
            ) : (
                <></>
            )}
        </Container>
    ) : (
        <></>
    );
};

const Container = styled.div`
    display: flex;
    background-color: #fff0bc;
    border-radius: 10px;
    padding-bottom: 1px;
`;

const CodeEditorContainer = styled.div<{ $width: number }>`
    height: inherit;
    flex: 1;
    width: ${({ $width }) => `${$width}%`};
`;

const NotesContainer = styled.div`
    display: flex;
    flex-direction: column;
    width: 25%;
    gap: 5px;
    margin: 5px 5px;
`;
