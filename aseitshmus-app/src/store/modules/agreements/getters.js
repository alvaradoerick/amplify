export default {
    getErrorResponse(state) {
        return state.errorResponse;
    },
    
    getAgreement(state) {
        return state.agreements ;
    },

    filteredAgreements(state){
        console.log(state.activeCategory.id)
        if(state.activeCategory.id === null || state.activeCategory.id ===0){
            return state.agreements
        }
        return state.agreements.filter(agreement => {
            
            return agreement.CategoryAgreementId === state.activeCategory?.id
        })
    }

}