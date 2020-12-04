<template>
  <div class="simple-table">
    <el-table
      v-loading="loading"
      ref="table"
      :data="tableData"
      :height="tableHeight"
      :max-height="maxHeight"
      :size="size"
      border
      style="width:100%"
      @sort-change="sortChange"
      @selection-change="selectionChange"
    >
      <slot />
    </el-table>
    <el-pagination
      v-if="usePage"
      :current-page="page.page"
      :page-size="page.limit"
      :total="dataTotal"
      background
      layout="total,sizes,prev,pager,next,jumper"
      @size-change="pageSizeChange"
      @current-change="pageIndexChange"
    />
  </div>
</template>
<script>
import axios from "axios";
import { mapGetters } from "vuex";
export default {
  props: {
    filter: { type: Object, default: null }, // 过滤项
    params: { type: Object, default: null }, // 参数
    url: { type: String, default: null }, // 接口地址
    body: { type: Array, default: null }, // 表体数据
    method: { type: String, default: "post" }, // 接口请求类型:默认post
    limit: { type: Number, default: 20 }, // 限制数量:默认20
    offset: { type: Number, default: 0 }, // 高度偏移量
    usePage: { type: Boolean, default: true }, // 使用分页
    autoLoad: { type: Boolean, default: true }, // 自动加载
    maxHeight: { type: Number, default: null }, // 最大高度
    size: { type: String, default: "small" }, // 尺寸:默认small
    clear: { type: Boolean, default: false } // 清除
  },
  data() {
    return {
      tableData: [],
      dataTotal: 0, // 总条数
      loading: false, // 加载中
      page: { page: 1, limit: this.limit }, // 分页
      sort: { sort: null, order: null }, // 排序
      height: {
        window: window.innerHeight,
        top: 0
      }, // 高度
      selection: [] // 选中项
    };
  },
  computed: {
    // 表格高度
    tableHeight() {
      return this.height.window - this.height.top - this.offset - this.fixed;
    },
    fixed() {
      return this.usePage ? 143 : 108;
    },
    ...mapGetters("table", {
      resizeTag: "getTag"
    })
  },
  methods: {
    loadData() {
      this.loading = true;
      //先清除旧数据
      this.clear && this.data.splice(0, this.data.length);
      axios({
        url: this.url,
        data: Object.assign(this.page, this.filter, this.sort),
        params: this.params,
        method: this.method,
        withCredentials: true
      })
        .then(res => {
          this.loading = false;
          const result = res.data;
          if (result.success) {
            this.tableData = result.data;
            this.dataTotal = result.count;
            this.$emit("on-complete", this.data);
          } else {
            this.showError(result.msg);
          }
        })
        .catch(err => {
          this.loading = false;
          const msg = err.response ? err.response.data.msg : err.message;
          this.showError(msg);
        });
    },
    showError(msg) {
      this.$msgbox({
        title: "加载数据出错",
        message: msg,
        type: "error"
      });
    },
    // 重新加载
    reload(again) {
      // 优先url获取数据，如果未设置url则考虑传进来的数据
      if (this.url) {
        if (again) {
          this.page.page = 1; // 从第1页开始
        }
        this.loadData();
      } else if (this.body) {
        this.tableData = this.body;
      }
    },
    // 切换页码
    pageIndexChange(pageIndex) {
      this.page.page = pageIndex;
      this.loadData();
    },
    // 切换页数
    pageSizeChange(pageSize) {
      this.page.limit = pageSize;
      this.loadData();
    },
    // 切换排序
    sortChange(obj) {
      if (obj.order === "normal") {
        this.sort.sort = null;
        this.sort.order = null;
      } else {
        this.sort.sort = obj.prop;
        this.sort.order = obj.order === "descending" ? "desc" : "asc";
      }
      this.reload();
    },
    // 选中更改
    selectionChange(arr) {
      this.selection = arr;
      this.$emit("on-selection-change", arr);
    },
    // 获取选中项
    getSelection() {
      return this.selection;
    },
    // 距离顶部高度
    topHeight() {
      this.height.top = this.$refs.table.$el.offsetTop;
    },
    // 重设高度
    resize() {
      this.topHeight();
    }
  },
  watch: {
    body(val) {
      this.data = val;
    },
    resizeTag() {
      this.resize();
    }
  },
  mounted() {
    this.$nextTick(() => this.topHeight());
    // 监听窗口大小改变
    window.addEventListener("resize", () => {
      this.height.window = window.innerHeight;
    });
    if (this.autoLoad) {
      this.reload();
    }
  }
};
</script>
<style scoped>
.simple-table .el-pagination {
  margin-top: 3px;
}
.simple-table >>> .txt-inline {
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
</style>
