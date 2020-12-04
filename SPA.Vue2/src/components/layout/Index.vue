<template>
  <div class="layout-main">
    <div class="layout-header">
      <div class="layout-logo left--shadow" :class="{'left--collapse':isCollapse}">
        <!-- logo -->
        <a @click="toHome" :title="adminName">
          <strong>{{isCollapse?"BW":adminName}}</strong>
        </a>
      </div>
      <div class="layout-collapse this-active" @click="onCollapse" :title="isCollapse?'展开':'收起'">
        <i :class="{'el-icon-s-fold':!isCollapse,'el-icon-s-unfold':isCollapse}"></i>
      </div>
      <div class="layout-nav">
        <!-- 页头导航 -->
        <el-menu
          :default-active="menuIndex"
          mode="horizontal"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b"
          @select="onMenuSelect"
        >
          <el-menu-item
            v-for="item in menuList"
            :key="item.id"
            :index="item.id.toString()"
          >{{item.title}}</el-menu-item>
        </el-menu>
      </div>
      <div class="layout-tool">
        <!-- 工具栏 -->
        <el-avatar icon="el-icon-user-solid"></el-avatar>
        <el-dropdown @command="onToolCommand">
          <span class="user-label">{{oidcUser&&oidcUser.UserName}}</span>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item icon="el-icon-switch-button" command="logout">退出</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </div>
    </div>
    <div class="layout-body">
      <div class="layout-body-left left--shadow">
        <!-- 左边导航 -->
        <el-scrollbar style="height:100%;">
          <el-menu
            :default-active="thisPath"
            class="layout-body-menu"
            :collapse="isCollapse"
            unique-opened
            router
          >
            <template v-for="item in menuDetail">
              <MenuGroup v-if="item.children&&item.children.length>0" :item="item" :key="item.id"></MenuGroup>
              <el-menu-item
                v-else
                :key="item.id"
                :index="item.path"
                @click="menuToTab(item)"
              >{{item.title}}</el-menu-item>
            </template>
          </el-menu>
        </el-scrollbar>
      </div>
      <div class="layout-body-content">
        <!-- 内容 -->
        <div class="layout-body-tabs">
          <!-- tabs -->
          <el-tabs
            v-model="tabIndex"
            type="border-card"
            @tab-remove="onTabRemove"
            @tab-click="onChangeTab"
          >
            <el-tab-pane
              :key="item.id"
              v-for="(item) in tabList"
              :name="item.id.toString()"
              :closable="!item.fixed"
            >
              <span slot="label">
                <i :class="item.icon" v-if="item.icon"></i>
                {{item.title}}
              </span>
            </el-tab-pane>
          </el-tabs>
        </div>
        <div class="layout-body-view">
          <!-- 视图 -->
          <keep-alive :include="include">
            <router-view />
          </keep-alive>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import menu from "@/assets/data/menu.json";
