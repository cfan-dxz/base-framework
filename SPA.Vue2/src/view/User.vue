<template>
  <h1>用户：{{oidcUser&&oidcUser.UserName}}</h1>
</template>

<script>
// import { getList } from "@/api/sys/user";
import { mapGetters } from "vuex";
export default {
  name: "User",
  data() {
    return {
      userName: null,
    };
  },
  computed: {
    ...mapGetters("oidcStore", {
      oidcUser: "oidcUser",
    }),
  },
  mounted() {},
};
</script>
