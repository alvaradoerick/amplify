export default {
    getErrorResponse(state) {
        return state.errorResponse;
    },
    
    getAgreement(state) {
        return state.agreements ;
    },

    filteredAgreements(state){
        if(state.activeCategory === null || state.activeCategory ===0){
            return state.agreements
        }
        return state.agreements.filter(agreement => agreement.CategoryAgreementId === state.activeCategory.id)
    }

}