<template>
  <input v-model="modelData" type="text" :name="name" />
</template>

<script>
export default {
  props: {
    model: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      modelData: this.model,
    };
  },
  watch: {
    modelData(newVal) {
      this.$emit("update:model", newVal);
    },
  },
  methods: {},
};
</script>

<style scoped>
input {
  outline: none;
  border: 0;
  filter: none;
}
</style>
