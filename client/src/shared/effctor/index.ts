import { Effect, createStore } from 'effector';

export const restoreFail = <Fail>(effect: Effect<any, any, Fail>, defaultState: Fail) => {
    return createStore(defaultState).on(effect.failData, (_, fail) => fail);
};
