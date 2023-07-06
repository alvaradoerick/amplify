import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]

export default {

    //Get

    async getAllLoans({
        commit,
        rootGetters
    }) {
        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/loanrequest`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const loanData = response.data;
        commit('setLoan', loanData);
        return loanData;
    },

    async getLoanById({
        commit,
        rootGetters
    }, payload) {
        const loanId = payload.rowId;

        const token = rootGetters['auth/getToken'];
        const response = await axios.get(`${apiUrl}/loanrequest/${loanId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        const loanData = response.data;
        commit('setLoan', loanData);
        return loanData;
    },

    //Post
    async getLoanCalculation({
        commit,
        rootGetters
    }, payload) {
        const token = rootGetters['auth/getToken'];
        const PersonId = rootGetters['auth/getLoggedInUser'];
        const loanData = payload.loanData;
        const updatedLoanData = {
            ...loanData,
            PersonId: PersonId,
        };

        const response = await axios.post(`${apiUrl}/loanrequest/calculation`,
            updatedLoanData, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        const loanDataResponse = response.data;
        commit('setLoanCalculation', loanDataResponse);
        return loanDataResponse;
    },

    async addLoanRequest({
        rootGetters
    }, payload) {
        const userId = rootGetters['auth/getLoggedInUser'];
        const token = rootGetters['auth/getToken'];
        const response = await axios.post(
            `${apiUrl}/loanrequest/${userId}`,
            payload.loanRequest, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        return response;
    },

    //Delete
    async deleteLoan({
        commit,
        rootGetters
    }, payload) {
        try {
            const token = rootGetters['auth/getToken'];
            const loanRequestId = payload.rowId;
            const response = await axios.delete(
                `${apiUrl}/loanrequest/${loanRequestId}`, {
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
    async updateLoan({
        rootGetters
    }, payload) {
        try {
            const loanRequestId = payload.loanRequestId;
            const loan = payload.loanState;
            const token = rootGetters['auth/getToken'];
            const response = await axios.patch(
                `${apiUrl}/loanrequest/${loanRequestId}`,
                loan, {
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
};