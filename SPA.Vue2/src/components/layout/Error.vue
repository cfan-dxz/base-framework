<template>
  <div class="error">
    <img src="../../assets/image/vue-logo.png" />
    <div style="width:300px;margin:20px auto;">
      <el-alert type="warning" show-icon :closable="false" title="404 Not Found" :description="msg"></el-alert>
    </div>
  </div>
</template>

<script>
export default {
  name: "Error",
  data() {
    return {
      msg: "Sorry, the page you visited does not exist."
    };
  }
};
</script>
<style scoped>
.error {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
