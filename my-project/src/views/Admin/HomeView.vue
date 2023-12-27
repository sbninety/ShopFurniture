<template>
    <v-app id="inspire">
        <side-bar/>
        <top-bar/>
        <v-main style="background-color: #EEEEEE;">
            <v-container class="py-8 px-6" fluid>
                <h3>Tổng quan</h3>
                <v-divider></v-divider>
                <v-row class="mt-5">
                    <v-col cols="12" lg="4">
                        <v-card elevation="2" class="rounded-lg">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <v-row>
                                    <v-col md="4">
                                        <v-avatar color="red" size="80" variant="flat" rounded="0">
                                            <v-icon size="60">
                                                mdi-sofa
                                            </v-icon>
                                        </v-avatar>    
                                    </v-col>
                                    <v-col md="8">
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <h3>Tổng sản phẩm</h3>
                                            <v-divider></v-divider>
                                        </v-row>
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <p>Số lượng: {{ products.length }}</p>
                                        </v-row>
                                    </v-col>
                                </v-row>
                            </v-card-text>
                        </v-card>
                    </v-col>
                    <v-col cols="12" lg="4">
                        <v-card elevation="2" class="rounded-lg">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <v-row>
                                    <v-col md="4">
                                        <v-avatar color="#48A54C" size="80" variant="flat" rounded="0"> 
                                            <v-icon size="60">
                                                mdi-order-bool-descending-variant
                                            </v-icon>
                                        </v-avatar>    
                                    </v-col>
                                    <v-col md="8">
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <h3>Tổng đơn hàng</h3>
                                            <v-divider></v-divider>
                                        </v-row>
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <p>Số lượng: {{ orders.length }}</p>
                                        </v-row>
                                    </v-col>
                                </v-row>
                            </v-card-text>
                        </v-card>
                    </v-col>
                    <v-col cols="12" lg="4">
                        <v-card elevation="2" class="rounded-lg">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <v-row>
                                    <v-col md="4">
                                        <v-avatar color="#00CAE3" size="80" variant="flat" rounded="0">
                                            <v-icon size="60" color="#ffffff">
                                                mdi-account-group
                                            </v-icon>
                                        </v-avatar>    
                                    </v-col>
                                    <v-col md="8">
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <h3>Tổng khách hàng</h3>
                                            <v-divider></v-divider>
                                        </v-row>
                                        <v-row class="mr-2 mt-5">
                                            <v-spacer></v-spacer>
                                            <p>Số lượng: {{ customers.length }}</p>
                                        </v-row>
                                    </v-col>
                                </v-row>
                            </v-card-text>
                        </v-card>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" lg="6">
                        <v-card color="warning" height="50px" width="300px" elevation="4" style="margin: auto;">
                            <v-card-title class="text-center">KHÁCH HÀNG</v-card-title>
                        </v-card>
                        <v-table style="padding-top: 50px; margin-top: -30px;">
                            <tbody>
                                <v-divider style="margin-left: 100px;"></v-divider>
                                <tr>
                                    <td>Khách hàng tiềm năng: </td>
                                    <td>{{ oldCustomer.length }}</td>
                                </tr>
                                <v-divider></v-divider>
                                <tr>
                                    <td>Khách hàng mới: </td>
                                    <td>{{ newCustomer.length }}</td>
                                </tr>
                            </tbody>
                        </v-table>
                    </v-col>
                    <v-col cols="12" lg="6">
                        <v-card color="#E91E63" height="50px" width="300px" elevation="4" style="margin: auto;">
                            <v-card-title class="text-center">ĐƠN HÀNG</v-card-title>
                        </v-card>
                        <v-table style="padding-top: 50px; margin-top: -30px;">
                            <tbody>
                                <v-divider style="margin-left: 100px;"></v-divider>
                                <tr>
                                    <td>Đơn hàng chờ xử lý: </td>
                                    <td>{{ waitOrder }}</td>
                                </tr>
                                <v-divider></v-divider>
                                <tr>
                                    <td>Đơn hàng thành công: </td>
                                    <td>{{ successOrder }}</td>
                                </tr>
                            </tbody>
                        </v-table>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-card color="#7784C8" height="50px" width="300px" elevation="4" style="margin: auto;">
                            <v-card-title class="text-center">DOANH THU NĂM 2023</v-card-title>
                        </v-card>
                        <v-table style="padding-top: 50px; margin-top: -30px;">
                            <tbody>
                                <tr>
                                    <v-timeline direction="horizontal" side="end" style="padding-bottom: 30px;">
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#F44336">
                                            <template v-slot:opposite>
                                                {{ month1 }} vnd
                                            </template>
                                            <b>Tháng 1</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#673AB7">
                                            <template v-slot:opposite>
                                                {{ month2 }} vnd
                                            </template>
                                            <b>Tháng 2</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#03A9F4">
                                            <template v-slot:opposite>
                                                {{ month3 }} vnd
                                            </template>
                                            <b>Tháng 3</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#4CAF50">
                                            <template v-slot:opposite>
                                                {{ month4 }} vnd
                                            </template>
                                            <b>Tháng 4</b>
                                        </v-timeline-item>
                                    </v-timeline>
                                </tr>
                                <br><br>
                                <tr>
                                    <v-timeline direction="horizontal" side="end" style="padding-bottom: 30px;">
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#FFEB3B">
                                            <template v-slot:opposite>
                                                {{ month5 }} vnd
                                            </template>
                                            <b>Tháng 5</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#FF5722">
                                            <template v-slot:opposite>
                                                {{ month6 }} vnd
                                            </template>
                                            <b>Tháng 6</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#607D8B">
                                            <template v-slot:opposite>
                                                {{ month7 }} vnd
                                            </template>
                                            <b>Tháng 7</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#E91E63">
                                            <template v-slot:opposite>
                                                {{ month8 }} vnd
                                            </template>
                                            <b>Tháng 8</b>
                                        </v-timeline-item>
                                    </v-timeline>
                                </tr>
                                <br><br>
                                <tr>
                                    <v-timeline direction="horizontal" side="end" style="padding-bottom: 30px;">
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#3F51B5">
                                            <template v-slot:opposite>
                                                {{ month9 }} vnd
                                            </template>
                                            <b>Tháng 9</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#00BCD4">
                                            <template v-slot:opposite>
                                                {{ month10 }} vnd
                                            </template>
                                            <b>Tháng 10</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#8BC34A">
                                            <template v-slot:opposite>
                                                {{ month11 }} vnd
                                            </template>
                                            <b>Tháng 11</b>
                                        </v-timeline-item>
                                        <v-timeline-item size="small" width="150px" class="text-center" dot-color="#FFC107">
                                            <template v-slot:opposite>
                                                {{ month12 }} vnd
                                            </template>
                                            <b>Tháng 12</b>
                                        </v-timeline-item>
                                    </v-timeline>
                                </tr>
                            </tbody>
                        </v-table>
                    </v-col>
                </v-row>
            </v-container>
        </v-main>
    </v-app>
