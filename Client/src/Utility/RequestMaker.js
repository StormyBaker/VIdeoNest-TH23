import axios from 'axios';
import { API_BASE_URL } from '../App';

export async function postMultipart(endpoint, data) {
    var response = await axios.post(`${API_BASE_URL}/${endpoint}`, data, {
        withCredentials: true,
        validateStatus: function () {
            return true;
        },
        headers: {
            "Content-Type": "multipart/form-data",
        }
    });

    if (response.status === 403) {
        return 403;
    }

    return response;
}

export async function post(endpoint, data) {
    var response = await axios.post(`${API_BASE_URL}/${endpoint}`, data, {
        withCredentials: true,
        validateStatus: function () {
            return true;
        }
    });

    if (response.status === 403) {
        return 403;
    }

    return response;
}

export async function get(endpoint) {
    var response = await axios.get(`${API_BASE_URL}/${endpoint}`, {
        withCredentials: true,
        validateStatus: function () {
            return true;
        }
    });

    if (response.status === 403) {
        return 403;
    }

    return response;
}