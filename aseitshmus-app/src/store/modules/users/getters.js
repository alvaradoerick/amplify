export default {
     getUsers(state) {
        return state.users;
    },
    getUser(state) {
        return state.user;
    },
    find(state) {
        return id => {
            return state.users.find(user => user.id === id);
        }
    }
}