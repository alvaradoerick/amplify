import axios from "axios";
const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {

    async getAll({
        commit
    }) {
        const response = await axios.get(`${apiUrl}/user`);
        const users = response.data;
        commit("setUsers", users);
        return response;
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
        commit('setUser', userData);
        return userData;
    },

    //     async delete({ _ }) { 
    //     return axios.get(apiUrl + "/api/v1/users/" + _)
    // }

    //     async find({ _ }) { 
    //     return axios.get(apiUrl + "/api/v1/users/" + _)
    // }

    //     async update({ _ }) { 
    //     return axios.get(apiUrl + "/api/v1/users/" + _)
    // }
    
    async patchProfile({ _ }, payload) {
            const personalInfo = payload.personalInfo;          
            const response = await axios.fetch(
                `${apiUrl}/user`, {
                ...personalInfo,
                ...workInfo,
                ...addressInfo,
                DateBirth: dayjs(personalInfo.DateBirth).format('YYYY-MM-DD'),
                WorkStartDate: dayjs(workInfo.WorkStartDate).format('YYYY-MM-DD'), //IdNumber: personalInfo.IdNumber.replaceAll('-', ''),
            }
            )
           return response;

    }
     

}