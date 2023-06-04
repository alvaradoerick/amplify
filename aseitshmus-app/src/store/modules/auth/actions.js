import axios from "axios";
import dayjs from 'dayjs';


const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {


    async register({
        dispatch,
    }, payload) {
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
                WorkStartDate: dayjs(workInfo.WorkStartDate).format('YYYY-MM-DD'), //IdNumber: personalInfo.IdNumber.replaceAll('-', ''),
            }
        )
        const userId = response.data.PersonId
        console.log(userId)
        await dispatch('addBeneficiaries', {
            userId,
            beneficiaryInfo
        });

        return response;
    },

    async addBeneficiaries({
        _
    }, payload) {
        console.log(_)
        const response = await axios.post(
            `${apiUrl}/users/${payload.userId}/beneficiary`,
            payload.beneficiaryInfo
        )
        return response;
    },

    async login({ commit}, payload) {
        try {
            const response = await axios.post(`${apiUrl}/login/authenticate`, payload.formData);
            const token = response.data.Token;
            const user = response.data.PersonId;
            const role = response.data.RoleId;
            commit('setToken', token);
            commit('setLoggedInUser', user);
            commit('setRole', role);
        } catch (error) {
            console.log(error);
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },
   
};