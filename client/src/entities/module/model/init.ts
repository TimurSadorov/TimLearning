import { sample } from 'effector';
import { EditableModulesGate, ModuleAllDataGate } from './model';
import {
    changeModuleOrderFx,
    createModuleFx,
    deleteModuleFx,
    findOrderedModulesFx,
    getModuleAllDataFx,
    restoreModuleFx,
    updateModuleFx,
} from './effects';

sample({
    clock: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: createModuleFx.done,
    source: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: changeModuleOrderFx.done,
    source: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: updateModuleFx.done,
    source: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: deleteModuleFx.done,
    source: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: restoreModuleFx.done,
    source: EditableModulesGate.state,
    target: findOrderedModulesFx,
});

sample({
    clock: ModuleAllDataGate.open,
    target: getModuleAllDataFx,
});
