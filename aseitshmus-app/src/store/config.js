import auth from "./modules/auth";
import users from "./modules/users";
import agreements from "./modules/agreements";

export default {
    namespaced: true,
    modules:
    {
        auth,
        users,
        agreements
    },

}
