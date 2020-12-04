export default {
  namespaced: true,
  state: {
    item: null
  },
  mutations: {
    setItem(state, item) {
      state.item = item;
    }
  },
  getters: {
    getItem: state => state.item
  },
  actions: {
    //菜单点击
    menuItemClick(context, item) {
      context.commit('setItem', item);
    }
  }
}
