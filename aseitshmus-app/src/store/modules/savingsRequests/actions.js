import api from '../../../api/AxiosInterceptors.js';

export default {

    //Get
    async getAllSavings({
        commit
    }) {
        const response = await api.get(`/savingsrequest`);   
        const savingsData = response.data;
        commit('setSavings', savingsData);
        return savingsData;
    },

    async getSavingsById({
        commit
    },payload) {
        const savingsId = payload.rowId
        const response = await api.get(`/savingsrequest/${savingsId}`);   
        const savingsData = response.data;
        commit('setSavings', savingsData);
        return savingsData;
    },

    //Post
    async addSavingsRequest({
        rootGetters
    }, payload) {
        const userId = rootGetters['auth/getLoggedInUser'];
        const response = await api.post(`/savingsrequest/${userId}`, payload.savingsData);   
            return response;
    },
    
    //Delete
    async deleteSavings({
        commit
    }, payload) {
        try {
            const savingsRequestId = payload.rowId;
            const response = await api.delete(`/savingsrequest/${savingsRequestId}`);   
            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

     //Put or Patch
     async updateSavings({
        _
    }, payload) {
        try {
            console.log(_)
            const savingsRequestId = payload.savingsRequestId;
            const savings = payload.savingsState;
            const response = await api.patch(`/savingsrequest/${savingsRequestId}`, savings);   
            return response
        } catch (error) {
            console.log(error)
        }
    },

};