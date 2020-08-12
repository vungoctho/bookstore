<template>
  <div class="modal is-active">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Information</p>
        <button class="delete" aria-label="close" @click="close"></button>
      </header>
      <section class="modal-card-body" v-html="dialogMessage"></section>
      <footer class="modal-card-foot buttons is-right">
        <button class="button" @click="close">Close</button>
      </footer>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    dialogMessage: ""
  },
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };

    return {
      close
    };
  }
};
</script>

<style>
</style>