import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: '',
    component: Home
  },
  {
    path: '/home',
    name: 'home',
    component: Home
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('../views/About.vue')
  },
  {
    path: '/error',
    name: 'error',
    component: () => import('../views/Error.vue')
  },  
  {
    path: '/forecast/:q',
    component: () => import( '../views/Forecast.vue'),
    props: true,
  },
  {
    path: '/history',
    component: () => import('../views/SearchHistory.vue')
  },
  {
    path: '*',
    name: 'notfound',
    component: () => import('../views/UrlNotFound.vue')
  }
]

const router = new VueRouter({
  routes,
  scrollBehavior() {
      document.getElementById('app').scrollIntoView();
  }
})

export default router
