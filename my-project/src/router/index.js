import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/Customer/HomeView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/customer/detail/:id',
    name: 'detail',
    component: () => import('../views/Customer/DetailView.vue')
  },
  {
    path: '/customer/shoppingcart',
    name: 'shoppingcart',
    component: () => import('../views/Customer/ShoppingCart.vue')
  },
  {
    path: '/customer/checkout',
    name: 'checkout',
    component: () => import('../views/Customer/CheckOutView.vue')
  },
  {
    path: '/customer/order',
    name: 'order',
    component: () => import('../views/Customer/OrderView.vue')
  },
  {
    path: '/admin/login',
    name: 'login',
    component: () => import('../views/Admin/LoginView.vue')
  },
  {
    path: '/admin',
    name: 'admin',
    component: () => import('../views/Admin/HomeView.vue')
  },
  {
    path: '/admin/product',
    name: 'product',
    component: () => import('../views/Admin/ProductView.vue')
  },
  {
    path: '/admin/order',
    name: 'adorder',
    component: () => import('../views/Admin/OrderView.vue')
  },
  {
    path: '/admin/customer',
    name: 'customer',
    component: () => import('../views/Admin/CustomerView.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
