import { resolve } from "path";
import { useEffect, useState } from "react";

export type ApiResponse<T> = {
    status: Number;
    statusText: String;
    data: T | undefined;
    error: any;
    loading: Boolean;
}

export const baseUrl = "https://localhost:3001/api"

export function useGetApi<TResponse>(url: string): ApiResponse<TResponse> {
    const [status, setStatus] = useState<Number>(0);
    const [statusText, setStatusText] = useState<String>('');
    const [data, setData] = useState<any>();
    const [error, setError] = useState<any>();
    const [loading, setLoading] = useState<boolean>(false);

    async function fetchApi() {
        try {
            const apiResponse = await fetch(baseUrl + url);
            const json = await apiResponse.json();
            setStatus(apiResponse.status);
            setStatusText(apiResponse.statusText);
            setData(json);
        } catch (error) {
            setError(error);
        }
        setLoading(false);
    }

    useEffect(() => {
        setLoading(true);
        fetchApi();
    }, []);

    return { status, statusText, data, error, loading };
}

export function useApi<TRequest, TResponse>(url: string, body: TRequest, methodType: "POST" | "PUT"): ApiResponse<TResponse> {
    console.log("1putting now")

    const [status, setStatus] = useState<Number>(0);
    const [statusText, setStatusText] = useState<String>('');
    const [data, setData] = useState<any>();
    const [error, setError] = useState<any>();
    const [loading, setLoading] = useState<boolean>(false);
    const content = JSON.stringify(body);
    
    async function fetchApi() {
        try {
            console.log("fetching noew")
            const apiResponse = await fetch(baseUrl + url , {
                body: content,
                method: methodType,
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "text/plain"
                }
            });
            const json = await apiResponse.json();
            setStatus(apiResponse.status);
            setStatusText(apiResponse.statusText);
            setData(json);

        } catch (error){
            setError(error)
        }
        setLoading(false)
        
    }
    //fetchApi();
    useEffect(() => {
        console.log("putting now")
        setLoading(true);
        fetchApi();
    }, [body]);
    
 return { status, statusText, data, error, loading };
};

// export function putApi<TRequest, TResponse>(url: string, body: TRequest, methodType: "POST" | "PUT"): ApiResponse<TResponse> {
//     console.log("1putting now")

//     const [status, setStatus] = useState<Number>(0);
//     const [statusText, setStatusText] = useState<String>('');
//     const [data, setData] = useState<any>();
//     const [error, setError] = useState<any>();
//     const [loading, setLoading] = useState<boolean>(false);
//     const content = JSON.stringify(body);
    
//     async function fetchApi() {
//         try {
//             console.log("fetching noew")
//             const apiResponse = await fetch(baseUrl + url , {
//                 body: content,
//                 method: methodType,
//                 headers: {
//                     "Content-Type": "application/json",
//                     "Accept": "text/plain"
//                 }
//             });
//             const json = await apiResponse.json();
//             setStatus(apiResponse.status);
//             setStatusText(apiResponse.statusText);
//             setData(json);

//         } catch (error){
//             setError(error)
//         }
//         setLoading(false)
        
//     }
//     fetchApi();
//     // useEffect(() => {
//     //     console.log("putting now")
//     //     setLoading(true);
//     //      fetchApi();
//     // }, []);
    
//  return { status, statusText, data, error, loading };
// };


export function useDeleteApi<TResponse>(url: string): ApiResponse<TResponse> {
    const [status, setStatus] = useState<Number>(0);
    const [statusText, setStatusText] = useState<String>('');
    const [data, setData] = useState<any>();
    const [error, setError] = useState<any>();
    const [loading, setLoading] = useState<boolean>(false);
    
    async function fetchApi() {
        try {
            const apiResponse = await fetch(baseUrl + url , {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "text/plain"
                }
            });
            const json = await apiResponse.json();
            setStatus(apiResponse.status);
            setStatusText(apiResponse.statusText);
            setData(json);

        } catch (error){
            setError(error)
        }
        setLoading(false)
        
    }
    useEffect(() => {
        setLoading(true);
        fetchApi();
    }, []);
    
 return { status, statusText, data, error, loading };
};