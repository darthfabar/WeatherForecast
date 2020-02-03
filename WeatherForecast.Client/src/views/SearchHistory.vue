<template>
  <div class="mt-3">
    <h2>Search History</h2>
    <b-container>
      <b-row
        v-for="(data,index) in getHistoryWithTodaysForecasts"
        :key="index"
        class="text-center history-row"
        @click="searchIt(data)"
      >
        <b-col>{{ data.city }}</b-col>
        <b-col>
          <v-icon name="thermometer-half" />
          {{ data.temp | temperatureUnit(temperatureUnit)}}
        </b-col>
        <b-col>
          <v-icon name="tint" />
          {{ data.humidity | rounding(2)}} %
        </b-col>
        <b-col>
          <v-icon name="wind" />
          {{ data.windspeed | rounding(2)}} m/s
        </b-col>
      </b-row>
      <b-row v-if="!hasHistory">
        <b-col>No History</b-col>
      </b-row>
    </b-container>
    <b-link class="mt-4 pb-5 mb-3" :to="{ name: 'home' }">Search again</b-link>
  </div>
</template>

<script>
import router from "../router";

export default {
  name: "search-history",
  data() {
    return {
      fields: [
        { name: "City", icon: null },
        { name: "temperature", icon: null },
        { name: "humidity", icon: null },
        { name: "windspeed", icon: null }
      ]
    };
  },
  computed: {
    appstate() {
      return this.$store.getters.getState;
    },
    getHistory() {
      return this.$store.getters.getHistory;
    },
    hasHistory() {
      return this.getHistory !== null && this.getHistory.length > 0;
    },
    getHistoryWithTodaysForecasts() {
      const history = this.getHistory;
      const today = new Date();
      let data = [];
      history.reverse().forEach(element => {
        const todaysForecast = element.forecasts.filter(
          f => new Date(f.date).getDay() === today.getDay()
        );

        if(todaysForecast.length ===0) todaysForecast.push({
          temperatureAverageInKelvin:"-",
          humidityAverageInPercent:"-",
          windSpeedAverageInMeterPerSecond:"-",
          });

        data.push({
          city: element.cityname,
          temp: todaysForecast[0].temperatureAverageInKelvin ?? "-",
          humidity: todaysForecast[0].humidityAverageInPercent ?? "-",
          windspeed: todaysForecast[0].windSpeedAverageInMeterPerSecond ?? "-"
        });
      });
      return data;
    },
    temperatureUnit() {
      return this.$store.getters.getTemperaturUnit;
    }
  },
  methods: {
    searchIt(rowdata) {
      router.push({
        path: "/forecast/" + rowdata.city
      });
      this.$store.dispatch("getWeatherForecast", rowdata.city);
    }
  }
};
</script>

<style>
.history-row:hover {
  border-style: solid;
  border-color: var(--primary);
  border-radius: 5px;
}
</style>