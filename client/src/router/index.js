import { createRouter, createWebHistory } from 'vue-router'
import FeedbackView from '../views/FeedbackView.vue'
import KursmaterialView from '../views/KursmaterialView.vue'
import FAQView from '../views/FAQView.vue'

const routes = [
  { path: '/', redirect: '/feedback' },
  { path: '/feedback', component: FeedbackView },
  { path: '/kursmaterial', component: KursmaterialView },
  { path: '/faq', component: FAQView },
]

export default createRouter({
  history: createWebHistory(),
  routes,
})
