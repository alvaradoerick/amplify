import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    //Get
    async getLoanCalculation({
        commit,
        rootGetters
    }, payload) {
        const token = rootGetters['auth/getToken'];
        const PersonId = rootGetters['auth/getLoggedInUser'];
        const loanData = payload.loanData;
        const updatedLoanData = {
            ...loanData,
            PersonId: PersonId,
          };

        const response = await axios.post(`${apiUrl}/loanrequest/calculation`,
        updatedLoanData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        }
    )
        const loanDataResponse = response.data;
        commit('setLoanCalculation', loanDataResponse);
        return loanDataResponse;
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
    async addLoanRequest({
        rootGetters
    }, payload) {
        const userId = rootGetters['auth/getLoggedInUser'];
        const token = rootGetters['auth/getToken'];
            const response = await axios.post(
                `${apiUrl}/loanrequest/${userId}`,
                payload.loanRequest, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            return response;
    },
    


    // //Delete
    // async deleteAgreement({
    //     commit,rootGetters
    // }, payload) {
    //     try {
    //         const token = rootGetters['auth/getToken'];
    //         const agreementId = payload.rowId;
    //         const response = await axios.delete(
    //             `${apiUrl}/agreement/${agreementId}`,{
    //                 headers: {
    //                     Authorization: `Bearer ${token}`
    //                 }
    //             }
    //         );
    //         return response;
    //     } catch (error) {
    //         const errorMessage = error.response.data.error;
    //         commit('setErrorResponse', errorMessage);
    //     }
    // },

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