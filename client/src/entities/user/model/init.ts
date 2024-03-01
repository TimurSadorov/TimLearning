import { sample } from 'effector';
import { loadUserFx } from './effects';
import { $isLoging } from './model';

sample({
    clock: loadUserFx.doneData,
    fn: () => true,
    target: $isLoging,
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
