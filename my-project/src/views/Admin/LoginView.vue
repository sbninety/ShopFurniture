<template>
    <div>
        <h2 class="text-center mt-15 mb-5">WELCOME</h2>
        <v-card
            class="mx-auto pa-12 pb-8"
            elevation="8"
            max-width="448"
            rounded="lg"
        >
            <v-form @submit.prevent="Login(acc)">
                <div class="text-subtitle-1 text-medium-emphasis">Tài khoản</div>
                <v-text-field
                    v-model="acc.username"
                    density="compact"
                    placeholder="Nhập tài khoản"
                    prepend-inner-icon="mdi-email-outline"
                    variant="outlined"
                ></v-text-field>
                <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
                    Mật khẩu
                </div>
                <v-text-field
                    v-model="acc.password"
                    :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
                    :type="visible ? 'text' : 'password'"
                    density="compact"
                    placeholder="Nhập mật khẩu"
                    prepend-inner-icon="mdi-lock-outline"
                    variant="outlined"
                    @click:append-inner="visible = !visible"
                ></v-text-field>
                <v-card
                    class="mb-12"
                    color="surface-variant"
                    variant="tonal"
                >
                </v-card>
                <v-btn
                    block
                    class="mb-8"
                    color="blue"
                    size="large"
                    variant="flat"
                    type="submit"
                >
                    Log In
                </v-btn>
            </v-form>
        </v-card>
    </div>
</template>

<script>
import { mapActions } from 'vuex'
export default {
    data(){
        return{
            visible: false,
            acc:{
                username: '',
                password: ''
            }
        }
    },
    methods:{
        ...mapActions(['Login'])
    }
}
</script>

<style>

</style>