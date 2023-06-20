import axios from "axios";


const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

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
};