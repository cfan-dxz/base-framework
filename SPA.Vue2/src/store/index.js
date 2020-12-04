import Vue from 'vue';
import Vuex from 'vuex';

//module
import layout from './module/layout'
import user from './module/user'
import table from './module/table'

//oidc
import oidcStore from '@/oidc/store'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    //
  },
  mutations: {
    //
  },
  actions: {
    //
  },
  modules: {
    layout,
    user,
    table,
    oidcStore,
  }
})
