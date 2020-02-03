<template>
  <div class="mt-3" v-if="showHistory"> 
    <h2>Search History</h2>
    <b-container>
        <b-row v-for="(data,index) in getHistoryWithTodaysForecasts" :key="index" class="text-center history-row" @click="searchIt(data)">
            <b-col>{{ data.city }}</b-col>            
            <b-col><v-icon name="thermometer-half"/> {{ data.temp | temperatureUnit(temperatureUnit)}}</b-col>            
            <b-col><v-icon name="tint"/> {{ data.humidity  | rounding(2)}} %</b-col>            
            <b-col><v-icon name="wind"/> {{ data.windspeed  | rounding(2)}} m/s</b-col>            
        </b-row>
    </b-container>
  </div>
</template>

<script>
import * as appstates from '../store/states';

export default {
    name: 'search-history',
    data() {
        return {
            fields: [
                { name: 'City', icon: null},
                { name: 'temperature', icon: null},
                { name: 'humidity', icon: null},
                { name: 'windspeed', icon: null},
            ]
        }
    },
    computed: {
        appstate() {
            return this.$store.getters.getState;
        },
        showHistory() {
            return this.$store.getters.getState === appstates.SEARCHHISTORY;
        },
        getHistory() {
            return this.$store.getters.getHistory;
        },
        
        getHistoryWithTodaysForecasts() {
            const history = this.getHistory;
            const today = new Date();
            let data = [];
            history.reverse().forEach(element => {
                const todaysForecast = element.forecasts.filter(f=> new Date(f.date).getDay() === today.getDay());
                data.push(
                    {
                        city: element.cityname,
                        temp: todaysForecast[0].temperatureAverageInKelvin,
                        humidity: todaysForecast[0].humidityAverageInPercent,
                        windspeed: parseFloat(todaysForecast[0].windSpeedAverageInMeterPerSecond).toFixed(2)
                    }
                );
            }         
            );
            return data;            
        },
        temperatureUnit() {
          return this.$store.getters.getTemperaturUnit;
      },
    },
    methods: {
        searchIt(rowdata){
            this.$store.dispatch('getWeatherForecast', rowdata.city);
        },
    }
}
</script>

<style>
 .history-row:hover {
     border-style: solid;
     border-color: var(--primary);
     border-radius: 5px;
    /* background-color: var(--primary); */
 }
</style>