<template>
    <v-app id="inspire">
        <side-bar/>
        <top-bar/>
        <v-main style="background-color: #EEEEEE;">
            <v-container class="py-8 px-6" fluid>
                <h3>Sản phẩm</h3>
                <v-divider></v-divider>
                <br>
                <v-row>
                    <v-spacer class="ml-3">
                        <v-form @submit.prevent="searchPro(keyword)">
                            <v-text-field
                                v-model="keyword"
                                density="compact" 
                                variant="outlined" 
                                label="Tìm kiếm" 
                                append-inner-icon="mdi-magnify" 
                                single-line hide-details 
                                class="mr-2"
                                style="width: 250px; background-color: #ffffff;">
                            </v-text-field>
                        </v-form>
                    </v-spacer>
                    <v-btn icon size="small" class="mr-4" @click="dialogAdd=true">
                        <v-icon>mdi-plus</v-icon>
                    </v-btn>
                </v-row>
                <v-row>
                    <v-col>
                        <v-data-iterator :items="products" :page="page" :items-per-page="9">
                            <template v-slot:default="{ items }">
                                <v-table>
                                    <thead>
                                        <tr>
                                            <th style="color: #4CAF50;">STT</th>
                                            <th style="color: #4CAF50; padding-left: 40px">Hình ảnh</th>
                                            <th style="color: #4CAF50;">Tên sản phẩm</th>
                                            <th style="color: #4CAF50;">Giá sản phẩm</th>
                                            <th style="color: #4CAF50;">Thao tác</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(product, i) in items" :key="i">
                                            <td>{{ i + 1 }}</td>
                                            <td class="pb-1 pt-1">
                                                <v-img :src="product.raw.imageUrl" width="150" height="100"></v-img>
                                            </td>
                                            <td>{{ product.raw.productName }}</td>
                                            
                                            <td>{{ product.raw.price }}</td>
                                            <td>
                                                <v-btn icon size="x-small" class="mr-3" @click="dialogDetail=true,currentItem=product.raw">
                                                    <v-icon>mdi-eye</v-icon>
                                                </v-btn>
                                            </td>
                                        </tr>
                                    </tbody>
                                </v-table>
                                <v-divider></v-divider>
                                <v-pagination
                                    v-model="page"
                                    :length="products.length/9 + 1"
                                    style="background-color: white"
                                ></v-pagination>
                            </template>
                        </v-data-iterator>
                    </v-col>
                </v-row>
            </v-container>
            <add-product-view
                :dialogAdd="dialogAdd"
                @close="dialogAdd=false"
                @updateData="getProducts"
            />
            <detail-product-view
                :dialogDetail="dialogDetail"
                @close="dialogDetail=false"
                :currentItem="currentItem"
            />
        </v-main>
    </v-app>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import SideBar from '../../components/SideBar.vue'
import TopBar from '../../components/TopBar.vue'
import AddProductView from './AddProductView.vue'
import DetailProductView from './DetailProductView.vue'   
import router from '@/router'
export default {
    components: { SideBar, TopBar, AddProductView, DetailProductView },
    data(){
        return{
            dialogAdd: false,
            dialogDetail: false,
            keyword: '',
            currentItem: '',
            page: 1
        }
    },
    computed:{
        ...mapGetters(['products','isAdminLogin','productImages'])
    },
    methods:{
        ...mapActions(['loadProducts','searchPro','loadProductImages']),
        getProducts(){
            this.loadProductImages();
            this.loadProducts();
        },
        checkLogin(){
            if(this.isAdminLogin == false){
                router.push('/admin/login');
            }
        }
    },
    created(){
        this.checkLogin();
        this.loadProducts();
        this.loadProductImages();
    }
}
</script>

<style>

</style>