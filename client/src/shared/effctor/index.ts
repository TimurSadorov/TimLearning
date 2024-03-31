import { Effect, createStore } from 'effector';

export const restoreFail = <Fail>(defaultState: Fail, ...effect: Effect<any, any, Fail>[]) => {
    return createStore(defaultState).on(
        effect.map((e) => e.failData),
        (_, fail) => fail,
    );
};
