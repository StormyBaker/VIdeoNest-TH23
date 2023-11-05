import { get } from '../Utility/RequestMaker';

export async function getVideoCounts() {
    const response = await get("video/counts");

    return response;
}