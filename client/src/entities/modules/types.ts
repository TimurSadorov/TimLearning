import { FindOrderedModulesResponse } from 'shared/api/generated';

export type EditableModule = Omit<FindOrderedModulesResponse, ''>;