</template>

<script>
import SideBar from '../../components/SideBar.vue'
import TopBar from '../../components/TopBar.vue'
import { mapActions, mapGetters } from 'vuex'
import router from '@/router'
export default {
    components: { SideBar, TopBar },
    computed:{
        ...mapGetters(['products','isAdminLogin','orders','customers']),
        waitOrder: function(){
            var arr = this.orders.filter(x => x.status === 1);
            return arr.length;
        },
        successOrder: function(){
            var arr = this.orders.filter(x => x.status === 3);
            return arr.length;
        },
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
        month12: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 12 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month1: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 1 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month2: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 2 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month3: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 3 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month4: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 4 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month5: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 5 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month6: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 6 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month7: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 7 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month8: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 8 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month9: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 9 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month10: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 10 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        },
        month11: function(){
            var sum = 0;
            for(let i = 0; i < this.orders.length; i++){
                if(this.orders[i].dateOrder.slice(5,7) == 11 && this.orders[i].status == 3){
                    sum += this.orders[i].totalOrder;
                }
            }
            return sum;
        }
    },
    methods:{
        ...mapActions(['loadProducts','initOrders','initUsers']),
        checkLogin(){
            if(this.isAdminLogin == false){
                router.push('/admin/login');
            }
        }
    },
    created(){
        this.checkLogin();
        this.loadProducts();
        this.initOrders();
        this.initUsers();
    }
}
</script>

<style>

</style>