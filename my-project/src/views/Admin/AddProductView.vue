<template>
    <div>
        <v-container>
            <v-dialog v-model="dialog" max-width="940px">
                <v-card>
                    <v-card-title>
                        Thêm sản phẩm
                    </v-card-title>
                    <v-card-text>
                        <v-row>
                            <v-col>
                                <v-text-field label="Tên sản phẩm" v-model="product.productName"></v-text-field>
                            </v-col>
                            <v-col>
                                <v-autocomplete
                                    label="Loại sản phẩm"
                                    :items="categories"
                                    item-title="categoryName"
                                    item-value="id"
                                    v-model="product.categoryId"
                                ></v-autocomplete>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-text-field label="Giá sản phẩm" v-model="product.price"></v-text-field>
                            </v-col>
                            <v-col>
                                <v-text-field label="Kích thước" v-model="product.size"></v-text-field>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-text-field label="Chất liệu" v-model="product.material"></v-text-field>
                            </v-col>
                            <v-col>
                                <v-text-field label="Màu sắc" v-model="product.color"></v-text-field>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-file-input v-model="currFiles" 
                                small-chips 
                                multiple
                                label="Hình ảnh"
                                @change="fileAdded">
                            </v-file-input>
                        </v-row>
                        <v-row class="mt-5">
                            <v-chip v-for="(file, index) in files" :key="file" closable @click:close="remove(index)" class="mr-2">
                                {{ file.name }}
                            </v-chip>
                        </v-row>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="$emit('close'),resetdata()" color="red">Hủy</v-btn>
                        <v-btn @click="AddProduct">Thêm</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-container>
    </div>
</template>

<script>
import axios from 'axios';
import { mapActions, mapGetters } from 'vuex';
export default {
    props: ['dialogAdd'],
    data(){
        return{
            currFiles: [],
            previousFiles: [],
            files: [],
            product:{
                id: 0,
                categoryId: '',
                productName: '',
                price: '',
                size: '',
                color: '',
                material: ''
            },
            imageUrls: []
        }
    },
    computed:{
        dialog:{
            get(){
                return this.dialogAdd;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        },
        ...mapGetters(['categories','token'])
    },
    watch: {
        files(val) {
            this.previousFiles = val
        }  
    },
    methods: {
        ...mapActions(['loadProductImages','loadProducts']),
        remove (index) {
            this.files.splice(index, 1)
            console.log(this.files)
        },
        fileAdded () {
            this.files = [
                ...this.files,
                ...this.currFiles
            ]
            console.log(this.files)
        },
        resetdata(){
            this.currFiles = [],
            this.previousFiles = [],
            this.files = [],
            this.product.id = 0;
            this.product.categoryId = '';
            this.product.productName = '';
            this.product.price = '';
            this.product.size = '';
            this.product.color = '';
            this.product.material = '';
            this.imageUrls = [];
        },
        Add(){
            axios.post('https://localhost:44309/api/Product', this.product, {headers: {'Authorization': 'Bearer ' + this.token}})
            .then(response =>{
                console.log(response.status);
                console.log("pass1");
                axios.get('https://localhost:44309/api/Product/GetLastProduct', {headers: {'Authorization': 'Bearer ' + this.token}})
                .then(response =>{
                    this.product = response.data;
                    console.log("pass2");
                    console.log(this.product);
                    let formData = new FormData();
                    for( var i = 0; i < this.files.length; i++ ){
                        let file = this.files[i];
                        formData.append('fileColection', file);
                    }
                    console.log(formData);
                    axios.post('https://localhost:44309/api/UploadImage/MultiUploadImage?productId=' + this.product.id ,formData, {headers: {'Content-Type': 'multipart/form-data','Authorization': 'Bearer ' + this.token}})
                    .then(response =>{
                        console.log(response.status);
                        console.log("pass3");
                        axios.get('https://localhost:44309/api/UploadImage/GetMultiImage?productId=' + this.product.id, {headers: {'Authorization': 'Bearer ' + this.token}})
                        .then(response =>{
                            this.imageUrls = response.data;
                            console.log(this.imageUrls);
                            console.log("pass4");
                            for(var image of this.imageUrls){
                                axios.post('https://localhost:44309/api/ProductImage', {productId: this.product.id,imageUrl: image}, {headers: {'Authorization': 'Bearer ' + this.token}})
                                .then(response =>{
                                    console.log(response.status);
                                    console.log("pass5");
                                    this.loadProductImages();
                                    this.loadProducts();
                                    this.resetdata();
                                    this.$emit('close');
                                    this.$emit('updateData');
                                }).catch(e =>{
                                    console.log(e);
                                })
                            }
                        }).catch(e =>{
                            console.log(e)
                        })
                    }).catch(e =>{
                        console.log(e);
                    })
                }).catch(e =>{
                    console.log(e);
                })
            }).catch(e =>{
                console.log(e);
            })
        },
        AddProduct(){
            this.Add();
        },
    }  
}
</script>

<style>

</style>