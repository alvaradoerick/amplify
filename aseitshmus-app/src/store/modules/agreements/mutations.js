export default {
    setErrorResponse(state, errorResponse) {
        state.errorResponse = errorResponse;
    },

    clearErrorResponse(state) {
        state.errorResponse = null;
    },

    setCategories(state, categories) {
        state.categories = categories;
    },

    setAgreements(state, agreements) {
        state.agreements = agreements;
    },
}