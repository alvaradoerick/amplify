import axios from "axios";


const apiUrl = process.env["VUE_APP_BASED_URL"]
export default {


    async addCategoryAgreement({
        _
    }, payload) {
            console.log(_)
            const response = await axios.post(
                `${apiUrl}/categoryagreement`,
                payload.agreementCategory
            )
            return response;
    },
};