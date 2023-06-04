export default {
    getToken(state) {
        return state.token;
    },

    isAuthenticated(state) {
        return state.token;
    },

    getLoggedInUser(state) {
        return state.user;
    },
    getLoginResponse(state) {
        return state.errorResponse;
    },  
    getRole(state) {
        return state.role;
    },

}