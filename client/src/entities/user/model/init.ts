import { sample } from 'effector';
import { UserGate } from './model';
import { loadUserFx } from './effects';

sample({
    clock: UserGate.open,
    target: loadUserFx,
});

// sample({
//     clock: chatModel.connection.events.newUserJoinedToRoom,
//     source: $user,
//     filter: (user) => user.isAdmin,
//     target: chatModel.messages.events.loadMessages,
// });

// sample({
//     clock: chatModel.connection.events.interlocutorLeftFromRoom,
//     source: $user,
//     filter: (user) => user.isAdmin,
//     target: chatModel.messages.events.clearMessage,
// });
