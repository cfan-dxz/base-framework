<template></template>

<script>
import { mapActions } from "vuex";

export default {
  name: "OidcCallback",
  methods: {
    ...mapActions("oidcStore", ["oidcSignInCallback"]),
  },
  mounted() {
    this.oidcSignInCallback()
      .then((redirectPath) => {
        this.$router.push(redirectPath);
        console.log(redirectPath);
      })
      .catch((err) => {
        console.error(err);
        this.$router.push("/oidc/callbackerror"); // Handle errors any way you want
      });
  },
};
</script>
