<template>
    <v-app id="inspire">
        <side-bar/>
        <top-bar/>
        <v-main style="background-color: #EEEEEE;">
            <v-container class="py-8 px-6" fluid>
                <h3>Khách hàng</h3>
                <v-divider></v-divider>
                <br>
                <v-container style="background-color: white;">
                    <v-tabs v-model="step" align-tabs="center">
                        <v-tab :value="1">Tất cả</v-tab>
                        <v-tab :value="2">Khách hàng tiềm năng</v-tab>
                        <v-tab :value="3">Khách hàng mới</v-tab>
                    </v-tabs>
                    <v-window v-model="step">
                        <v-window-item :value="1">
                            <v-data-iterator :items="allCustomer" :page="page1" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #E91E63;">STT</th>
                                                <th style="color: #E91E63;">Tên khách hàng</th>
                                                <th style="color: #E91E63;">Số điện thoại</th>
                                                <th style="color: #E91E63;">Số đơn hàng</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.name }}</td>
                                                <td>{{ o.raw.phone }}</td>
                                                <td>{{ o.raw.totalOrder }}</td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page1"
                                        :length="allCustomer.length/9 + 1"
                                        style="background-color: white"
                                    ></v-pagination>
                                </template>
                            </v-data-iterator>
                        </v-window-item>
                        <v-window-item :value="2">
                            <v-data-iterator :items="oldCustomer" :page="page2" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #E91E63;">STT</th>
                                                <th style="color: #E91E63;">Tên khách hàng</th>
                                                <th style="color: #E91E63;">Số điện thoại</th>
                                                <th style="color: #E91E63;">Số đơn hàng</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.name }}</td>
                                                <td>{{ o.raw.phone }}</td>
                                                <td>{{ o.raw.totalOrder }}</td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page2"
                                        :length="oldCustomer.length/9 + 1"
                                        style="background-color: white"
                                    ></v-pagination>
                                </template>
                            </v-data-iterator>
                        </v-window-item>
                        <v-window-item :value="3">
                            <v-data-iterator :items="newCustomer" :page="page3" :items-per-page="9">
                                <template v-slot:default="{ items }">
                                    <v-table class="mt-5">
                                        <thead>
                                            <tr>
                                                <th style="color: #E91E63;">STT</th>
                                                <th style="color: #E91E63;">Tên khách hàng</th>
                                                <th style="color: #E91E63;">Số điện thoại</th>
                                                <th style="color: #E91E63;">Số đơn hàng</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(o,i) in items" :key="i">
                                                <td>{{ i+1 }}</td>
                                                <td>{{ o.raw.name }}</td>
                                                <td>{{ o.raw.phone }}</td>
                                                <td>{{ o.raw.totalOrder }}</td>
                                            </tr>
                                        </tbody>
                                    </v-table>
                                    <v-divider></v-divider>
                                    <v-pagination
                                        v-model="page3"
                                        :length="newCustomer.length/9 + 1"
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
                @close="dialogDetail=false"
            />
            <v-dialog v-model="dialogHandle" max-width="350px">
                <v-card>
                    <v-card-text class="text-center">Đơn hàng đã xử lý xong?</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green">Đồng ý</v-btn>
                        <v-btn color="red" @click="dialogHandle=false">Hủy bỏ</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
            <v-dialog v-model="dialogSuccess" max-width="400px">
                <v-card>
                    <v-card-text class="text-center">Đơn hàng đã vận chuyển thành công?</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green">Đồng ý</v-btn>
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
import { mapGetters } from 'vuex'
export default {
    components: { SideBar, TopBar, OrderDetailView },
    data(){
        return{
            step: null,
            dialogDetail: false,
            dialogHandle: false,
            dialogSuccess: false,
            page1: 1,
            page2: 1,
            page3: 1
        }
    },
    computed:{
        ...mapGetters(['isAdminLogin','customers','orders']),
        oldCustomer: function(){
            var cus = [];
            for(let i = 0; i < this.customers.length; i++){
                var arr = this.orders.filter(x => x.userId == this.customers[i].id);
                if(arr.length > 10){
                    cus.push({...this.customers[i],totalOrder: arr.length});
                }
            }
            return cus;
        },
        newCustomer: function(){
            var cus = [];
            for(let i = 0; i < this.customers.length; i++){
                var arr = this.orders.filter(x => x.userId == this.customers[i].id);
                if(arr.length < 5){
                    cus.push({...this.customers[i],totalOrder: arr.length});
                }
            }
            return cus;
        },
        allCustomer: function(){
            var cus = [];
            for(let i = 0; i < this.customers.length; i++){
                var arr = this.orders.filter(x => x.userId == this.customers[i].id);
                cus.push({...this.customers[i],totalOrder: arr.length});
            }
            return cus;
        }
    },
    methods:{
        checkLogin(){
            if(this.isAdminLogin == false){
                router.push('/admin/login');
            }
        }
    },
    created(){
        this.checkLogin();
    }
}
</script>

<style>

</style>