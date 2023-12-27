<template>
    <div>
        <v-container>
            <v-dialog v-model="dialog" max-width="940px">
                <v-card>
                    <v-card-title>
                        Sửa thông tin sản phẩm
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
                        <v-row>
                            <v-chip v-for="(file, index) in files" :key="file" closable @click:close="remove(index)" class="mr-2">
                                {{ file.name }}
                            </v-chip>
                        </v-row>
                        <v-row class="mt-10">
                            <div class="ml-5 mt-3" v-for="(image, index) in listImage" :key="image">
                                <v-chip label pill style="width: 150px;height: 125px;">
                                    <v-avatar start rounded="0" style="width: 150px;height: 100px;">
                                        <v-img :src="image.imageUrl"></v-img>
                                    </v-avatar>
                                </v-chip>
                                <v-btn icon style="margin-left: -13px; margin-top: -125px; width: 27px; height: 27px;" @click="removeImg(index)">
                                    <v-icon size="15">mdi-close</v-icon>
                                </v-btn>
                            </div>
                        </v-row>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="$emit('close'),resetdata()" color="red">Hủy</v-btn>
                        <v-btn @click="UpdateProduct">Sửa</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-container>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters, mapActions } from 'vuex';
export default {
    props: ['dialogEdit','curItem'],
    data(){
        return{
            currFiles: [],
            previousFiles: [],
            files: [],
            product:{
                id: '',
                categoryId: '',
                // categoryName: '',
                productName: '',
                price: '',
                size: '',
                color: '',
                material: ''
            },
            listImage: [],
            removeImage: []
        }
    },
    computed:{
        ...mapGetters(['categories','productImages','token']),
        dialog:{
            get(){
                return this.dialogEdit;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        }
    },
    watch: {
        files(val) {
            this.previousFiles = val
        },
        curItem:function(){
            this.product.id = this.curItem.id;
            this.product.categoryId = this.curItem.categoryId;
            this.product.productName = this.curItem.productName;
            this.product.price = this.curItem.price;
            this.product.size = this.curItem.size;
            this.product.color = this.curItem.color;
            this.product.material = this.curItem.material;
        }
    },
    methods: {
        ...mapActions(['loadProductImages','loadProducts']),
        removeImg(index){
            this.removeImage.push(this.listImage[index]);
            this.listImage.splice(index,1);
            console.log(this.removeImage);
        },
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
            this.removeImage = []
        },
        UpdateProduct(){
            axios.put('https://localhost:44309/api/Product/' + this.product.id, this.product, {headers: {'Authorization': 'Bearer ' + this.token}})
            .then(response =>{
                console.log(response.status);
                if(this.removeImage.length > 0){
                    for(var j = 0; j < this.removeImage.length; j++){
                        console.log(this.removeImage[j].id);
                        console.log(this.removeImage[j].imageUrl);
                        axios.get('https://localhost:44309/api/UploadImage/RemoveImage?productId=' + this.product.id + '&url=' + this.removeImage[j].imageUrl, {headers: {'Authorization': 'Bearer ' + this.token}})
                        .then(response =>{
                            console.log(response.status);
                        }).catch(e =>{
                            console.log(e);
                        })
                        axios.delete('https://localhost:44309/api/ProductImage/' + this.removeImage[j].id, {headers: {'Authorization': 'Bearer ' + this.token}})
                        .then(response => {
                            console.log(response.status);
                            this.loadProductImages();
                            this.loadProducts();
                        }).catch(e =>{
                            console.log(e);
                        })
                    }
                }
                if(this.files.length > 0){
                    let formData = new FormData();
                    for( var i = 0; i < this.files.length; i++ ){
                        let file = this.files[i];
                        formData.append('fileColection', file);
                    }
                    axios.post('https://localhost:44309/api/UploadImage/UpAndGetImageUrl?productId=' + this.product.id, formData, {headers: {'Content-Type': 'multipart/form-data','Authorization': 'Bearer ' + this.token}})
                    .then(response =>{
                        var listUrls = response.data;
                        for(var image of listUrls){
                                axios.post('https://localhost:44309/api/ProductImage', {productId: this.product.id,imageUrl: image}, {headers: {'Authorization': 'Bearer ' + this.token}})
                                .then(response =>{
                                    console.log(response.status);
                                    this.loadProductImages();
                                    this.loadProducts();
                                }).catch(e =>{
                                    console.log(e);
                                })
                            }
                    }).catch(e =>{
                        console.log(e);
                    })
                }
                this.loadProductImages();
                this.loadProducts();
                this.resetdata();
                this.$emit('close');
            }).catch(e =>{
                console.log(e);
            }) 
        }
    },
    updated(){
        
        this.listImage = this.productImages.filter(item => item.productId == this.curItem.id);
    }
}
</script>

<style>

</style>