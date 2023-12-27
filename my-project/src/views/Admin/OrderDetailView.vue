<template>
    <div>
        <v-container>
            <v-dialog
                max-width="940px"
                v-model="dialog" 
            >
                <v-card>
                    <v-card-title class="text-center mt-3">
                        <b>CHI TIẾT ĐƠN HÀNG</b>
                    </v-card-title>
                    <v-card-text>
                        <v-row>
                            <v-spacer></v-spacer>
                            <v-col cols="5">
                                <p><b>Tên người nhận: </b>{{ order.customerName }}</p>
                            </v-col>
                            <v-col cols="5">
                                <p><b>Số điện thoại: </b>{{ order.customerPhone }}</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-spacer></v-spacer>
                            <v-col cols="5">
                                <p><b>Địa chỉ: </b>{{ order.customerAddress }}</p>
                            </v-col>
                            <v-col cols="5">
                                <p><b>Email: </b>{{ order.customerEmail }}</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-spacer></v-spacer>
                            <v-col cols="5">
                                <p><b>Ngày đặt hàng: </b>{{ order.dateOrder.slice(0,10) }}</p>
                            </v-col>
                            <v-col cols="5">
                                <p><b>Tổng tiền: </b>{{ order.totalOrder }} vnd</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-card class="mt-3" v-for="o in listOrderDetail" :key="o">
                                    <v-row class="mt-2 ml-2 mb-2">
                                        <v-col cols="3">
                                            <v-img :src="o.product.imageUrl"></v-img>
                                        </v-col>
                                        <v-col cols="8">
                                            <v-row class="mt-1 ml-1">
                                                <h3>{{ o.product.productName }}</h3>
                                            </v-row>
                                            <v-row>
                                                <v-col cols="6">
                                                    <p class="mt-2">Kich thuoc: {{ o.product.size }}</p>
                                                    <p class="mt-2">Chat lieu: {{ o.product.material }}</p>
                                                    <p class="mt-2">Mau sac: {{ o.product.color }}</p>
                                                </v-col>
                                                <v-col cols="6">
                                                    <p class="mt-2">Số lượng: {{ o.quantity }}</p>
                                                    <p class="mt-2">Giá tiền: {{ o.product.price*o.quantity }} vnd</p>
                                                </v-col>
                                            </v-row>
                                        </v-col>
                                    </v-row>
                                </v-card>
                            </v-col>
                        </v-row>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="$emit('close')" color="red">Đóng</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-container>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
    props: ['dialogDetail','currentOrder'],
    data(){
        return{
            listOrderDetail: [],
            order:{
                id: 0,
                userId: 0,
                customerName: '',
                dateOrder: '',
                customerPhone: '',
                customerEmail: '',
                customerAddress: '',
                totalOrder: 0,
                status: 0
            }
        }
    },
    methods:{
        setOrderDetail(){
            this.listOrderDetail = [];
            for(let i = 0; i < this.orderDetails.length; i++){
                if(this.orderDetails[i].orderId == this.order.id){
                    this.listOrderDetail.push(this.orderDetails[i]);
                }
            }
            for(let i = 0; i < this.listOrderDetail.length; i++){
                var product = this.products.filter(x => x.id == this.listOrderDetail[i].productId);
                this.listOrderDetail[i] = {...this.listOrderDetail[i],...{product: product[0]}};
            }
        }
    },
    computed:{
        dialog:{
            get(){
                return this.dialogDetail;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        },
        ...mapGetters(['orderDetails','products'])
    },
    watch:{
        currentOrder:function(){
            this.order.id = this.currentOrder.id;
            this.order.userId = this.currentOrder.userId;
            this.order.customerName = this.currentOrder.customerName;
            this.order.customerAddress = this.currentOrder.customerAddress;
            this.order.customerEmail = this.currentOrder.customerEmail;
            this.order.customerPhone = this.currentOrder.customerPhone;
            this.order.totalOrder = this.currentOrder.totalOrder;
            this.order.status = this.currentOrder.status;
            this.order.dateOrder = this.currentOrder.dateOrder;
        }
    },
    updated(){
        this.setOrderDetail();
    }
}
</script>

<style>

</style>