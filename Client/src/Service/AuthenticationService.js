import { post } from '../Utility/RequestMaker';

export async function register(email, password) {
    const response = await post("account/register", {
        email: email, password: password
    });

    return response;
}

export async function login(email, password) {
    const response = await post("account/authenticate", {
        email, password
    });

    return response;
}