import { get } from '../Utility/RequestMaker';

export async function getNestCount() {
    const response = await get("playlist/count");

    return response;
}

export async function getAllNests() {
    const response = await get("playlist/all");

    return response;
}

export async function getNest(guid) {
    const response = await get(`playlist/single/${guid}`);

    return response;
}