<template>
  <header class="appheader">    
    <!-- As a link -->
    <b-navbar variant="info" type="light" class="nav-bar">
      <b-navbar-brand href="#/home">
        <img src="../assets/Antu_weather-clouds-logo.png" class="d-inline-block align-top" alt="Icon" height="32" width="32">
        Weatherforecast
      </b-navbar-brand>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown text="Temperature unit" right>
            <b-dropdown-item @click="setTemperatureUnit('K')" href="#">Kelvin</b-dropdown-item>
            <b-dropdown-item @click="setTemperatureUnit('C')" href="#">Celcius</b-dropdown-item>
            <b-dropdown-item @click="setTemperatureUnit('F')" href="#">Fahrenheit</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </header>
</template>

<script>
export default {
  name: 'AppHeader',
  data() {
    return {
      selectedTemperatureUnit: '',
    }    
  },
  methods: {
    setTemperatureUnit(unit) {
      this.selectedTemperatureUnit = unit;
      this.$store.dispatch('setTemperatureUnit',this.selectedTemperatureUnit);
    },
  },
}
</script>

<style>
  .nav-icon {
    margin-right: 10px;
  }
  .nav-bar {
    box-shadow: 0 3px 2px -2px rgba(0,0,0,.9)
  }
</style>