import store from '@/store'

export default {
  //表格调整
  resize() {
    store.dispatch('table/resize');
  }
}
