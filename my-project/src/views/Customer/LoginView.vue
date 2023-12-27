<template>
    <v-container>
        <v-dialog v-model="dialog" scrollable fullscreen>
            <v-card>
                <v-card-title>
                    <v-tabs v-model="step" color="blue" style="padding-left: 500px;">
                        <v-tab :value="1">Đăng Nhập</v-tab>
                        <v-tab :value="2">Đăng Ký</v-tab>
                        <v-spacer></v-spacer>
                        <v-btn icon size="small" variant="text" @click="$emit('close')">
                            <v-icon>mdi-window-close</v-icon>
                        </v-btn>
                    </v-tabs>
                </v-card-title>
                <v-card-text>
                    <v-window v-model="step">
                        <v-window-item :value="1" class="text-center mb-3 mt-4">
                            <v-form @submit.prevent="Login(acc),this.$emit('close')">
                                <p>Đăng nhập với:</p>
                                <br>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-facebook</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-google</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-twitter</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-github</v-icon>
                                </v-btn>
                                <p class="mt-5 mb-5">Hoặc:</p>
                                <v-row justify="center">
                                    <v-col cols="12" sm="5">
                                        <v-text-field v-model="acc.username" label="Tài khoản" outlined dense color="blue" autocomplete="false"></v-text-field>
                                        <v-text-field
                                            v-model="acc.password"
                                            label="Mật khẩu"
                                            outlined dense color="blue" 
                                            :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
                                            :type="visible ? 'text' : 'password'"
                                            @click:append-inner="visible = !visible"
                                        ></v-text-field>
                                        <v-row>
                                            <v-col cols="12" sm="7">
                                                <v-checkbox label="Nhớ mật khẩu" color="blue"></v-checkbox>
                                            </v-col>
                                            <v-col cols="12" sm="5" class="mt-4">
                                                <span style="color: #2196F3;">Quên mật khẩu?</span>
                                            </v-col>
                                        </v-row>
                                        <v-btn block type="submit" color="blue">Đăng nhập</v-btn>
                                    </v-col>
                                </v-row>
                                <p class="mt-4">Chưa có tài khoản? <span style="color: #2196F3;">Đăng ký</span></p>
                            </v-form>
                        </v-window-item>
                        <v-window-item :value="2" class="text-center mb-3 mt-4">
                            <v-form @submit.prevent="SignUp(user),this.$emit('close')">
                                <p>Đăng ký với:</p>
                                <br>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-facebook</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-google</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-twitter</v-icon>
                                </v-btn>
                                <v-btn icon class="mx-3" size="40" variant="text">
                                    <v-icon color="blue">mdi mdi-github</v-icon>
                                </v-btn>
                                <p class="mt-5 mb-5">Hoặc:</p>
                                <v-row justify="center">
                                    <v-col cols="12" sm="8">
                                        <v-row>
                                            <v-col>
                                                <v-text-field v-model="user.Name" :rules="[rules.required]" label="Họ tên" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                            <v-col>
                                                <v-text-field v-model="user.Account" :rules="[rules.required]" label="Tên tài khoản" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                        </v-row>
                                        <v-row>
                                            <v-col>
                                                <v-text-field v-model="user.Password" :rules="[rules.required, rules.password]" label="Mật khẩu" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                            <v-col>
                                                <v-text-field v-model="user.Email" :rules="[rules.required, rules.email]" label="Email" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                        </v-row>
                                        <v-row>
                                            <v-col>
                                                <v-text-field v-model="rePass" :rules="[rules.required, rules.password]" label="Nhập lại mật khẩu" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                            <v-col>
                                                <v-text-field v-model="user.Phone" :rules="[rules.required, rules.phone]" label="Số điện thoại" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                        </v-row>
                                        <v-row>
                                            <v-col>
                                                <v-text-field v-model="user.Address" :rules="[rules.required]" label="Địa chỉ" outlined dense color="blue" autocomplete="false"></v-text-field>
                                            </v-col>
                                        </v-row>
                                        <v-row>
                                            <v-col cols="12" sm="7">
                                                <v-checkbox label="Tôi đồng ý với các điều khoản" color="blue"></v-checkbox>
                                            </v-col>
                                        </v-row>
                                        <v-btn class="mb-3" block color="blue" type="submit">Đăng ký</v-btn>
                                    </v-col>
                                </v-row>
                            </v-form>
                        </v-window-item>
                    </v-window>
                </v-card-text>
            </v-card>
        </v-dialog>
    </v-container>
</template>

<script>
import { mapActions } from 'vuex'
export default {
    data(){
        return{
            step: null,
            visible: false,
            user:{
                Id: 0,
                Account: '',
                Password: '',
                Name: '',
                Phone: '',
                Email: '',
                Address: '',
                IsAdmin: 0,
                IsCustomer: 1
            },
            rePass: '',
            rules: {
                required: value => !!value || 'Bắt buộc.',
                password: value =>{
                    if (value == this.user.Password) return true
                    return 'Mật khẩu phải giống nhau.'
                },
                email: value => {
                    const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                    return pattern.test(value) || 'Bạn phải nhập Email.'
                },
                phone: value => {
                    const pattern = /^\d{10}$/;
                    return pattern.test(value) || 'Bạn phải nhập số điện thoại.'
                }
            },
            acc:{
                username:'',
                password:''
            }
        }
    },
    props: ['dialogLogin'],
    computed:{
        dialog:{
            get(){
                return this.dialogLogin;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        }
    },
    methods:{
        ...mapActions(['SignUp','Login'])
    }
}
</script>

<style>

</style>