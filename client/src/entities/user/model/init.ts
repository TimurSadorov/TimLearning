import { sample } from 'effector';

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
