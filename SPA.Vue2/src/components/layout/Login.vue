<template>
  <div class="login-page">
    <el-card class="login-form">
      <div class="login-logo">
        <h2>{{adminName}}</h2>
      </div>
      <el-form ref="form" :model="userInfo" :rules="rules" @keydown.enter.native="handleSubmit">
        <el-form-item prop="account">
          <el-input
            type="text"
            v-model="userInfo.account"
            prefix-icon="el-icon-user-solid"
            placeholder="账号"
          ></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input
            type="password"
            v-model="userInfo.password"
            prefix-icon="el-icon-lock"
            placeholder="密码"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button
            type="success"
            icon="el-icon-thumb"
            @click="handleSubmit"
            :loading="loading"
            style="width:100%"
          >登录</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <div class="copyright">Copyright © 2020 版权所有 {{companyName}}</div>
  </div>
</template>
<script>
import auth from "@/lib/auth.token"; //auth
import admin from "@/config/admin"; //admin
import { mapActions } from "vuex";
/**
 * 登录
 */
export default {
  data() {
    return {
      adminName: admin.name,
      companyName: admin.company,
      loading: false,
      userInfo: {
        account: null,
        password: null
      },
      //表单验证
      rules: {
        account: [
          {
            required: true,
            message: "账号不能为空"
          }
        ],
        password: [
          {
            required: true,
            message: "密码不能为空"
          }
        ]
      }
    };
  },
  methods: {
    ...mapActions("user", ["login"]),
    handleSubmit() {
      this.$refs.form.validate().then(() => {
        this.loading = true;
        this.login(this.userInfo)
          .then(res => {
            this.loading = false;
            if (res.success) {
              this.$message.success("登录成功");
              const data = res.data;
              auth.setToken(data.token, data.expiresIn);
              setTimeout(() => {
                this.back();
              }, 1000);
            } else {
              this.$msgbox({
                title: "登录失败",
                message: res.msg,
                type: "error"
              });
            }
          })
          .catch(err => (this.loading = false));
      });
    },
    home() {
      this.$router.replace({ path: "/" });
    },
    back() {
      let redirect = this.$route.query.redirect;
      if (redirect) {
        this.$router.push({ path: redirect });
      } else {
        this.home();
      }
    }
  }
};
</script>

<style scoped>
.login-page {
  position: absolute;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-image: url("../../assets/image/bg-login.jpg");
  background-size: 100% 100%;
  background-color: #909399;
}
.login-form {
  width: 350px;
}
.login-logo {
  text-align: center;
  color: #ff9900;
  margin-bottom: 15px;
}
.copyright {
  position: fixed;
  text-align: center;
  bottom: 10px;
  left: 0;
  right: 0;
  color: #fff;
  font-size: 12px;
}
</style>
