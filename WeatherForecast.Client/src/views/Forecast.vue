<template>
  <div>
    <b-container class="bv-example-row">
      <b-row v-if="isInLoadingAppState">
        <Loading/>
      </b-row>
      <div class="mt-3" v-if="isInResultsAppState">
        <h2>Weather for {{ forecasts.cityname}}</h2>
        <forecast-current v-bind:currentforecast="getCurrentWeather"/>
        <h4>Forecasts</h4>
        <b-card-group columns>
        <ForecastForDay 
          v-for="(data,index) in forecasts.forecasts"
          v-bind:forecastforday="data"
          :key="index"
        />
        </b-card-group>
      </div>
      <div class="mt-3" v-if="isInNoResultsState">
        <b-alert
          show
          variant="warning"
        >No data for {{ getCurrentSearchstring || 'empty string'}} found.</b-alert>
      </div>
      <div class="mt-3" v-if="isInErrorState">
        <b-alert
          show
          variant="danger"
        >An Error Occurred, Please Try Again Later.</b-alert>
      </div>
    </b-container>
    <b-link class="pb-5" :to="{ name: 'home' }" v-if="!isInLoadingAppState">Search again</b-link>
  </div>
</template>

<script>
import * as appstates from "../store/states";
import ForecastForDay from "../components/ForecastForDay.vue";
import ForecastCurrent from "../components/ForecastCurrent.vue";
import Loading from "../components/Loading.vue";

export default {
  name: "Forecast",
  props: ["q"],
  components: {
    ForecastForDay,
    ForecastCurrent,
    Loading
  },
  data() {
    return {
      searchstring: ""
    };
  },
  created() {
    this.$store.dispatch("getWeatherForecast", this.q);
  },
  computed: {
    isInLoadingAppState() {
      return this.$store.getters.getState === appstates.LOADING;
    },
    isInResultsAppState() {
      return this.$store.getters.getState === appstates.RESULTS;
    },
    isInNoResultsState() {
      return this.$store.getters.getState === appstates.NORESULTS;
    },
    isInErrorState() {
      return this.$store.getters.getState === appstates.ERROR;
    },
    forecasts() {
      return this.$store.getters.getCurrentForecasts;
    },
    getCurrentWeather() {
      return this.forecasts.currentWeather;
    },
    getCurrentSearchstring() {
      return this.$store.getters.getCurrentSearchString;
    }
  }
};
</script>
<style>
</style>