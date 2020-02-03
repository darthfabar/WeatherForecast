import axios from 'axios'

export default() => {    
    return axios.create({
        baseURL: process.env.VUE_APP_ROOT_API,
        withCredentials: false,
        headers: {
            'Access-Control-Allow-Origin': '*',            
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
}