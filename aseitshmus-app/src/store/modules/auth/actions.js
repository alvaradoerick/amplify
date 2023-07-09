import axios from "axios";
import dayjs from 'dayjs';


const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {

    async register({
        dispatch,
        commit
    }, payload) {
        try {
            const personalInfo = payload.personalInfo;
            const workInfo = payload.workInfo;
            const addressInfo = payload.addressInfo;
            const beneficiaryInfo = payload.beneficiaryInfo;
            const response = await axios.post(
                `${apiUrl}/user`, {
                    ...personalInfo,
                    ...workInfo,
                    ...addressInfo,
                    DateBirth: dayjs(personalInfo.DateBirth).format('YYYY-MM-DD'),
                    WorkStartDate: dayjs(workInfo.WorkStartDate).format('YYYY-MM-DD')
                }
            )
            const PersonId = response.data.PersonId
            await dispatch('beneficiaries/addBeneficiaries', {
                PersonId,
                beneficiaryInfo
            },{ root: true });

            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

    async login({
        commit, dispatch
    }, payload) {
        try {
            const response = await axios.post(`${apiUrl}/login/authenticate`, payload.formData);
            localStorage.setItem('token', response.data.Token);
            localStorage.setItem('loggedInUser', response.data.PersonId);
            localStorage.setItem('role', response.data.RoleId);
            await  dispatch('fetchLoginData');
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

    async logout({
        commit, dispatch
    }) {
        try {
            localStorage.removeItem('token');
            localStorage.removeItem('loggedInUser');
            localStorage.removeItem('role');
            await  dispatch('fetchLoginData');
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

    async fetchLoginData({ commit }) {
        return new Promise((resolve) => {
            commit('setToken', localStorage.getItem('token'));
            commit('setLoggedInUser', localStorage.getItem('loggedInUser'));
            commit('setRole', localStorage.getItem('role'));
            resolve();
          });
      },

    async resetPasswordUnauthenticated({
        commit,
    }, payload) {
        try {
            const resetData = payload.resetData;
            const response = await axios.patch(
                `${apiUrl}/password/resetPassword`,
                resetData
            )

            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },

};