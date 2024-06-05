import React, { useCallback, useDeferredValue, useEffect, useMemo, useState } from 'react';
import AceEditor, { IMarker } from 'react-ace';
import './css/lesson-editing.css';
import 'ace-builds/src-noconflict/mode-csharp';
import 'ace-builds/src-noconflict/theme-monokai';
import 'ace-builds/src-noconflict/theme-tomorrow';
import 'ace-builds/src-noconflict/ext-language_tools';
import styled from 'styled-components';

export type NoteMarker = Pick<IMarker, 'startRow' | 'startCol' | 'endRow' | 'endCol'> & { isSelected: boolean };

export type CodeAnchor = {
    row: number;
    column: number;
};

export type CodeSelection = {
    anchor: CodeAnchor;
    cursor: CodeAnchor;
    lead: CodeAnchor;
};

export type CodeEditorProps = {
    value?: string;
    onChange?: (value?: string) => void;
    onSelectionChange?: (value: CodeSelection, event?: any) => void;
    readonly?: boolean;
    readonlyNoteMarkers?: NoteMarker[];
    fontSize?: string;
    width?: string;
};

export const CodeEditor = ({
    value,
    onChange,
    onSelectionChange,
    readonly,
    readonlyNoteMarkers,
    fontSize,
    width,
}: CodeEditorProps) => {
    const [codeValue, setСodeValue] = useState(value);
    useEffect(() => setСodeValue(value), [value]);

    const [rowCount, setRowCount] = useState(1);
    useEffect(() => {
        if (!!codeValue) {
            setRowCount(codeValue.split(/\n/).length);
        } else {
            setRowCount(1);
        }
    }, [codeValue]);

    const onChangeCode = useCallback(
        (v: string) => {
            setСodeValue(v);
            onChange?.(v);
        },
        [onChange],
    );

    const markers = useMemo(
        () =>
            readonlyNoteMarkers?.map<IMarker>((m) => ({
                ...m,
                type: 'text',
                className: m.isSelected ? 'selected-note-marker' : 'note-marker',
            })),
        [readonlyNoteMarkers],
    );

    return readonly ? (
        <ReadonlyEditor
            $rowCount={rowCount}
            mode="csharp"
            width={width ?? '100%'}
            fontSize={fontSize ?? '1em'}
            value={codeValue}
            markers={markers}
            onSelectionChange={onSelectionChange}
            setOptions={{
                foldStyle: 'manual',
                readOnly: true,
                highlightActiveLine: false,
                highlightGutterLine: false,
            }}
        />
    ) : (
        <AceEditor
            className="code-editor"
            mode="csharp"
            theme={'monokai'}
            height={`${rowCount * 18 + 2}px`}
            width={width ?? '100%'}
            fontSize={fontSize ?? '1em'}
            value={codeValue}
            onChange={onChangeCode}
            onSelectionChange={onSelectionChange}
            enableLiveAutocompletion={true}
            enableBasicAutocompletion={true}
            setOptions={{
                foldStyle: 'manual',
            }}
        />
    );
};

const ReadonlyEditor = styled(AceEditor)<{ $rowCount: number }>`
    min-height: ${({ $rowCount }) => `${$rowCount * 18 + 2}px`};
    height: 100% !important;
    border-radius: 10px;
    border: 1px solid #e5e5e5;
`;
