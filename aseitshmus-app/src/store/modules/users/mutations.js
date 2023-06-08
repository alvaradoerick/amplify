export default {
    setUsers(state, users) { 
        state.users = users;
    },
    setErrorResponse(state, errorResponse) {
        state.errorResponse = errorResponse;
    },

    clearErrorResponse(state) {
        state.errorResponse = null;
    },
}