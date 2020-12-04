import axios from 'axios'
// import router from '@/router';
import {
  MessageBox,
} from 'element-ui';
//nprogress
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

//获取axios实例
const getInstance = () => {
  const instance = axios.create();
  //请求拦截器
  instance.interceptors.request.use((config) => {
    NProgress.start();
    return config;
  }, (err) => {
    MessageBox({
      title: "接口请求错误",
      message: JSON.stringify(err),
      type: "error"
    });
    return Promise.reject(err);
  });
  //响应拦截器
  instance.interceptors.response.use((res) => {
    NProgress.done();
    return res.data;
  }, (err) => {
    NProgress.done();
    const res = err.response;
    let msg = res ? res.data.msg : err.message;
    MessageBox({
      title: "接口响应错误",
      message: msg,
      type: "error"
    });
    //如果是登录过期则跳转登录页
    // if (res && res.status === 401) {
    //   router.push({
    //     name: "Login"
    //   });
    // }
    return Promise.reject(err);
  });
  return instance;
}

//初始化
const httpRequest = (option) => {
  const instance = getInstance();
  return instance({
    baseURL: option.webHost || null,
    url: option.url,
    params: option.params || {},
    data: option.data || null,
    method: option.method || 'get',
    withCredentials: true
  });
}

export default httpRequest
