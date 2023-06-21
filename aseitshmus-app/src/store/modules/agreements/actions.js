import axios from "axios";


const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    //Get
    async getAllAgreements({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/agreement`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const agreementData = response.data;
        commit('setAgreement', agreementData);
        return agreementData;
    },

    async getActiveAgreements({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/agreement/active-agreements`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const agreementData = response.data;
        commit('setAgreement', agreementData);
        return agreementData;
    },

    //Post
    async addAgreement({
        rootGetters
    }, payload) {
        const token = rootGetters['auth/getToken'];
            const response = await axios.post(
                `${apiUrl}/agreement`,
                payload.agreementData, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            return response;
    },

    
};