import admin from "@/config/admin"; //admin
// import auth from "@/lib/auth.token"; //auth
// import { userLogout } from "@/api/sys/auth"; //用户接口
import { mapGetters, mapActions } from "vuex";
export default {
  components: {
    MenuGroup: () => import("./MenuGroup"),
  },
  data() {
    return {
      adminName: admin.name,
      isCollapse: false,
      menuList: menu,
      menuIndex: null,
      tabList: [
        {
          id: -1,
          title: "首页",
          path: "/",
          icon: "el-icon-s-home",
          fixed: true,
        },
      ],
      tabIndex: null,
      include: ["Home"],
    };
  },
  computed: {
    menuDetail() {
      const menu = this.menuList.find((m) => m.id == this.menuIndex);
      return menu && menu.children ? menu.children : [];
    },
    thisPath() {
      return this.$route.path;
    },
    ...mapGetters("layout", {
      menuItem: "getItem",
    }),
    // ...mapGetters("user", {
    //   user: "getUser"
    // })
    ...mapGetters("oidcStore", {
      oidcUser: "oidcUser",
    }),
  },
  methods: {
    //选择菜单
    onMenuSelect(key, keyPath) {
      this.activeMenu(key);
    },
    //折叠
    onCollapse() {
      this.isCollapse = !this.isCollapse;
    },
    //关闭tab
    onTabRemove(name) {
      const item = this.tabList.find((e) => e.id == name);
      if (item) {
        this.setIncludeByPath(item.path, false);
        this.tabList.splice(
          this.tabList.findIndex((e) => e.id == name),
          1
        );
        if (item.id == this.tabIndex) {
          const last = this.tabList[this.tabList.length - 1];
          this.activeTab(last.id);
          this.$router.push(last.path);
        }
      }
    },
    //切换tab
    onChangeTab(tab) {
      const item = this.tabList.find((e) => e.id == tab.name);
      if (item) {
        item.path && this.$router.push(item.path);
        this.activeMenu(item.rootId);
      }
    },
    //激活菜单
    activeMenu(index) {
      if (index) {
        this.menuIndex = index.toString();
      }
    },
    //激活tab
    activeTab(index) {
      if (index) {
        this.tabIndex = index.toString();
      }
    },
    //菜单添加到tab
    menuToTab(item) {
      if (item) {
        if (this.tabList.findIndex((e) => e.id === item.id) === -1) {
          this.tabList.push(item);
          this.setIncludeByPath(item.path, true);
        }
        this.activeTab(item.id);
        this.activeMenu(item.rootId);
      }
    },
    //设置组件缓存
    setIncludeByPath(path, add) {
      const index = this.$router.options.routes.find((e) => e.path === "/");
      if (index) {
        const arr = index.children;
        if (arr) {
          const r = arr.find((e) => e.path === path);
          if (r) {
            if (add && !this.include.includes(r.name)) {
              this.include.push(r.name);
            } else if (!add && this.include.includes(r.name)) {
              this.include.splice(this.include.indexOf(r.name), 1);
            }
          }
        }
      }
    },
    //查找菜单
    selectMenu(list, path) {
      for (let i = 0; i < list.length; i++) {
        const item = list[i];
        if (item.path === path) {
          this.menuToTab(item);
          break;
        } else if (item.children) {
          this.selectMenu(item.children, path);
        }
      }
    },
    //回到首页
    toHome() {
      const home = this.tabList[0];
      this.$router.push(home.path);
      this.activeTab(home.id);
    },
    //init
    init() {
      //默认选中第1项菜单
      if (this.menuList && this.menuList.length > 0) {
        this.menuIndex = this.menuList[0].id.toString();
      }
      this.activeTab(this.tabList[0].id);
      this.selectMenu(this.menuList, this.thisPath);
    },
    onToolCommand(command) {
      switch (command) {
        case "logout": {
          this.$confirm("确定要退出登录吗?", "注销", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning",
          }).then(() => {
            // userLogout.request().then((res) => {
            //   if (res.success) {
            //     auth.removeToken();
            //     this.$router.push("/login");
            //   } else {
            //     this.$Message.error(res.msg);
            //   }
            // });
            this.signOutOidc();
          });
          break;
        }
      }
    },
    autoCollapse() {
      //页面宽度小于1280px自动折叠
      const fullWidth = document.documentElement.clientWidth;
      this.isCollapse = fullWidth < 1280;
    },
    ...mapActions("oidcStore", [
      "authenticateOidc",
      "removeOidcUser",
      "signOutOidc",
    ]),
  },
  watch: {
    menuItem(val) {
      this.menuToTab(val);
    },
  },
  mounted() {
    this.init();
    this.autoCollapse();
    window.addEventListener("resize", () => this.autoCollapse());
  },
};
</script>

<style scoped>
.layout-main {
  --head-height: 60px;
  --left-width: 200px;
}
.left--collapse {
  min-width: 64px;
  width: 64px;
}
.left--shadow {
  box-shadow: 2px 0 6px rgba(0, 21, 41, 0.35);
}
.layout-header {
  display: flex;
  position: absolute;
  top: 0;
  left: 0;
  height: var(--head-height);
  width: 100%;
  background-color: #545c64;
}
.layout-collapse {
  width: 60px;
  line-height: var(--head-height);
  font-size: 20px;
  text-align: center;
  cursor: pointer;
  color: #ffffff;
}
.this-active:hover {
  color: #ffd04b;
}
.layout-nav {
  flex: auto;
  white-space: nowrap;
  overflow-y: hidden;
  overflow-x: auto;
}
.layout-nav >>> .el-menu-item {
  display: inline-block;
  float: none;
}
.layout-logo {
  line-height: var(--head-height);
  text-align: center;
  color: #ffffff;
  transition: all 0.3s ease-in-out;
}
.layout-logo a {
  cursor: pointer;
}
.layout-logo:not(.left--collapse) {
  width: var(--left-width);
  min-width: var(--left-width);
}
.layout-tool {
  white-space: nowrap;
  color: #ffffff;
  padding: 0 8px;
  line-height: var(--head-height);
}
.layout-tool >>> .el-avatar {
  vertical-align: middle;
}
.layout-tool .user-label {
  cursor: pointer;
  color: #ffffff;
}
.layout-tool .user-label:hover {
  color: #ffd04b;
}
.layout-body {
  display: flex;
  position: absolute;
  top: var(--head-height);
  bottom: 0;
  left: 0;
  right: 0;
}
.layout-body-left {
  z-index: 999;
}
.layout-body-left >>> .el-scrollbar__wrap {
  overflow-x: hidden;
}
.layout-body-menu {
  min-height: calc(100vh - var(--head-height));
  border-right: none;
}
.layout-body-menu:not(.el-menu--collapse) {
  width: var(--left-width);
}
.layout-body-content {
  flex: auto;
  overflow: hidden;
}
.layout-body-title {
  padding: 8px;
  background-color: #f2f6fc;
  border-bottom: solid 1px #e6e6e6;
}
.layout-body-tabs >>> .el-tabs__content {
  display: none;
}
.layout-body-tabs >>> .el-tabs--border-card {
  border: none;
}
.layout-body-tabs >>> .el-tabs__nav-wrap {
  margin: 0;
}
.layout-body-view {
  height: calc(100vh - var(--head-height) -40px);
  overflow: auto;
  position: relative;
}
</style>
