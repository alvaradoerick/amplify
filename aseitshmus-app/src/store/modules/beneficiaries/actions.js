import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {

    async addBeneficiaries({
        commit
    }, payload) {
        try {
            const response = await axios.post(
                `${apiUrl}/users/${payload.PersonId}/beneficiary`,
                payload.beneficiaryInfo
            )
            return response;
        } catch (error) {
            const errorMessage = error.response.data.error;
            commit('setErrorResponse', errorMessage);
        }
    },
    async getBeneficiaries({
        commit,
        rootGetters
    },payload) {
        const token = rootGetters['auth/getToken'];
        const PersonId = payload.PersonId;
        const response = await axios.get(`${apiUrl}/users/${PersonId}/beneficiary`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        const beneficiariesData = response.data;
        commit('setBeneficiaries', beneficiariesData);
        return beneficiariesData;
    },

    async deleteBeneficiaries({
        commit,
        rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const PersonId = payload.PersonId;
            const response = await axios.delete(
                `${apiUrl}/users/${PersonId}/beneficiary`, {
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

    async deleteAndInsertBeneficiaries({ dispatch, commit }, payload) {
        try {
          await dispatch('deleteBeneficiaries', { PersonId: payload.PersonId });     
          const insertPayload = {
            PersonId: payload.PersonId,
            beneficiaryInfo: payload.beneficiaryInfo,
          };
          const response = await dispatch('addBeneficiaries', insertPayload);
          return response;
        } catch (error) {
          const errorMessage = error.response.data.error;
          commit('setErrorResponse', errorMessage);
        }
      },
      
}