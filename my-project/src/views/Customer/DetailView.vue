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
            <v-row class="mt-5">
                <v-col>
                    <v-carousel hide-delimiters>
                        <v-carousel-item
                            v-for="(item,i) in listImages"
                            :key="i"
                            :src="item.imageUrl"
                            cover
                        ></v-carousel-item>
                    </v-carousel>
                </v-col>
                <v-col class="ml-4">
                    <v-row class="mt-1">
                        <h3 style="color: red;">{{ product.productName }}</h3>
                    </v-row>
                    <v-row>
                        <h2>{{ product.price }}</h2><span>đ</span>
                    </v-row>
                    <v-row>
                        <v-card variant="outlined" width="100%" height="100%" class="mr-5 pl-3 pt-3">
                            <p>Kích thước (Ngang x Cao x Sâu) (cm):</p>
                            <v-btn class="mt-2 mb-2" variant="outlined" color="#e65100">
                                {{ product.size }}
                            </v-btn>
                            <p>Chất liệu:</p>
                            <v-btn class="mt-2 mb-2" variant="outlined" color="#e65100">
                                {{ product.material }}
                            </v-btn>
                            <p>Màu sắc:</p>
                            <v-btn class="mt-2 mb-5" variant="outlined" color="#e65100">
                                {{ product.color }}
                            </v-btn>
                        </v-card>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-btn variant="flat" color="orange" width="95%" to="/customer/shoppingcart" @click="addToCart(product)"><p style="color: white;font-weight: bold;">Mua ngay</p></v-btn>
                        </v-col>
                        <v-col>
                            <v-btn variant="outlined" width="95%" color="orange" @click="dialog=true,addToCart(product)">Thêm vào giỏ</v-btn>
                        </v-col>
                    </v-row>
                    <v-row class="mt-5 pt-3" style="border-top: 1px solid black; width: 100%; font-size: 15px;">
                        <v-icon color="green">mdi mdi-check</v-icon>
                        <p><b>Chất liệu: </b>{{ product.material }}.</p>
                    </v-row>
                    <v-row style="width: 100%; font-size: 15px;">
                        <v-icon color="green">mdi mdi-check</v-icon>
                        <p><b>Tình trạng: </b>Hàng mới 100%.</p>
                    </v-row>
                    <v-row style="width: 100%; font-size: 15px;">
                        <v-icon color="green">mdi mdi-check</v-icon>
                        <p><b>Trạng thái: </b>Còn hàng.</p>
                    </v-row>
                    <v-row style="width: 100%; font-size: 15px;">
                        <v-icon color="green">mdi mdi-check</v-icon>
                        <p><b>Chi phí giao hàng: </b>Miễn phí.</p>
                    </v-row>
                    <v-row style="width: 100%; font-size: 15px;">
                        <v-icon color="green">mdi mdi-check</v-icon>
                        <p><b>Thời gian giao hàng: </b>Từ 6 giờ đến 10 ngày làm việc.</p>
                    </v-row>
                </v-col>
            </v-row>
            <v-dialog v-model="dialog" max-width="550px">
                <v-card>
                    <v-card-title class="text-right">
                        <v-btn icon @click="dialog=false" variant="text">
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

                        <div class="text-h4 font-weight-bold">Thêm giỏ hàng thành công</div>
                        </div>
                    </v-card-text>
                </v-card>
            </v-dialog>
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
import { mapActions, mapGetters } from 'vuex'
export default {
    components: { NavBar },
    data(){
        return{
            dialog: false,
            links: ['Home', 'About Us', 'Team', 'Services', 'Blog', 'Contact Us'],
            listImages: []
        }
    },
    methods:{
        ...mapActions(['listProductByCat','getProduct','addToCart'])
    },
    computed:{
        ...mapGetters(['categories','product','productImages'])
    },
    created(){
        this.getProduct(this.$route.params.id);
        this.listImages = this.productImages.filter(item => item.productId == this.$route.params.id);
    }
}
</script>

<style>

</style>