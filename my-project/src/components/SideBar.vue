<template>
    <v-navigation-drawer location="left" style="padding: 0px;" >
        <v-img src="https://fastly.picsum.photos/id/10/2500/1667.jpg?hmac=J04WWC_ebchx3WwzbM-Z4_KC_LeLBWr5LZMaAkWkF68">
            <div class="text-center">
                <h1 style="color: white;margin-top: 60px;">WOODPRO</h1>
            </div>
        </v-img>
        <v-divider></v-divider>

        <v-list>
            <v-list-item
                v-for="[icon, text, link] in links"
                :key="icon"
                :value="icon"
                :to="link"
                color="primary"
            >
                <template v-slot:prepend>
                    <v-icon :icon="icon"></v-icon>
                </template>

                <v-list-item-title>{{ text }}</v-list-item-title>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
export default {
    data(){
        return{
            links: [
                ['mdi-microsoft-windows', 'Thống kê','/admin'],
                ['mdi-list-box-outline', 'Sản phẩm','/admin/product'],
                ['mdi-list-box', 'Đơn hàng','/admin/order'],
                ['mdi-account-multiple', 'Khách hàng', '/admin/customer']
            ]
        }
    }
}
</script>

<style>

</style>