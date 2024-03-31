import { Api, SharedTypes } from '@shared';

export type EditableModule = SharedTypes.Clone<Api.Services.FindOrderedModulesResponse>;

export type ModuleAllData = SharedTypes.Clone<Api.Services.ModuleAllDataResponse>;
