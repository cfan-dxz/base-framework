<template>
  <el-submenu :index="item.id.toString()">
    <template slot="title">
      <i :class="item.icon" v-if="item.icon"></i>
      <span>{{item.title}}</span>
    </template>
    <template v-for="node in item.children">
      <MenuGroup v-if="node.children&&node.children.length>0" :item="node" :key="node.id"></MenuGroup>
      <el-menu-item
        v-else
        :key="node.id"
        :index="node.path"
        @click="onItemClick(node)"
      >{{node.title}}</el-menu-item>
    </template>
  </el-submenu>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "MenuGroup",
  props: {
    item: { type: Object, default: null }
  },
  data() {
    return {};
  },
  methods: {
    onItemClick(item) {
      this.menuItemClick(item);
    },
    ...mapActions("layout", ["menuItemClick"])
  }
};
</script>
