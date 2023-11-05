import { post } from '../Utility/RequestMaker';

export async function requestDownload(url) {
    const response = await post("download", {url: url.split("?")[0]});

    return response;
}