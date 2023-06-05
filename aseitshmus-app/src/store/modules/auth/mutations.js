export default {
    setToken(state, token) {
        state.token = token;
    },

    setLoggedInUser(state, loggedInUser) {
        state.loggedInUser = loggedInUser;
    },

    setRole(state, role) {
        state.role = role;
    },

    setErrorResponse(state, password) {
        state.password = password;
    },

    setPassword(state, errorResponse) {
        state.errorResponse = errorResponse;
    },

    clearToken(state) {
        state.token = null;
    },
}