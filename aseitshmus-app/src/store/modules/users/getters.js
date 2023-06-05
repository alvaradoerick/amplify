export default {
     getUsers(state) {
        return state.users;
    },
    find(state) {
        return id => {
            return state.users.find(user => user.id === id);
        }
    }
}