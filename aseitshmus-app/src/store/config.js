import auth from "./modules/auth";
import users from "./modules/users";
import agreements from "./modules/agreements";
import beneficiaries from "./modules/beneficiaries";
import categories from "./modules/categories";
import loanTypes from "./modules/loanTypes";
import savingsTypes from "./modules/savingsTypes";

export default {
    namespaced: true,
    modules:
    {
        auth,
        users,
        agreements,
        beneficiaries,
        categories,
        loanTypes,
        savingsTypes,

    },

}
