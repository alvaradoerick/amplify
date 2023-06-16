export default {
     getUsers(state) {
        return state.users;
    },
    getBeneficiaries(state) {
        return state.beneficiaries;
    },
    // find(state) {
    //     return id => {
    //         return state.users.find(user => user.id === id);
    //     }
    // },
    getErrorResponse(state) {
        return state.errorResponse;
    },
}