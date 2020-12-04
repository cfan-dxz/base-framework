export default {
  namespaced: true,
  state: {
    resizeTag: 0
  },
  mutations: {
    changeTag(state) {
      state.resizeTag += 1;
    }
  },
  getters: {
    getTag: state => state.resizeTag
  },
  actions: {
    //调整
    resize(context) {
      context.commit('changeTag');
    }
  }
}
