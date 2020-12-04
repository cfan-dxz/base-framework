// import {
//   userLogin,
//   getCurrentUser
// } from "@/api/sys/auth"; //认证接口

export default {
  namespaced: true,
  state: {
    userInfo: null //用户信息
  },
  mutations: {
    setUser(state, info) {
      state.userInfo = info
    }
  },
  getters: {
    getUser: state => state.userInfo
  },
  actions: {
    // //登录
    // login(context, dto) {
    //   return new Promise((resolve, reject) => {
    //     try {
    //       userLogin
    //         .request(dto)
    //         .then(res => {
    //           if (res.success) {
    //             context.commit('setUser', res.data.user);
    //           }
    //           resolve(res);
    //         })
    //         .catch(err => reject(err));
    //     } catch (err) {
    //       reject(err);
    //     }
    //   });
    // },
    // //当前用户
    // currentUser(context) {
    //   return new Promise((resolve, reject) => {
    //     try {
    //       getCurrentUser.request().then(res => {
    //         const data = res.data;
    //         context.commit('setUser', data);
    //         resolve(data);
    //       }).catch(err => reject(err));
    //     } catch (err) {
    //       reject(err);
    //     }
    //   });
    // }
  }
}
