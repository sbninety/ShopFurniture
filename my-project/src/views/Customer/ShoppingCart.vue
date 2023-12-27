<template>
    <v-app>
        <NavBar/>
        <br><br><br>
        <v-container>
            <v-layout
                class="visible"
                style="height: 56px;"
            >
                <v-bottom-navigation
                    active
                >
                    <v-btn
                        v-for="item in categories"
                        :key="item"
                        size="large"
                        style="font-size: 14px;"
                        @click="listProductByCat(item.id)"
                    >{{ item.categoryName }}</v-btn>
                </v-bottom-navigation>
            </v-layout>
            <hr>
            <v-row class="mt-4">
                <v-col align="center">
                    <h2>GIỎ HÀNG CỦA BẠN</h2>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="8">
                    <h3>Sản phẩm đã chọn</h3>
                    <v-card class="mt-3" v-for="o in cart" :key="o">
                        <v-row class="mt-2 ml-2 mb-2">
                            <v-col cols="3">
                                <v-img :src="o.item.imageUrl"></v-img>
                            </v-col>
                            <v-col cols="4">
                                <h3>{{ o.item.productName }}</h3>
                                <p class="mt-2">Kích thước: {{ o.item.size }}</p>
                                <p class="mt-2">Chất liệu: {{ o.item.material }}</p>
                                <p class="mt-2">Màu sắc: {{ o.item.color }} </p>
                            </v-col>
                            <v-col cols="5" class="mt-4">
                                <v-row>
                                    <v-btn color="blue" variant="flat" @click="minusQuantity(o.item),updateTotal()">-</v-btn>
                                    <div style="border: 1px solid black; width: 100px; text-align: center;" class="pt-1 ml-1 mr-1">
                                        <p>{{ o.quantity }}</p>
                                    </div>
                                    <v-btn color="blue" variant="flat" @click="addQuantity(o.item),updateTotal()">+</v-btn>
                                    <v-btn color="red" variant="flat" class="ml-1" @click="removeItem(o.item),updateTotal()">
                                        <v-icon>mdi mdi-delete-outline</v-icon>
                                    </v-btn>
                                </v-row>
                                <v-row class="mt-6" style="padding-left: 70px;">
                                    <h4>{{ o.item.price*o.quantity }} vnd</h4>
                                </v-row>
                            </v-col>
                        </v-row>
                    </v-card>
                </v-col>
                <v-col cols="4">
                    <h3>Tổng kết</h3>
                    <v-card class="mt-3">
                        <v-row class="mt-2 ml-2">
                            <v-col>
                                <p>Tổng giá: </p>
                            </v-col>
                            <v-col>
                                <p>{{ total }} vnd</p>
                            </v-col>
                        </v-row>
                        <v-row class="mt-1 ml-2 mb-2">
                            <v-col>
                                <p>Ship: </p>
                            </v-col>
                            <v-col>
                                <p>Free</p>
                            </v-col>
                        </v-row>
                        <hr>
                        <v-row class="mt-1 ml-2 mb-2">
                            <v-col>
                                <p><b>Tổng tiền:</b></p>
                            </v-col>
                            <v-col>
                                <p><b>{{ total }} vnd</b></p>
                            </v-col>
                        </v-row>
                        <v-row class="mt-1 ml-2 mb-2 mr-2">
                            <v-btn color="blue" block @click="checkLogin()">Thanh toán</v-btn>
                        </v-row>
                    </v-card>
                </v-col>
            </v-row>
            <login-view
                :dialogLogin="dialogLogin"
                @close="dialogLogin=flase"
            />
        </v-container>
        <v-footer class="bg-black">
            <v-row justify="center" no-gutters>
                <v-btn
                    v-for="link in links"
                    :key="link"
                    color="white"
                    variant="text"
                    class="mx-2"
                    rounded="xl"
                >
                {{ link }}
                </v-btn>
                <v-col class="text-center mt-4" cols="12">
                {{ new Date().getFullYear() }} — <strong>WOODPRO</strong>
                </v-col>
            </v-row>
        </v-footer>
    </v-app>
</template>

<script>
import NavBar from '../../components/NavBar.vue'
import router from '@/router'
import { mapActions, mapGetters } from 'vuex'
import LoginView from '../Customer/LoginView.vue'
export default {
    components: { NavBar, LoginView },
    data(){
        return{
            links: ['Home', 'About Us', 'Team', 'Services', 'Blog', 'Contact Us'],
            total: 0,
            dialogLogin: false
        }
    },
    methods:{
        ...mapActions(['listProductByCat','addQuantity','removeItem','minusQuantity']),
        updateTotal(){
            this.total = 0;
            for(let i = 0; i < this.cart.length; i++){
                this.total += this.cart[i].item.price*this.cart[i].quantity;
            }
        },
        checkLogin(){
            if(this.isCustomerLogin == false){
                this.dialogLogin = true;
            }else{
                router.push('/customer/checkout');
            }
        }
    },
    computed:{
        ...mapGetters(['categories','cart','isCustomerLogin'])
    },
    mounted(){
        this.updateTotal();
    }
}
</script>

<style>

</style>