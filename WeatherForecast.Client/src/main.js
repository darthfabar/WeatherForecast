import Vue from 'vue'
import App from './App.vue'
import store from './store'

import { BootstrapVue } from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Icon from 'vue-awesome/components/Icon'

import 'vue-awesome/icons'; //TODO nur nötige
import router from './router'

Vue.component('v-icon', Icon);
Vue.use(BootstrapVue);

Vue.config.productionTip = false
Vue.use(require('vue-moment'));



const temperatureUnitFilter = function(value, unit) {
  const valueToNumber = parseFloat(value);
  let calcFunc;
  let unitrepresentation ='';
  if (isNaN(valueToNumber)) return value;
  if (unit.toUpperCase() === 'K') {    
    calcFunc = (val) => val.toFixed(2);
    unitrepresentation = 'K';    
  }
  if (unit.toUpperCase() === 'C') {
    calcFunc = (val) => (val - 273.15).toFixed(2);
    unitrepresentation = '°C';
  }
  if (unit.toUpperCase() === 'F') {
    calcFunc = (val) => (val * (9/5) - 459.67).toFixed(2);
    unitrepresentation = '°F';    
  }
  if (!calcFunc) return `${valueToNumber} ${unitrepresentation}`;
  return `${calcFunc(valueToNumber)} ${unitrepresentation}`;  
}

const roundingFilter = function(value, fixedto) {
  const valueToNumber = parseFloat(value);
  if (isNaN(valueToNumber)) return value;
  return valueToNumber.toFixed(fixedto);
}

Vue.filter('temperatureUnit', temperatureUnitFilter);
Vue.filter('rounding', roundingFilter);


// Icon from https://commons.wikimedia.org/wiki/File:Antu_weather-clouds.svg
new Vue({
  store,
  router,
  render: h => h(App)
}).$mount('#app')
