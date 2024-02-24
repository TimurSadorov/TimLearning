import { createEffect, createStore, forward, sample } from 'effector';
import { useStore } from 'effector-react';
import jwtDecode from 'jwt-decode';
import { getToken, setToken } from 'shared/local-storage';
import { auth } from '../api/auth';
import { User } from '../types';





forward({
    from: authFx.doneData,
    to: loadUserFx,
});

forward({
    from: loadUserFx.doneData,
    to: $user,
});

sample({
    clock: chatModel.connection.events.newUserJoinedToRoom,
    source: $user,
    filter: (user) => user.isAdmin,
    target: chatModel.messages.events.loadMessages,
});

sample({
    clock: chatModel.connection.events.interlocutorLeftFromRoom,
    source: $user,
    filter: (user) => user.isAdmin,
    target: chatModel.messages.events.clearMessage,
});

export const useUser = () => useStore($user);
export const useAuthenticating = () => useStore(authFx.pending);

export const effects = {
    authFx,
    loadUserFx,
};
