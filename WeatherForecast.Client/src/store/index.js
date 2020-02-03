import Vue from "vue";
import Vuex from "vuex";
import * as appstates from "./states";
import weatherservice from "../api/weatherservice";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentAppState: appstates.START,
    currentsearch: "",
    searchhistory: [],
    currentweatherdata: null,
    temperatureUnit: "C"
  },
  mutations: {
    setCurrentSearchString: (state, searchstring) => {
      state.currentsearch = searchstring;
    },
    setState: (state, appstate) => {
      state.currentAppState = appstate;
    },
    setCurrentWeatherData: (state, currentweathersearch) => {
      state.currentweatherdata = currentweathersearch;
    },
    addCurrentSearchToHistory: state => {
      if (state.currentweatherdata) {
        state.searchhistory.push(state.currentweatherdata);
      }
    },
    setStateBasedOnResult: state => {
      state.currentAppState =
        state.currentweatherdata && state.currentweatherdata.forecasts
          ? appstates.RESULTS
          : appstates.NORESULTS;
    },
    setTemperatureUnit: (state, unit) => {
      state.temperatureUnit = unit;
    }
  },
  actions: {
    setCurrentSearchString: (context, searchstring) => {
      context.commit("setCurrentSearchString", searchstring);
    },
    getWeatherForecast: async (context, search) => {
      context.commit("setCurrentSearchString", search);
      context.commit("setState", appstates.LOADING);
      await weatherservice
        .getForecast(search)
        .then(response => {
          if (response.data.cityname) {
            context.commit("setCurrentSearchString", response.data.cityname);
          }
          if (response.data.forecasts) {
            context.commit("setCurrentWeatherData", response.data);
          } else {
            context.commit("setCurrentWeatherData", null);
          }
          context.commit("addCurrentSearchToHistory");
          context.commit("setStateBasedOnResult");
        })
        .catch(() => {
          context.commit("setState", appstates.ERROR);
        })
        .finally(() => {});
    },
    setTemperatureUnit: (context, unit) => {
      unit = unit.toUpperCase();
      const supported = ["K", "C", "F"];
      if (supported.indexOf(unit) !== -1) {
        context.commit("setTemperatureUnit", unit);
      }
    }
  },
  getters: {
    getCurrentForecasts: store => {
      return store.currentweatherdata;
    },
    getState: store => {
      return store.currentAppState;
    },
    getCurrentSearchString: store => {
      return store.currentsearch;
    },
    getHistory: store => {
      return store.searchhistory;
    },
    getTemperaturUnit: store => {
      return store.temperatureUnit;
    }
  }
});
