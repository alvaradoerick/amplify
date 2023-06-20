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
        commit('setCategory', categoryData);
        return categoryData;
    },

    async addCategory({
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
        commit('setCategory', categoryData);
        return categoryData;
    },

    async updateCategory({
        rootGetters
    }, payload) {
        try {
            const categoryId = payload.categoryId;
            const agreementCategory = payload.agreementCategory;
            const token = rootGetters['auth/getToken'];
            const response = await axios.put(
                `${apiUrl}/categoryagreement/${categoryId}`,
                agreementCategory, {
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