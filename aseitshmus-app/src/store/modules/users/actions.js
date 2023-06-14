import axios from "axios";
const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {

    async getAll({
        commit,rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/user`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const users = response.data;
        commit("setUsers", users);
        return response;
    },
    async getUserById({
        commit,
        rootGetters
    },payload) {
        const userId = payload.rowId;
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/user/${userId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const userData = response.data;
        commit('setUsers', userData);
        return userData;
    },

    async getById({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const userId = rootGetters['auth/getLoggedInUser'];
        const response = await axios.get(`${apiUrl}/user/${userId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const userData = response.data;
        commit('setUsers', userData);
        return userData;
    },

    async deleteUser({
        commit,rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const userId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/user/${userId}`,{
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            console.log()
            commit('setErrorResponse', errorMessage);
        }
    },

    //     async find({ _ }) { 
    //     return axios.get(apiUrl + "/api/v1/users/" + _)
    // }

    //     async update({ _ }) { 
    //     return axios.get(apiUrl + "/api/v1/users/" + _)
    // }

    async patchProfile({
        rootGetters
    }, payload) {
        try {
            const personalInfo = payload.personalInfo;
            const token = rootGetters['auth/getToken'];
            const userId = rootGetters['auth/getLoggedInUser'];
            const response = await axios.patch(
                `${apiUrl}/user/${userId}`,
                personalInfo, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            return response
        } catch (error) {
            console.log(error)
        }
    }
}