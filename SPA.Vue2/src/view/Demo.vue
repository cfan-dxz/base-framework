<template>
  <div class="page">
    <!-- head -->
    <SimpleHead @on-search="reloadTable(true)" :model="filter">
      <template slot="button">
        <!-- 操作按钮 -->
        <el-form-item>
          <el-button-group>
            <el-button type="success" icon="el-icon-plus">添加</el-button>
            <el-button type="primary" icon="el-icon-edit">编辑</el-button>
            <el-button type="danger" icon="el-icon-delete">删除</el-button>
          </el-button-group>
        </el-form-item>
      </template>
      <!-- 默认搜索项 -->
      <el-form-item>
        <el-input v-model="filter.input" placeholder="请输入内容"></el-input>
      </el-form-item>
      <template slot="more">
        <!-- 更多搜索项 -->
        <el-form-item>
          <el-date-picker
            type="daterange"
            start-placeholder="添加开始日期"
            end-placeholder="添加结束日期"
            style="width:260px"
          />
        </el-form-item>
      </template>
    </SimpleHead>
    <!-- table -->
    <SimpleTable ref="table" :filter="filter">
      <el-table-column type="selection" width="35" fixed="left" />
      <el-table-column prop="date" label="日期" width="180"></el-table-column>
      <el-table-column prop="name" label="姓名" width="180"></el-table-column>
      <el-table-column prop="address" label="地址"></el-table-column>
    </SimpleTable>
  </div>
</template>

<script>
// import { getList } from "@/api/sys/user";
export default {
  name: "Demo",
  components: {
    SimpleHead: () => import("@/components/common/SimpleHead"), //头部组件
    SimpleTable: () => import("@/components/common/SimpleTable") //表格组件
  },
  data() {
    return {
      // tableUrl: getList.url,
      // 过滤项
      filter: {
        input: null
      }
    };
  },
  methods: {
    reloadTable(again) {
      this.$refs.table.reload(again);
    }
  }
};
</script>

<style scoped>
</style>
