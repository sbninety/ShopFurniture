<template>
    <v-app-bar :elevation="2">
        <template v-slot:prepend>
            <v-app-bar-nav-icon></v-app-bar-nav-icon>
        </template>
        <v-app-bar-title></v-app-bar-title>
        <v-spacer></v-spacer>
        <v-btn rounded="xl" elevation="2" variant="outlined">Wellcome, {{ admin.account }}</v-btn>
        <v-btn icon @click="SignOutAdmin">
            <v-icon>mdi-logout</v-icon>
        </v-btn>
    </v-app-bar>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
export default {
    data(){
        return{
            menus: [
                ['mdi-logout', 'Đăng xuất']
            ]
        }
    },
    computed:{
        ...mapGetters(['admin'])
    },
    methods:{
        ...mapActions(['SignOutAdmin'])
    }
}
</script>

<style>

</style>