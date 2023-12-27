import { createStore } from 'vuex'
import router from '@/router'
import axios from 'axios'

export default createStore({
  state: {
    categories: [],
    products:[],
    productImages:[],
    product:[],
    token: '',
    customer: '',
    isCustomerLogin: false,
    admin: '',
    isAdminLogin: false,
    cart: [],
    orders: [],
    orderDetails: [],
    users:[],
    customers:[]
  },
  getters: {
    products: state => state.products,
    categories: state => state.categories,
    productImages: state => state.productImages,
    product: state => state.product,
    token: state => state.token,
    customer: state => state.customer,
    isCustomerLogin: state => state.isCustomerLogin,
    admin: state => state.admin,
    isAdminLogin: state => state.isAdminLogin,
    cart: state => state.cart,
    orders: state => state.orders,
    orderDetails: state => state.orderDetails,
    users: state => state.users,
    customers: state => state.customers
  },
  mutations: {
    initProducts(state, products){
      state.products = products;
      for(var i = 0; i < state.products.length; i++){
        var listImage = state.productImages.filter(item => item.productId == state.products[i].id);
        state.products[i] = {...this.state.products[i],...{imageUrl: listImage[0].imageUrl}};
      }
    },
    initCategories(state, categories){
      state.categories = categories
    },
    initProductImages(state, productImages){
      state.productImages = productImages
    },
    listProductByCat(state, catId){
      state.products = state.products.filter(item => item.categoryId == catId);
    },
    searchProduct(state, keyword){
      state.products = state.products.filter(item => item.productName.indexOf(keyword) > -1);
    },
    getProduct(state, product){
      var listImage = state.productImages.filter(item => item.productId == product.id);
      product = {...product,...{imageUrl: listImage[0].imageUrl}};
      state.product = product;
      console.log(state.product);
    },
    SignUp(state, customer){
      state.customer = customer;
    },
    Login(state, customer){
      state.customer = customer;
      state.isCustomerLogin = true;
    },
    LoginAdmin(state, admin){
      state.admin = admin;
      state.isAdminLogin = true;
    },
    SignOut(state){
      state.customer = '';
      state.isCustomerLogin = false;
      state.token = '';
    },
    SignOutAdmin(state){
      state.admin = '';
      state.isAdminLogin = false;
      state.token = '';
    },
    setToken(state, token){
      state.token = token;
    },
    addToCart(state, product){
      let dem = 0;
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity++;
          dem++;
        }
      }
      if(dem == 0){
        state.cart.push({item: product, quantity: 1})
      }
      console.log(state.cart);
    },
    addQuantity(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity++;
        }
      }
    },
    removeItem(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart.splice(i, 1);
        }
      }
    },
    minusQuantity(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity--;
          if(state.cart[i].quantity == 0){
            state.cart.splice(i, 1);
          }
        }
      }
    },
    setCart(state){
      state.cart = [];
    },
    initOrders(state, orders){
      state.orders = orders;
    },
    initOrderDetails(state, orderDetails){
      state.orderDetails = orderDetails;
    },
    deleteOrder(state, orderId){
      var newArr = state.orders.filter(x => x.id != orderId);
      state.orders = newArr;
    },
    initUsers(state, users){
      state.users = users;
    },
    initCustomers(state){
      state.customers = state.users.filter(x => x.isCustomer == 1);
    }
  },
  actions: {
    loadCategories({commit}){
      axios.get('https://localhost:44309/api/Category')
      .then(response => {
        if(response.data){
          commit("initCategories", response.data);
        }
      }).catch(e => {
        console.log(e);
      })
    },
    loadProducts({commit}){
      axios.get('https://localhost:44309/api/Product')
      .then(response  =>{
        if(response.data){
          commit("initProducts",response.data);
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    loadProductImages({commit}){
      axios.get('https://localhost:44309/api/ProductImage')
      .then(response =>{
        if(response.data){
          console.log('ok');
          commit("initProductImages", response.data);
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    listProductByCat({commit},id){
      axios.get('https://localhost:44309/api/Product')
      .then(response  =>{
        if(response.data){
          commit("initProducts",response.data);
          commit("listProductByCat", id);
          router.push('/');
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    searchProduct({commit},keyword){
      axios.get('https://localhost:44309/api/Product')
      .then(response  =>{
        if(response.data){
          commit("initProducts",response.data);
          commit("searchProduct", keyword);
          router.push('/');
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    searchPro({commit},keyword){
      axios.get('https://localhost:44309/api/Product')
      .then(response  =>{
        if(response.data){
          commit("initProducts",response.data);
          commit("searchProduct", keyword);
          router.push('/admin/product');
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    getProduct({commit}, id){
      if(id){
        axios.get('https://localhost:44309/api/Product' + "/" + id)
        .then(response =>{
          if(response.data){
            commit("getProduct", response.data);
          }
        }).catch(e =>{
          console.log(e);
        })
      }
    },
    SignUp({commit}, customer){
      axios.post('https://localhost:44309/api/User/SignUp', customer)
      .then(response =>{
        console.log(response.status);
        commit("SignUp", customer);
        console.log(customer);
      }).catch(e =>{
        console.log(e);
      })
    },
    Login({commit}, acc){
      axios.post('https://localhost:44309/api/User/Login?account=' + acc.username + '&password=' + acc.password)
      .then(response =>{
        if(response.data.user.isCustomer == 1){
          commit("Login", response.data.user);
          commit("setToken", response.data.token);
        }
        if(response.data.user.isAdmin == 1){
          commit("LoginAdmin", response.data.user);
          commit("setToken", response.data.token);
          router.push('/admin');
        }
      }).catch(e =>{
        console.log(e);
      })
    },
    SignOut({commit}){
      commit("SignOut");
      router.push('/');
    },
    SignOutAdmin({commit}){
      commit("SignOutAdmin");
      router.push('/admin/login')
    },
    addToCart({commit}, product){
      commit("addToCart", product);
    },
    addQuantity({commit}, product){
      commit("addQuantity", product);
    },
    removeItem({commit}, product){
      commit("removeItem", product);
    },
    minusQuantity({commit}, product){
      commit("minusQuantity", product);
    },
    setCart({commit}){
      commit("setCart");
    },
    initOrders({commit}){
      axios.get('https://localhost:44309/api/Order', {headers: {'Authorization': 'Bearer ' + this.state.token}})
      .then(response =>{
        commit("initOrders", response.data);
        console.log(this.state.orders);
      }).catch(e =>{
        console.log(e);
      })
    },
    initOrderDetails({commit}){
      axios.get('https://localhost:44309/api/OrderDetail', {headers: {'Authorization': 'Bearer ' + this.state.token}})
      .then(response =>{
        commit("initOrderDetails", response.data);
      }).catch(e =>{
        console.log(e);
      })
    },
    deleteOrder({commit}, orderId){
      axios.delete('https://localhost:44309/api/Order/' + orderId, {headers: {'Authorization': 'Bearer ' + this.state.token}})
      .then(response =>{
          commit("deleteOrder", orderId);
          console.log(response.status);
      }).catch(e =>{
          console.log(e);
      })
    },
    initUsers({commit}){
      axios.get('https://localhost:44309/api/User', {headers: {'Authorization': 'Bearer ' + this.state.token}})
      .then(response =>{
        commit("initUsers", response.data);
        commit("initCustomers");
        console.log(response.status);
      }).catch(e =>{
        console.log(e);
      })
    }
  },
  modules: {
  }
})
