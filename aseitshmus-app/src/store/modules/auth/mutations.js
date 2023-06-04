export default {
    setToken(state, token) {
        state.token = token;
    },

    setLoggedInUser(state, loggedInUser) {
        state.user = loggedInUser;
    },

    setRole(state, role) {
        state.role = role;
    },

    setErrorResponse(state, errorResponse) {
        state.errorResponse = errorResponse;
    },

    clearToken(state) {
        state.token = null;
    },
}