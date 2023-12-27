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
                    <h2>ĐƠN HÀNG CỦA BẠN</h2>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="4">
                    <v-card>
                        <v-card-title>
                            Thông tin cá nhân
                            <v-btn icon size="x-small" style="margin-left: 130px;" variant="outlined">
                                <v-icon>mdi-pencil</v-icon>
                            </v-btn>
                        </v-card-title>
                        <v-card-text class="mt-2">
                            <v-row>
                                <v-col align="center">
                                    <h3 class="pt-5">id: {{ customer.account }}</h3>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col align="right">
                                    <h4>Tên khách hàng: </h4>
                                </v-col>
                                <v-col>
                                    <p>{{ customer.name }}</p>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col align="right">
                                    <h4>Email: </h4>
                                </v-col>
                                <v-col>
                                    <p>{{ customer.email }}</p>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col align="right">
                                    <h4>Số điện thoại: </h4>
                                </v-col>
                                <v-col>
                                    <p>{{ customer.phone }}</p>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col align="right">
                                    <h4>Địa chỉ: </h4>
                                </v-col>
                                <v-col>
                                    <p>{{ customer.address }}</p>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-card>
                </v-col>
                <v-col cols="8">
                    <v-container style="background-color: white;">
                        <v-tabs v-model="step" align-tabs="center">
                            <v-tab :value="1">Đang xử lý</v-tab>
                            <v-tab :value="2">Đang vận chuyển</v-tab>
                            <v-tab :value="3">Đã nhận hàng</v-tab>
                        </v-tabs>
                        <v-window v-model="step">
                            <v-window-item :value="1">
                                <v-table class="mt-5">
                                    <thead>
                                        <th></th>
                                        <th>Người nhận</th>
                                        <th>Ngày đặt</th>
                                        <th>Tổng tiền</th>
                                        <th>Thao tác</th>
                                    </thead>
                                    <tbody align="center">
                                        <tr v-for="(order, i) in statusOne" :key="i">
                                            <td>{{ i+1 }}</td>
                                            <td>{{ order.customerName }}</td>
                                            <td>{{ order.dateOrder.slice(0,10) }}</td>
                                            <td>{{ order.totalOrder }} vnd</td>
                                            <td>
                                                <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = order">
                                                    <v-icon>mdi-eye</v-icon>
                                                </v-btn>
                                                <v-btn icon size="small" variant="text" @click="dialogCancel=true, orderId = order.id">
                                                    <v-icon>mdi-window-close</v-icon>
                                                </v-btn>
                                            </td>
                                        </tr>
                                    </tbody>
                                </v-table>
                            </v-window-item>
                            <v-window-item :value="2">
                                <v-table class="mt-5">
                                    <thead>
                                        <th></th>
                                        <th>Người nhận</th>
                                        <th>Ngày đặt</th>
                                        <th>Tổng tiền</th>
                                        <th>Thao tác</th>
                                    </thead>
                                    <tbody align="center">
                                        <tr v-for="(order, i) in statusTwo" :key="i">
                                            <td>{{ i+1 }}</td>
                                            <td>{{ order.customerName }}</td>
                                            <td>{{ order.dateOrder.slice(0,10) }}</td>
                                            <td>{{ order.totalOrder }} vnd</td>
                                            <td>
                                                <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = order">
                                                    <v-icon>mdi-eye</v-icon>
                                                </v-btn>
                                            </td>
                                        </tr>
                                    </tbody>
                                </v-table>
                            </v-window-item>
                            <v-window-item :value="3">
                                <v-table class="mt-5">
                                    <thead>
                                        <th></th>
                                        <th>Người nhận</th>
                                        <th>Ngày đặt</th>
                                        <th>Tổng tiền</th>
                                        <th>Thao tác</th>
                                    </thead>
                                    <tbody align="center">
                                        <tr v-for="(order, i) in statusThree" :key="i">
                                            <td>{{ i+1 }}</td>
                                            <td>{{ order.customerName }}</td>
                                            <td>{{ order.dateOrder.slice(0,10) }}</td>
                                            <td>{{ order.totalOrder }} vnd</td>
                                            <td>
                                                <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = order">
                                                    <v-icon>mdi-eye</v-icon>
                                                </v-btn>
                                            </td>
                                        </tr>
                                    </tbody>
                                </v-table>
                            </v-window-item>
                        </v-window>
                    </v-container>
                </v-col>
            </v-row>
        </v-container>
        <order-detail-view
            :dialogDetail="dialogDetail"
            :currentOrder="currentOrder"
            @close="dialogDetail=false"
        />
        <login-view
            :dialogLogin="dialogLogin"
            @close="dialogLogin=false"
        />
        <v-dialog v-model="dialogCancel" max-width="400px">
            <v-card>
                <v-card-text class="text-center">Bạn có muốn hủy đơn hàng này không?</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green" @click="deleteOrder(orderId),dialogCancel=false">Đồng ý</v-btn>
                    <v-btn color="red" @click="dialogCancel=false">Hủy bỏ</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
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
import { mapActions, mapGetters } from 'vuex'
import OrderDetailView from './OrderDetailView.vue'
import LoginView from '../Customer/LoginView.vue'
export default {
    components: { NavBar, OrderDetailView, LoginView },
    data(){
        return{
            step: null,
            dialogDetail: false,
            dialogCancel: false,
            dialogLogin: false,
            links: ['Home', 'About Us', 'Team', 'Services', 'Blog', 'Contact Us'],
            listOrder: [],
            orderId: '',
            currentOrder: ''
        }
    },
    methods:{
        ...mapActions(['listProductByCat','initOrderDetails','initOrders','deleteOrder']),
        checkLogin(){
            if(this.isCustomerLogin == false){
                this.dialogLogin = true;
            }
        }
    },
    computed:{
        ...mapGetters(['categories','customer','isCustomerLogin','orders']),
        statusOne: function () {
            return this.orders.filter(i => i.status === 1 && i.userId == this.customer.id);
        },
        statusTwo: function () {
            return this.orders.filter(i => i.status === 2 && i.userId == this.customer.id);
        },
        statusThree: function(){
            return this.orders.filter(i => i.status === 3 && i.userId == this.customer.id);
        }
    },
    created(){
        this.checkLogin();
        this.initOrders();
        this.initOrderDetails();
    }
}
</script>

<style>

</style>