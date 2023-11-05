import { get, postMultipart } from '../Utility/RequestMaker';

export async function getUserInformation() {
    const response = await get("user/me");

    return response;
}

export async function updateUserInformation(data) {
    const response = await postMultipart("user/update", data);

    return response;
}