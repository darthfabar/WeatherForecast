<template>
  <div class="currentweather mx-auto" v-if="visible">    
    <p>
      <v-icon class="icon-forecast" name="thermometer-half"/>current temperature {{ currentforecast.temperatureInKelvin | temperatureUnit(temperatureUnit)}}
    </p>      
    <p>
      <v-icon class="icon-forecast" name="tint"/>current humidity {{ currentforecast.humidityInPercent | rounding(2) }} %
    </p>
    <p>
      <v-icon class="icon-forecast" name="wind"/>current windspeed {{ currentforecast.windSpeedInMeterPerSecond  | rounding(2) }} m/s
    </p>
  </div>
</template>

<script>
export default {
    name:'ForecastCurrent',
    props: ['currentforecast'],    
    computed: {
      temperatureUnit() {
          return this.$store.getters.getTemperaturUnit;
      },
      visible() {
        return this.currentforecast !== undefined
            && this.currentforecast.temperatureInKelvin !== undefined
            && this.currentforecast.humidityInPercent !== undefined
            && this.currentforecast.windSpeedInMeterPerSecond !== undefined;
      }
    }
}
</script>

<style>
.icon-forecast{
  margin-right: 5px;
  padding-bottom: 2px;
}
.currentweather {
  margin: 1rem;
  max-width: 30%;
	opacity: 1;
	animation-name: fadeInOpacity;
	animation-iteration-count: 1;
	animation-timing-function: ease-in;
	animation-duration: 0.5s;
}
@media only screen and (max-width: 600px) {
.currentweather{
  max-width: 100%;
}
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