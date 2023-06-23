import axios from "axios";
import dayjs from 'dayjs';

const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {

    async getAll({
        commit,
        rootGetters
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

    //gets data from the selected row. Admin purposes
    async getUserById({
        commit,
        rootGetters
    }, payload) {
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

    // gets the date for logged in user
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
        commit,
        rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const userId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/user/${userId}`, {
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

    //Method for user to update their own profile
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
    },

    // Method for admin to update user
    async patchUser({
        rootGetters
    }, payload) {
        try {
            const personId = payload.PersonId;
            const userInfo = payload.userInfo
            const token = rootGetters['auth/getToken'];

            userInfo.DateBirth = dayjs(userInfo.DateBirth).format('YYYY-MM-DD');
            userInfo.WorkStartDate = dayjs(userInfo.WorkStartDate).format('YYYY-MM-DD');
            userInfo.EnrollmentDate = dayjs(userInfo.EnrollmentDate).format('YYYY-MM-DD');

            console.log(payload.userInfo)
            const response = await axios.patch(
                `${apiUrl}/user/employee/${personId}`,
                userInfo, {
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

    // Activate or deactivate accounts
    async patchUserStatus({
        rootGetters,
        commit
    }, payload) {
        try {
            const personId = payload.PersonId;
            const token = rootGetters['auth/getToken'];
            const response = await axios.patch(
                `${apiUrl}/user/activateuser/${personId}`, {}, {
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

    // User Resets password authenticated
    async resetPasswordAuthenticated({
        commit,
        rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const userId = rootGetters['auth/getLoggedInUser'];
            const resetData = payload.resetData;
            const response = await axios.patch(
                `${apiUrl}/password/resetPassword/${userId}`,
                resetData, {
                    headers: {
                        Authorization: `Bearer ${token}`
        
                    }
                }
            )
            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },
}