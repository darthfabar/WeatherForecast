<template>
  <div>
    <div>
      <h2 class="mt-3">Welcome</h2>
      <div class="mt-3">
        <b-form inline @submit.stop.prevent>
          <b-input-group class="mx-auto">
            <b-form-input
              class="ml-3"
              placeholder="Search"
              v-model="searchstring"
              v-on:keyup.enter="search"
            ></b-form-input>
            <b-input-group-append class="mr-3">
              <b-button v-on:click="search" variant="primary" v-b-popover.hover.bottom="'type in german city name or a zip-code'">
                <v-icon name="search" />
              </b-button>
              <b-button v-if="hasHistory()" v-on:click="gotohistory" variant="secondary" v-b-popover.hover.bottom="'Search history'">
                <v-icon name="history" />
              </b-button>
            </b-input-group-append>
          </b-input-group>
        </b-form>
      </div>
    </div>
  </div>
</template>

<script>
import router from "../router";

export default {
  name: "Home",
  data() {
    return {
      searchstring: ""
    };
  },
  methods: {
    search() {
      if (!this.searchstring) return;
      router.push({
        path: "/forecast/" + this.searchstring
      });
    },
    hasHistory() {
      return (
        this.$store.getters.getHistory &&
        this.$store.getters.getHistory.length > 0
      );
    },
    gotohistory() {
      router.push({
        path: "/history/"
      });
    },
    getCurrentSearchstring() {
      return this.$store.getters.getCurrentSearchString;
    }
  }
};
</script>

<style>
</style>