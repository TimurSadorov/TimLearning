import React, { useCallback, useEffect, useState } from 'react';
import AceEditor from 'react-ace';
import './css/lesson-editing.css';
import 'ace-builds/src-noconflict/mode-csharp';
import 'ace-builds/src-noconflict/theme-monokai';
import 'ace-builds/src-noconflict/theme-tomorrow';
import 'ace-builds/src-noconflict/ext-language_tools';

export type CodeEditorTheme = 'light' | 'dark';

type Props = {
    value?: string;
    onChange?: (value?: string) => void;
    readonly?: boolean;
    theme?: CodeEditorTheme;
};

export const CodeEditor = ({ value, onChange, readonly, theme }: Props) => {
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

    return (
        <AceEditor
            className="code-editor"
            mode="csharp"
            theme={theme === 'light' ? 'tomorrow' : 'monokai'}
            height={`${rowCount * 18 + 2}px`}
            width="100%"
            fontSize="1.12em"
            value={codeValue}
            onChange={onChangeCode}
            enableLiveAutocompletion={true}
            enableBasicAutocompletion={true}
            setOptions={{
                foldStyle: 'manual',
                readOnly: readonly,
                highlightActiveLine: !readonly,
                highlightGutterLine: !readonly,
            }}
        />
    );
};
