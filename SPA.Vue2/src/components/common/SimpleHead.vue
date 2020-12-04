<template>
  <div class="simple-head">
    <div class="simple-head-default">
      <div class="simple-head-item">
        <el-form ref="form" :inline="true" :model="model">
          <slot name="button" />
          <slot />
          <el-form-item>
            <el-button type="primary" icon="el-icon-search" v-on:click="onSearch">搜索</el-button>
          </el-form-item>
          <el-form-item v-if="model">
            <el-button icon="el-icon-refresh" v-on:click="onReset">重置</el-button>
          </el-form-item>
        </el-form>
      </div>
      <div class="simple-head-expand">
        <el-button
          v-if="hasMore"
          :icon="moreBtnIcon"
          type="primary"
          v-on:click="onExpand"
          title="更多"
        />
      </div>
    </div>
    <template v-if="isMore">
      <el-form v-show="expand" ref="formMore" :inline="true" :model="model">
        <slot name="more" />
      </el-form>
    </template>
  </div>
</template>
<script>
import table from "@/lib/ext.table"; //table
export default {
  props: {
    model: { type: Object, default: null }
  },
  data: function() {
    return {
      hasMore: false,
      isMore: false,
      expand: false
    };
  },
  computed: {
    moreBtnIcon() {
      if (this.expand) {
        return "el-icon-arrow-up";
      } else {
        return "el-icon-arrow-down";
      }
    }
  },
  methods: {
    onSearch() {
      this.$emit("on-search");
    },
    onReset() {
      this.model &&
        Object.keys(this.model).forEach(k => {
          if (Array.isArray(this.model[k])) {
            this.model[k] = [];
          } else {
            this.model[k] = null;
          }
        });
      this.$emit("on-reset");
    },
    onExpand() {
      this.isMore = !this.isMore;
      const action = () => {
        this.expand = !this.expand;
        table.resize();
      };
      if (this.isMore) {
        action();
      } else {
        this.isMore = true;
        this.$nextTick(() => {
          action();
          this.keypressListener(this.$refs.formMore.$el);
        });
      }
      this.$nextTick(() => {
        this.$emit("on-expand", this.isMore);
      });
    },
    //监听回车
    keypressListener(elem) {
      if (elem) {
        elem.addEventListener("keypress", e => {
          let keycode = (e || event).keyCode;
          if (keycode === 13) {
            this.$emit("on-search"); //回车搜索
            return false;
          }
        });
      }
    }
  },
  mounted() {
    const more = this.$slots.more;
    if (more) {
      this.hasMore = true;
    }
    this.keypressListener(this.$refs.form.$el);
  }
};
</script>
<style scoped>
.simple-head >>> .el-form-item {
  margin-bottom: 5px !important;
  margin-right: 3px !important;
}
.simple-head-default {
  display: flex;
  flex-direction: row;
}
.simple-head-item {
  width: 95%;
}
.simple-head-expand {
  width: 100px;
  flex-grow: 1;
  text-align: right;
}
</style>
