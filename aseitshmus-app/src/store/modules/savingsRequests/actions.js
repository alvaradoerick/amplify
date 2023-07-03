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

    // async getActiveAgreements({
    //     commit,
    //     rootGetters
    // }) {
    //     const token = rootGetters['auth/getToken'];
    //     const response = await axios.get(`${apiUrl}/agreement/active-agreements`, {
    //         headers: {
    //             Authorization: `Bearer ${token}`
    //         }
    //     })
    //     const agreementData = response.data;
    //     commit('setAgreement', agreementData);
    //     return agreementData;
    // },

    // async getAgreementById({
    //     commit,
    //     rootGetters
    // },payload) {
    //     const agreementId = payload.rowId;

    //     const token = rootGetters['auth/getToken'];
    //     const response = await axios.get(`${apiUrl}/agreement/${agreementId}`, {
    //         headers: {
    //             Authorization: `Bearer ${token}`
    //         }
    //     })
    //     const agreementData = response.data;
    //     commit('setAgreement', agreementData);
    //     return agreementData;
    // },

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

    //  //Put or Patch
    //  async updateAgreement({
    //     rootGetters
    // }, payload) {
    //     try {
    //         const agreementId = payload.AgreementId;
    //         const agreement = payload.agreementData;
    //         const token = rootGetters['auth/getToken'];
    //         const response = await axios.put(
    //             `${apiUrl}/agreement/${agreementId}`,
    //             agreement, {
    //                 headers: {
    //                     Authorization: `Bearer ${token}`
    //                 }
    //             }
    //         );
    //         return response
    //     } catch (error) {
    //         console.log(error)
    //     }
    // },

};