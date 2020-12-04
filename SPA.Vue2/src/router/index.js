import Vue from 'vue'
import store from '@/store'
import Router from 'vue-router'
Vue.use(Router)

// import token from "@/lib/auth.token"; //token
import admin from "@/config/admin"; //admin

//oidc
import oidcRouter from '@/oidc/router';

//module
import demo from './module/demo';

//解决重复导航报错问题
//push
const routerPush = Router.prototype.push
Router.prototype.push = function push(to) {
  return routerPush.call(this, to).catch(err => err)
}
//replace
const routerReplace = Router.prototype.replace
Router.prototype.replace = function replace(to) {
  return routerReplace.call(this, to).catch(err => err)
}

//login
// const LOGIN_PAGE_NAME = 'Login'

const router = new Router({
  mode: 'history', //history模式
  routes: [{ //error
      path: '*',
      name: 'Error',
      redirect: '/error'
    },
    // { //login
    //   path: '/login',
    //   name: LOGIN_PAGE_NAME,
    //   meta: {
    //     title: '登录'
    //   },
    //   component: () => import('@/components/layout/Login')
    // },
    ...oidcRouter.routes, //oidc
    { //layout
      path: '/',
      name: 'Index',
      title: 'Bestwo',
      component: () => import('@/components/layout/Index'),
      children: [{ //error
          path: '/error',
          name: 'Error',
          component: () => import('@/components/layout/Error')
        }, { //首页
          path: '/',
          name: 'Home',
          component: () => import('@/view/home/Index')
        },
        ...demo, //demo
      ]
    }
  ]
})

router.beforeEach((to, from, next) => {
  //标题
  if (to.meta.title) {
    document.title = to.meta.title;
  } else {
    document.title = admin.name;
  }

  ////oldauth
  // if (!token.existsToken() && to.name !== LOGIN_PAGE_NAME) {
  //   //未登录且要跳转的页面不是登录页
  //   next({
  //     name: LOGIN_PAGE_NAME, //跳转到登录页
  //     query: {
  //       redirect: to.fullPath
  //     }
  //   })
  // } else {
  //   next();
  // }

  //oidc
  oidcRouter.handle(to.name, to.fullPath);

  next();
});

router.afterEach(route => {
  // if (route.name !== LOGIN_PAGE_NAME) {
  //   //当前用户
  //   store.dispatch('user/currentUser');
  // }
});

export default router
