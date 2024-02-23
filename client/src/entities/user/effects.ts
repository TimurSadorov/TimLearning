const loadUserFx = createEffect(() => {
    const token = getToken();
    const userInfo = token ? jwtDecode<{ nameid: string }>(token) : null;

    return {
        isAuthenticated: !!userInfo,
        username: userInfo?.nameid ?? '',
        isAdmin: userInfo?.nameid?.startsWith('admin') ?? false,
    };
});

const authFx = createEffect(async (data: { username: string }) => {
    const resp = await auth(data);
    const { jwt } = resp.data;

    setToken(jwt);
});