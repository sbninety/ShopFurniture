<template>
    <div>
        <v-container>
            <v-dialog
                max-width="940px"
                v-model="dialog"
            >
                <v-card>
                    <v-card-title>
                        <v-row>
                            <v-col>
                                Chi tiết sản phẩm
                            </v-col>
                            <v-col class="text-right">
                                <v-btn icon size="x-small" class="mr-2" @click="dialogEdit=true;dialog=false">
                                    <v-icon>mdi-pencil</v-icon>
                                </v-btn>
                                <v-btn icon @click="$emit('close')" size="x-small">
                                    <v-icon>mdi-close</v-icon>
                                </v-btn>
                            </v-col>
                        </v-row>
                    </v-card-title>
                    <v-card-text>
                        <v-row>
                            <v-col>
                                <p><b>Tên sản phẩm: </b>{{ product.productName }}</p>
                            </v-col>
                            <v-col>
                                <p><b>Loại sản phẩm: </b>{{ product.categoryName }}</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <p><b>Giá sản phẩm: </b>{{ product.price }} vnd</p>
                            </v-col>
                            <v-col>
                                <p><b>Kích thước: </b>{{ product.size }}</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <p><b>Chất liệu: </b>{{ product.material }}</p>
                            </v-col>
                            <v-col>
                                <p><b>Màu sắc: </b>{{ product.color }}</p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <p><b>Hình ảnh: </b></p>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col
                                cols="12"
                                md="4"
                                v-for="(image,index) in listImage" :key="index"
                            >
                                <v-img :src="image.imageUrl"></v-img>
                            </v-col>
                        </v-row>
                    </v-card-text>
                    <v-card-actions></v-card-actions>
                </v-card>
            </v-dialog>
        </v-container>
        <edit-product-view
            :dialogEdit="dialogEdit"
            @close="dialogEdit=false"
            :curItem="currentItem"
        />
    </div>
</template>

<script>
import EditProductView from './EditProductView.vue';
import { mapActions, mapGetters } from 'vuex'
export default {
  components: { EditProductView },
    props: ['dialogDetail','currentItem'],
    computed:{
        ...mapGetters(['categories','productImages']),
        dialog:{
            get(){
                return this.dialogDetail;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        }
    },
    methods:{
        ...mapActions(['loadProductImages'])
    },
    data(){
        return{
            dialogEdit: false,
            product:{
                id: '',
                categoryId: '',
                categoryName: '',
                productName: '',
                price: '',
                size: '',
                color: '',
                material: ''
            },
            listImage: []
        }
    },
    watch:{
        currentItem:function(){
            this.product.id = this.currentItem.id;
            for(var i = 0; i < this.categories.length; i++ ){
                if(this.currentItem.categoryId == this.categories[i].id){
                    this.product.categoryName = this.categories[i].categoryName;
                }
            }
            this.product.categoryId = this.currentItem.categoryId;
            this.product.productName = this.currentItem.productName;
            this.product.price = this.currentItem.price;
            this.product.size = this.currentItem.size;
            this.product.color = this.currentItem.color;
            this.product.material = this.currentItem.material;
        }
    },
    updated(){
        
        this.listImage = this.productImages.filter(item => item.productId == this.currentItem.id);
    }
}
</script>

<style>

</style>