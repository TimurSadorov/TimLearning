import { UploadOutlined } from '@ant-design/icons';
import { Api, SharedUI } from '@shared';
import { Button, Upload, UploadProps } from 'antd';
import React, { useMemo } from 'react';

type Props = {
    accept?: string;
    maxSizeMb?: number;
    validTypes?: string[];
    messageOnInvalidType?: string;
    onChange?: (value: string) => void;
    value?: string;
};

export const UploaderInput = ({ accept, maxSizeMb, validTypes, messageOnInvalidType, onChange, value }: Props) => {
    const uploaderConfig = useMemo<UploadProps>(
        () => ({
            accept: accept,
            beforeUpload: async (file) => {
                if (!!maxSizeMb && maxSizeMb < file.size / (1024 * 1024)) {
                    SharedUI.Model.Notification.notifyErrorFx(`Максимальный размер файла ${maxSizeMb}Мб!`);
                    return Upload.LIST_IGNORE;
                }
                if (!!validTypes && validTypes.length !== 0 && !validTypes.some((t) => t === file.type)) {
                    SharedUI.Model.Notification.notifyErrorFx(
                        messageOnInvalidType ?? `Допустимые форматы: ${validTypes.join(',')}!`,
                    );
                    return Upload.LIST_IGNORE;
                }

                // тут можно конкретно для упражнения загрузка файла, по можно действие в пропс
                const fileId = await Api.Services.StoredFileService.saveExerciseAppFile({
                    File: file,
                });
                onChange?.(fileId);

                return false;
            },
            onRemove: () => {
                onChange?.('');
            },
            maxCount: 1,
        }),
        [],
    );

    return (
        <>
            <Upload {...uploaderConfig} defaultFileList={!!value ? [{ uid: 'app-file', name: 'Файл приложения' }] : []}>
                <Button icon={<UploadOutlined />}>Загрузить файл</Button>
            </Upload>
        </>
    );
};
