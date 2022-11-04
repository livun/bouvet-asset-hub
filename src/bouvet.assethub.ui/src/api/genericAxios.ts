import axios from 'axios';
import apiClient from '../config/apiClient';


export const regularHeaders = {
    'Content-Type': 'application/json',
    Accept: 'application/json',
}

export const formHeaders = {
    'Content-Type': 'multipart/formdata',
}
export async function get<TResponse>(url: string, headers?: any) {
    const response = await apiClient.get<TResponse>(
        url, 
        { headers: headers},
    );
    return response.data
}

export async function postItem<TRequest, TResponse>(url: string, body: TRequest, headers: any) {
    const content = JSON.stringify(body);
    const response = await apiClient.post<TResponse>(
        url, 
        content, 
        { headers: headers},
    );
    return response.data
}

export async function putItem<TRequest, TResponse>(url: string, body: TRequest, headers: any) {
    const content = JSON.stringify(body);
    console.log(content)
    const response = await apiClient.put<TResponse>(
        url, 
        content, 
        { headers: headers},
    );
    return response.data
}

// export async function putItem<TRequest, TResponse>(url: string, body: TRequest, headers: any) {
//     const content = JSON.stringify(body);
//     console.log(content)
//     const response = await apiClient<TResponse>( {
//         method: "PUT",
//         url: url,
//         body: content,
//         config: { headers: {
//             'Content-Type': 'application/json',
//             'Accept': 'application/json'
//         }},
//     );
//     return response.data
// }

export async function deleteItem<TResponse>(url: string, headers: any) {
    const response = await apiClient.delete<TResponse>(
        url, 
        { headers: headers},
    );
    return response.data
}