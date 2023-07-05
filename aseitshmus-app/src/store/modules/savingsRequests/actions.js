import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    //Get
    async getAllSavings({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/savingsrequest`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const savingsData = response.data;
        commit('setSavings', savingsData);
        return savingsData;
    },

    async getSavingsById({
        commit,
        rootGetters
    },payload) {
        const savingsId = payload.rowId;

        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/savingsrequest/${savingsId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const savingsData = response.data;
        commit('setSavings', savingsData);
        return savingsData;
    },

    //Post
    async addSavingsRequest({
        rootGetters
    }, payload) {
        const userId = rootGetters['auth/getLoggedInUser'];
        const token = rootGetters['auth/getToken'];
            const response = await axios.post(
                `${apiUrl}/savingsrequest/${userId}`,
                payload.savingsData, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            return response;
    },
    


    //Delete
    async deleteSavings({
        commit,rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const savingsRequestId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/savingsrequest/${savingsRequestId}`,{
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

     //Put or Patch
     async updateSavings({
        rootGetters
    }, payload) {
        try {
            const savingsRequestId = payload.savingsRequestId;
            const savings = payload.savingsState;
            const token = rootGetters['auth/getToken'];
            const response = await axios.patch(
                `${apiUrl}/savingsrequest/${savingsRequestId}`,
                savings, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            return response
        } catch (error) {
            console.log(error)
        }
    },

};