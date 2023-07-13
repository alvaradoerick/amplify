import  axios  from 'axios';
// import {
//   useRouter
// } from 'vue-router';

// const router = useRouter()

const API_URL = process.env["VUE_APP_BASED_URL"];

const api = axios.create({
  baseURL: API_URL
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);

// api.interceptors.response.use(
//   response => {
//     return response;
//   },
//   error => {
//     if (error.response.status === 401) {
//       const originalRequest = error.config;
//       const wwwAuthenticate = error.response.headers['WWW-Authenticate'];
//       console.log(wwwAuthenticate);
//       console.log(error);
//       if (
//         wwwAuthenticate &&
//         wwwAuthenticate.includes('Bearer realm="Your Realm", error="invalid_token", error_description="The token has expired"')
//       ) {
//         originalRequest._retry = true;
//         router.push({ name: 'login' });
//       }
//     }
//     return Promise.reject(error);
//   }
// );


export default api;