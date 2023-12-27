<template>
    <v-app id="inspire">
        <side-bar/>
        <top-bar/>
        <v-main style="background-color: #EEEEEE;">
            <v-container class="py-8 px-6" fluid>
                <h3>Đơn hàng</h3>
                <v-divider></v-divider>
                <br>
                <v-container style="background-color: white;">
                    <v-tabs v-model="step" align-tabs="center">
                        <v-tab :value="1">Đang xử lý</v-tab>
                        <v-tab :value="2">Đang vận chuyển</v-tab>
                        <v-tab :value="3">Thành công</v-tab>
                    </v-tabs>
                    <v-window v-model="step">
                        <v-window-item :value="1">
                            <v-data-iterator :items="waitOrder" :page="page1" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #FB8C00;">STT</th>
                                                <th style="color: #FB8C00;">Người nhận</th>
                                                <th style="color: #FB8C00;">Ngày đặt</th>
                                                <th style="color: #FB8C00;">Tổng tiền</th>
                                                <th style="color: #FB8C00;">Thao tác</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.customerName }}</td>
                                                <td>{{ o.raw.dateOrder.slice(0,10) }}</td>
                                                <td>{{ o.raw.totalOrder }} vnd</td>
                                                <td>
                                                    <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = o.raw">
                                                        <v-icon>mdi-eye</v-icon>
                                                    </v-btn>
                                                    <v-btn icon size="small" variant="text" @click="dialogHandle=true, currentOrder = o.raw">
                                                        <v-icon>mdi-arrow-right</v-icon>
                                                    </v-btn>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page1"
                                        :length="waitOrder.length/9 + 1"
                                        style="background-color: white"
                                    ></v-pagination>
                                </template>
                            </v-data-iterator>
                        </v-window-item>
                        <v-window-item :value="2">
                            <v-data-iterator :items="tranferOrder" :page="page2" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #FB8C00;">STT</th>
                                                <th style="color: #FB8C00;">Người nhận</th>
                                                <th style="color: #FB8C00;">Ngày đặt</th>
                                                <th style="color: #FB8C00;">Tổng tiền</th>
                                                <th style="color: #FB8C00;">Thao tác</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.customerName }}</td>
                                                <td>{{ o.raw.dateOrder.slice(0,10) }}</td>
                                                <td>{{ o.raw.totalOrder }} vnd</td>
                                                <td>
                                                    <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = o.raw">
                                                        <v-icon>mdi-eye</v-icon>
                                                    </v-btn>
                                                    <v-btn icon size="small" variant="text" @click="dialogSuccess=true, currentOrder = o.raw">
                                                        <v-icon>mdi-arrow-right</v-icon>
                                                    </v-btn>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page2"
                                        :length="tranferOrder.length/9 + 1"
                                        style="background-color: white"
                                    ></v-pagination>
                                </template>
                            </v-data-iterator>
                        </v-window-item>
                        <v-window-item :value="3">
                            <v-data-iterator :items="successOrder" :page="page3" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #FB8C00;">STT</th>
                                                <th style="color: #FB8C00;">Người nhận</th>
                                                <th style="color: #FB8C00;">Ngày đặt</th>
                                                <th style="color: #FB8C00;">Tổng tiền</th>
                                                <th style="color: #FB8C00;">Thao tác</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.customerName }}</td>
                                                <td>{{ o.raw.dateOrder.slice(0,10) }}</td>
                                                <td>{{ o.raw.totalOrder }} vnd</td>
                                                <td>
                                                    <v-btn icon size="small" variant="text" @click="dialogDetail=true, currentOrder = o.raw">
                                                        <v-icon>mdi-eye</v-icon>
                                                    </v-btn>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page3"
                                        :length="successOrder.length/9 + 1"
                                        :total-visible="5"
                                        style="background-color: white"
                                    ></v-pagination>
                                </template>
                            </v-data-iterator>
                        </v-window-item>
                    </v-window>
                </v-container>
            </v-container>
            <order-detail-view
                :dialogDetail="dialogDetail"
                :currentOrder="currentOrder"
                @close="dialogDetail=false"
            />
            <v-dialog v-model="dialogHandle" max-width="350px">
                <v-card>
                    <v-card-text class="text-center">Đơn hàng đã xử lý xong?</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green" @click="handleNext()">Đồng ý</v-btn>
                        <v-btn color="red" @click="dialogHandle=false">Hủy bỏ</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
            <v-dialog v-model="dialogSuccess" max-width="400px">
                <v-card>
                    <v-card-text class="text-center">Đơn hàng đã vận chuyển thành công?</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green" @click="handleSuccess()">Đồng ý</v-btn>
                        <v-btn color="red" @click="dialogSuccess=false">Hủy bỏ</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-main>
    </v-app>
</template>

<script>
import SideBar from '../../components/SideBar.vue'
import TopBar from '../../components/TopBar.vue'
import OrderDetailView from '../Admin/OrderDetailView.vue'
import router from '@/router'
import { mapActions, mapGetters } from 'vuex'
import axios from 'axios'
export default {
    components: { SideBar, TopBar, OrderDetailView },
    data(){
        return{
            step: null,
            dialogDetail: false,
            dialogHandle: false,
            dialogSuccess: false,
            orderId: '',
            currentOrder: '',
            page1: 1,
            page2: 1,
            page3: 1
        }
    },
    computed:{
        ...mapGetters(['isAdminLogin','orders','token']),
        waitOrder: function(){
            var arr = this.orders.filter(x => x.status === 1);
            return arr;
        },
        tranferOrder: function(){
            var arr = this.orders.filter(x => x.status === 2);
            return arr;
        },
        successOrder: function(){
            var arr = this.orders.filter(x => x.status === 3);
            return arr;
        },
    },
    methods:{
        ...mapActions(['initOrders','initOrderDetails']),
        checkLogin(){
            if(this.isAdminLogin == false){
                router.push('/admin/login');
            }
        },
        handleNext(){
            this.currentOrder.status = 2;
            axios.put('https://localhost:44309/api/Order/' + this.currentOrder.id, this.currentOrder, {headers: {'Authorization': 'Bearer ' + this.token}})
            .then(response =>{
                console.log(response.status);
                this.initOrders();
                this.dialogHandle = false;
            }).catch(e =>{
                console.log(e);
            })
        },
        handleSuccess(){
            this.currentOrder.status = 3;
            axios.put('https://localhost:44309/api/Order/' + this.currentOrder.id, this.currentOrder, {headers: {'Authorization': 'Bearer ' + this.token}})
            .then(response =>{
                console.log(response.status);
                this.initOrders();
                this.dialogSuccess = false;
            }).catch(e =>{
                console.log(e);
            })
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