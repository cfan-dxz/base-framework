import cookie from "@/lib/ext.cookie"; //cookie
import admin from "@/config/admin"; //admin

const host = function () {
  //设置主域cookie
  let _host = document.domain,
    _main = admin.domain;
  if (_host.indexOf(_main) != -1) {
    _host = _main;
  }
  return _host;
}

export default {
  setToken: function (token, expireSeconds) {
    cookie.set({
      key: admin.tokenName,
      value: token,
      expires: expireSeconds,
      domain: host()
    });
  },
  getToken: function () {
    const token = cookie.get(admin.tokenName);
    return token;
  },
  removeToken: function () {
    cookie.set({
      key: admin.tokenName,
      value: 'none',
      expires: -1,
      domain: host()
    });
  },
  existsToken: function () {
    const token = this.getToken();
    if (!token) {
      return false;
    }
    return true;
  }
}
