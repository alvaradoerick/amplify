import axios from "axios";


const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    async getAllCategories({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/categoryagreement`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const categoryData = response.data;
        commit('setCategories', categoryData);
        return categoryData;
    },

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

    async deleteCategory({
        commit,
    }, payload) {
        try {
            const categoryId = payload.rowId;
            console.log(payload)
            console.log(payload.rowId)
            const response = await axios.delete(
                `${apiUrl}/categoryagreement/${categoryId}`
            );
            return response;
        } catch (error) {
            console.lo
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
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