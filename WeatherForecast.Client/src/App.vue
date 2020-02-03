<template>
  <div id="app">
    <app-header />
    <router-view />
    <div class="footer">
      <b-link class="mx-auto" :to="{ name: 'about' }">about</b-link>
    </div>
  </div>
</template>
<script>
import AppHeader from "./components/AppHeader.vue";
export default {
  name: "app",
  components: {
    AppHeader
  }
};
</script>
<style>
.footer {
  background-color: var(--light);
  margin-top: 10px;
  position: fixed;
  left: 0;
  bottom: 0;
  width: 100%;
}
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
