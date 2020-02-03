<template>
  <div>    
    <b-card  header-tag="header" class="mx-auto">
      <template v-slot:header>
        <h6 class="mb-0">{{ forecastforday.date | moment("dddd, MMMM Do YYYY")}}</h6>
      </template>
      <b-card-text><v-icon class="icon-forecast" name="thermometer-half"/>Average temperature {{ forecastforday.temperatureAverageInKelvin | temperatureUnit(temperatureUnit)}}</b-card-text>
      <b-card-text><v-icon class="icon-forecast" name="tint"/>Average humidity {{ forecastforday.humidityAverageInPercent | rounding(2) }} %</b-card-text>
      <b-card-text><v-icon class="icon-forecast" name="wind"/>Average windspeed {{ forecastforday.windSpeedAverageInMeterPerSecond  | rounding(2) }} m/s</b-card-text>
    </b-card>
  </div>
</template>

<script>
export default {
    name:'forecastforday',
    props: ['forecastforday'],    
    computed: {
      temperatureUnit() {
          return this.$store.getters.getTemperaturUnit;
      },
    }
}
</script>

<style>
.icon-forecast{
  margin-right: 5px;
  padding-bottom: 2px;
}
.card {
  margin: 1rem;
	opacity: 1;
	animation-name: fadeInOpacity;
	animation-iteration-count: 1;
	animation-timing-function: ease-in;
	animation-duration: 0.5s;
}
@keyframes fadeInOpacity {
	0% {
		opacity: 0;
	}
	100% {
		opacity: 1;
	}
}
</style>