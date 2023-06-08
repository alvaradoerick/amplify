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

    setErrorResponse(state, errorResponse) {
        state.errorResponse = errorResponse;
    },

    clearErrorResponse(state) {
        state.errorResponse = null;
    },
    
    setPassword(state, password) {
        state.password = password;
    },

    clearToken(state) {
        state.token = null;
    },
}