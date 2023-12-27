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
                    <h2>CHECK OUT</h2>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="8">
                    <h3>Thông tin khách hàng</h3>
                    <v-card class="mt-3">
                        <v-card-text>
                            <v-row>
                                <v-col>
                                    <v-text-field label="Họ tên" v-model="infor.customerName" :rules="[rules.required]"></v-text-field>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col>
                                    <v-text-field label="Địa chỉ" v-model="infor.customerAddress" :rules="[rules.required]"></v-text-field>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col>
                                    <v-text-field label="Email" v-model="infor.customerEmail" :rules="[rules.required, rules.email]"></v-text-field>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col>
                                    <v-text-field label="Số điện thoại" v-model="infor.customerPhone" :rules="[rules.required, rules.phone]"></v-text-field>
                                </v-col>
                            </v-row>
                        </v-card-text>
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
                            <v-btn color="blue" block @click="handleBuy()">Mua hàng</v-btn>
                        </v-row>
                    </v-card>
                </v-col>
            </v-row>
            <v-dialog v-model="dialogSuccess" max-width="550px">
                <v-card>
                <v-card-title class="text-right">
                    <v-btn icon @click="loadProducts" to="/" variant="text">
                    <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <div class="py-12 text-center">
                        <v-icon
                            class="mb-6"
                            color="success"
                            icon="mdi-check-circle-outline"
                            size="128"
                        ></v-icon>
                        <div class="text-h4 font-weight-bold">Đặt hàng thành công</div>
                    </div>
                </v-card-text>
                </v-card>
            </v-dialog>
            <v-dialog v-model="dialogError" max-width="550px">
                <v-card>
                <v-card-title class="text-right">
                    <v-btn icon @click="dialogError=false" variant="text">
                    <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <div class="py-12 text-center">
                        <v-icon
                            class="mb-6"
                            color="error"
                            icon="mdi-alert-circle-outline"
                            size="128"
                        ></v-icon>
                        <div class="text-h4 font-weight-bold">Vui lòng điền đầy đủ các bước mua hàng</div>
                    </div>
                </v-card-text>
                </v-card>
            </v-dialog>
            <login-view
                :dialogLogin="dialogLogin"
                @close="dialogLogin=false"
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
import LoginView from '../Customer/LoginView.vue'
import { mapActions, mapGetters } from 'vuex'
import axios from 'axios'
export default {
    components: { NavBar, LoginView },
    data(){
        return{
            links: ['Home', 'About Us', 'Team', 'Services', 'Blog', 'Contact Us'],
            total: 0,
            infor: {
                id: 0,
                userId: 0,
                customerName: '',
                dateOrder: '',
                customerPhone: '',
                customerEmail: '',
                customerAddress: '',
                totalOrder: '',
                status: 1,
            },
            dialogSuccess: false,
            dialogError: false,
            dialogLogin: false,
            rules: {
                required: value => !!value || 'Bắt buộc.',
                email: value => {
                    const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                    return pattern.test(value) || 'Bạn phải nhập Email.'
                },
                phone: value => {
                    const pattern = /^\d{10}$/;
                    return pattern.test(value) || 'Bạn phải nhập số điện thoại.'
                }
            }
        }
    },
    methods:{
        ...mapActions(['listProductByCat','setCart','loadProducts']),
        calcTotal(){
            for(let i = 0; i < this.cart.length; i++){
                this.total += this.cart[i].item.price*this.cart[i].quantity;
            }
        },
        setInfor(){
            var d = new Date();
            var month = d.getMonth() + 1;
            this.infor.customerName = this.customer.name;
            this.infor.dateOrder = d.getFullYear() + '-' + month + '-' + d.getDate();
            this.infor.customerAddress = this.customer.address,
            this.infor.customerEmail = this.customer.email;
            this.infor.customerPhone = this.customer.phone;
            this.infor.totalOrder = this.total;
            this.infor.userId = this.customer.id;
        },
        handleBuy(){
            if(this.isCustomerLogin == false){
                this.dialogLogin = true;
            }else{
                if(this.infor.customerName == '' || this.infor.customerAddress == '' || this.infor.customerEmail == '' || this.infor.customerPhone == '' || this.total == 0){
                    this.dialogError = true;
                }else{
                    axios.post('https://localhost:44309/api/Order', this.infor, {headers: {'Authorization': 'Bearer ' + this.token}})
                    .then(response =>{
                        console.log(response.status);
                        console.log('pass1');
                        axios.get('https://localhost:44309/api/Order/GetLastOrder', {headers: {'Authorization': 'Bearer ' + this.token}})
                        .then(response =>{
                            var order = response.data;
                            console.log('pass2');
                            for(let i = 0; i < this.cart.length; i++){
                                axios.post('https://localhost:44309/api/OrderDetail',{orderId: order.id, productId: this.cart[i].item.id, quantity: this.cart[i].quantity}, {headers: {'Authorization': 'Bearer ' + this.token}})
                                .then(response =>{
                                    console.log(response.status);
                                    console.log('pass3');
                                }).catch(e =>{
                                    console.log(e);
                                })
                            }
                            this.setCart();
                            this.dialogSuccess = true;
                        }).catch(e =>{
                            console.log(e);
                        })
                    }).catch(e =>{
                        console.log(e);
                    })
                }
            }
        },
        checkLogin(){
            if(this.isCustomerLogin == false){
                this.dialogLogin = true;
            }
        }
    },
    computed:{
        ...mapGetters(['categories','cart','customer','isCustomerLogin','token'])
    },
    created(){
        this.checkLogin();
        this.calcTotal();
        this.setInfor();
    }
}
</script>

<style>

</style>