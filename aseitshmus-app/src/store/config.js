import auth from "./modules/auth";
import users from "./modules/users";
import agreements from "./modules/agreements";
import beneficiaries from "./modules/beneficiaries";


export default {
    namespaced: true,
    modules:
    {
        auth,
        users,
        agreements,
        beneficiaries,

    },

}
