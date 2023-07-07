export default {
    getErrorResponse(state) {
        return state.errorResponse;
    },
    
    getLoans(state) {
        return state.loans ;
    },

    getLoanCalculation(state) {
        return [state.loanCalculation];
    },

}