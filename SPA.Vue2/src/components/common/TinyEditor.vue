<template>
  <div>
    <editor v-model="content" :init="init"></editor>
  </div>
</template>
<script>
import tinymce from "tinymce/tinymce";
import Editor from "@tinymce/tinymce-vue";
import "tinymce/themes/silver/theme";
import "tinymce/icons/default/icons";
import "tinymce/plugins/image";
import "tinymce/plugins/media";
import "tinymce/plugins/table";
import "tinymce/plugins/lists";
import "tinymce/plugins/contextmenu";
import "tinymce/plugins/wordcount";
import "tinymce/plugins/colorpicker";
import "tinymce/plugins/textcolor";
import "tinymce/plugins/fullscreen";
import "tinymce/plugins/preview";
import "tinymce/plugins/code";
// import { uploadImageForCkeditor } from "@/api/bigdata/upload";
export default {
  props: {
    //传入一个value，使组件支持v-model绑定
    value: {
      type: String,
      default: ""
    },
    height: { type: Number, default: 400 }
  },
  components: {
    Editor
  },
  data() {
    return {
      init: {
        language_url: "/static/tinymce/langs/zh_CN.js", //语言包的路径
        language: "zh_CN", //语言
        skin_url: "/static/tinymce/skins/ui/oxide", //skin路径
        base_url: "/static/tinymce",
        suffix: ".min",
        height: this.height, //编辑器高度
        branding: false, //是否禁用“Powered by TinyMCE”
        menubar: false, //顶部菜单栏显示
        images_upload_handler: (blobInfo, success, failure) => {
          //图片上传处理
          const xhr = new XMLHttpRequest();
          xhr.withCredentials = false;
          // xhr.open("POST", uploadImageForCkeditor.url);
          xhr.onload = () => {
            if (xhr.status != 200) {
              failure("HTTP Error: " + xhr.status);
              return;
            }
            const resJson = JSON.parse(xhr.responseText);
            if (!resJson || !resJson.uploaded) {
              failure("Invalid JSON: " + xhr.responseText);
              return;
            }
            success(resJson.url);
          };
          const formData = new FormData();
          formData.append("file", blobInfo.blob(), blobInfo.filename());
          xhr.send(formData);
        },
        fontsize_formats: "8px 10px 12px 14px 16px 18px 24px 36px 48px",
        plugins:
          "lists image media table textcolor wordcount contextmenu fullscreen preview code",
        toolbar:
          "undo redo |  formatselect fontselect fontsizeselect | forecolor backcolor | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | lists image media table | removeformat code | preview fullscreen"
      },
      content: this.value
    };
  },
  watch: {
    value(val) {
      this.content = val || "";
    },
    content(val) {
      this.$emit("input", val);
    }
  },
  mounted() {
    tinymce.init({});
  }
};
</script>
