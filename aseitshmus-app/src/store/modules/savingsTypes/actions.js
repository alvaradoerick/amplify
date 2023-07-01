import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    async getAllTypes({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/SavingsType`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const typeData = response.data;
        commit('setType', typeData);
        return typeData;
    },

    async getActiveTypes({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/SavingsType/active-categories`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const typeData = response.data;
        commit('setType', typeData);
        return typeData;
    },

    async getTypeById({
        commit,
        rootGetters
    }, payload) {
        const typeId = payload.rowId;
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/SavingsType/${typeId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const typeData = response.data;
        commit('setType', typeData);
        return typeData;
    },

    //Post
    async addType({
        rootGetters
    }, payload) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.post(
            `${apiUrl}/SavingsType`,
            payload.savingsType, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        return response;
    },

    //Delete
    async deleteType({
        commit,
        rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const typeId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/SavingsType/${typeId}`, {
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
    async updateType({
        rootGetters,
        commit
    }, payload) {
        try {
            const typeId = payload.typeId;
            const savingsType = payload.savingsType;
            const token = rootGetters['auth/getToken'];
            const response = await axios.put(
                `${apiUrl}/SavingsType/${typeId}`,
                savingsType, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            return response
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },
};