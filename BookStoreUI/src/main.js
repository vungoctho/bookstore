import { createApp } from 'vue'
import App from './App.vue'
import './index.css'
import 'bulma/css/bulma.min.css'
import store from './store'

const app = createApp(App) 
app.use(store);
app.mount('#app')
