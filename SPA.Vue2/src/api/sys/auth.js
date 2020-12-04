import httpRequest from '@/lib/api.request'

const apiHost = process.env.ApiHost;

//用户登录
export const userLogin = {
  url: `${apiHost}/api/Auth/UserLogin`, //接口地址
  request(data) {
    return httpRequest({
      url: this.url,
      method: 'post',
      data
    })
  }
}

//获取当前用户
export const getCurrentUser = {
  url: `${apiHost}/api/Auth/GetCurrentUser`, //接口地址
  request() {
    return httpRequest({
      url: this.url,
      method: 'get'
    })
  }
}

//退出登录
export const userLogout = {
  url: `${apiHost}/api/Auth/UserLogout`, //接口地址
  request() {
    return httpRequest({
      url: this.url,
      method: 'get'
    })
  }
}
