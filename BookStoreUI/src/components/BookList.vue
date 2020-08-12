<template>
  <div>
    <div v-if="state.loading">loading...</div>
    <div>
      <div class="field has-addons container">
        <div class="control">
          <input class="input" type="text" placeholder="Enter your search key" v-model="searchText" />
        </div>
        <div class="control">
          <a class="button is-info" @click="search">Search</a>
        </div>
      </div>
    </div>
    <br />

    <div class="columns is-mobile is-multiline" style="padding-left: 80px;">
      <div class="column is-one-quarter" v-for="(book, idx) in state.bookList" :key="idx">
        <div class="tile is-ancestor">
          <div class="tile is-vertical is-10">
            <div class="tile">
              <div class="tile is-parent">
                <article class="tile is-child notification is-info">
                  <strong>{{book.volumeInfo.title}}</strong>
                  <br />
                  <small>Authors: {{book.volumeInfo.author}}</small>
                  <br />
                  <small>Published Date: {{book.volumeInfo.publishedDate}}</small>
                  <br />
                  <figure class="image is-4by3">
                    <img :src="book.volumeInfo.imageLinks.thumbnail" />
                  </figure>
                  <br />
                  <p>{{book.volumeInfo.subtitle}}</p>
                  <br />
                  <div class="control">
                    <button class="button is-primary" @click="buy(book)">Buy</button>
                  </div>
                </article>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <information-modal v-show="state.showDialog" @close="state.showDialog = false"
    :dialog-message="state.dialogMessage"/>
    
  </div>
</template>

<script>
import { reactive, onMounted, ref } from "vue";
import { useStore } from "vuex";
import informationModal from '../modals/Information.vue';
export default {
  components: {
    informationModal
  },
  setup() {
    const store = useStore();
    const state = reactive({
      bookList: [],
      showDialog: false,
      dialogMessage:""
    });
    const searchText = ref("");

    const search = async () => {
      //get data
      const books = await store.dispatch("setBookList", searchText.value);
      state.bookList = books;
    };

    onMounted(async () => {
      //get data
      const books = await store.dispatch("setBookList");
      state.bookList = books;
    });

    const buy = async book => {      
      const result = await store.dispatch("buyBook", book);
      state.dialogMessage = "You have bought <strong>"+book.volumeInfo.title+"</strong> successfully";
      state.showDialog = true;
    };

    return {
      state,
      search,
      buy,
      searchText
    };
  }
};
</script>

<style>
</style>