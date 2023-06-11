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
        commit,rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const categoryId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/categoryagreement/${categoryId}`,{
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


    async getCategoryById({
        commit,
        rootGetters
    },payload) {
        const categoryId = payload.rowId;
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/categoryagreement/${categoryId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const categoryData = response.data;
        commit('setCategories', categoryData);
        return categoryData;
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