<template>
  <v-app>
    <NavBar/>
    <br>
    <br>
    <br>
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
            variant="plain"
          >{{ item.categoryName }}</v-btn>
        </v-bottom-navigation>
      </v-layout>
      <hr>
      <v-data-iterator :items="products" :page="page" :items-per-page="9">
        <template v-slot:default="{ items }">
          <v-container fluid>
            <v-row>
              <v-col
                v-for="item in items"
                :key="item"
                cols="12"
                md="4"
              >
                <v-card style="height: 500px;">
                  <v-img
                    :src="item.raw.imageUrl"
                    width="500px"
                    height="400px"
                    aspect-ratio="1"
                  ></v-img>
                  <v-row justify="center" style="padding-left: 25px;padding-right: 25px;text-align: center;">
                    <router-link style="text-decoration: none; color: black;" :to="'/customer/detail/' + item.raw.id">{{item.raw.productName}}</router-link>
                  </v-row>
                  <v-row class="mb-5">
                    <v-col>
                      <div style="margin-left: 20px;text-align: center;margin-top: 10px;">
                        <p style="color: red;font-weight: bold;">{{ item.raw.price }}<sup>đ</sup></p>
                      </div>
                    </v-col>
                    <v-col>
                      <div class="text-center" style="margin-right: 30px;margin-top: 7px;">
                        <v-btn
                          append-icon="mdi mdi-cart-outline"
                          size="small"
                          variant="outlined"
                          @click="dialog=true,addToCart(item.raw)"
                        >
                            Thêm giỏ hàng
                          <template v-slot:append>
                            <v-icon></v-icon>
                          </template>
                        </v-btn>
                      </div>
                    </v-col>
                  </v-row>
                </v-card>
              </v-col>
            </v-row>
          </v-container>
          <v-pagination
            v-model="page"
            :length="products.length/9 + 1"
            rounded="circle"
          ></v-pagination>
        </template>
      </v-data-iterator>
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

import NavBar from '@/components/NavBar.vue'
import { mapGetters, mapActions} from 'vuex'
export default {
  name: 'HomeView',
  data () {
    return {
      dialog: false,
      page: 1,
      tab: null,
      links: ['Home', 'About Us', 'Team', 'Services', 'Blog', 'Contact Us']
    }
  },
  computed:{
    ...mapGetters(['products','categories'])
  },
  methods:{
    ...mapActions(['listProductByCat','addToCart'])
  },
  components: {
    NavBar
  }

}
</script>
