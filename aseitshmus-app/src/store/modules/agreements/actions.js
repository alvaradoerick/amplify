import axios from "axios";


const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {
    async addCategoryAgreement({
        rootGetters
    }, payload) {
        const token = rootGetters['auth/getToken'];
            const response = await axios.post(
                `${apiUrl}/categoryagreement`,
                payload.agreementCategory, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            return response;
    },

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