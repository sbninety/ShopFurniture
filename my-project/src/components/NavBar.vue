<template>
    <div>
        <v-app-bar app dark flat>
            <v-btn disabled></v-btn>
            <v-btn @click="loadProducts" to="/" variant="plain">
                <v-toolbar-title>WOODPRO</v-toolbar-title>
            </v-btn>
            <div style="position: absolute;margin-left: 260px;margin-right: auto;left: 0;right: 0;">
                <h4>Uy tín, chất lượng, 10 năm nhập gỗ từ Trung Quốc</h4>
            </div>
            <v-spacer></v-spacer>
            <v-col cols="3">
                <v-form @submit.prevent="searchProduct(keyword)">
                    <v-text-field 
                        v-model="keyword"
                        density="compact" 
                        variant="outlined" 
                        label="Tìm kiếm" 
                        append-inner-icon="mdi-magnify" 
                        single-line hide-details 
                        class="mr-2">
                    </v-text-field>
                </v-form>
            </v-col>
            <v-btn icon class="mr-2" @click="checkLogin()">
                <v-icon>mdi mdi-shopping</v-icon>
            </v-btn>
            <v-btn icon class="mr-2" to="/customer/shoppingcart">
                <v-badge :content="cart.length">
                    <v-icon>mdi mdi-cart-outline</v-icon>
                </v-badge>
            </v-btn>
            <v-btn variant="outlined" @click="dialogLogin=true" v-if="isCustomerLogin==false">
                Đăng nhập
            </v-btn>
            <v-btn variant="outlined" @click="SignOut" v-else>
                Đăng xuất
            </v-btn>
        </v-app-bar>
        <login-view
            :dialogLogin="dialogLogin"
            @close="dialogLogin=flase"
        />
    </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import LoginView from '../views/Customer/LoginView.vue'
import router from '@/router'
export default {
  components: { LoginView },
    data(){
        return{
            dialogLogin: false,
            keyword:'',
        }
    },
    methods:{
        ...mapActions(['searchProduct','loadProducts','SignOut']),
        checkLogin(){
            if(this.isCustomerLogin == false){
                this.dialogLogin = true;
            }else{
                router.push('/customer/order');
            }
        }
    },
    computed:{
        ...mapGetters(['isCustomerLogin','cart'])
    }
}
</script>

<style>

</style>