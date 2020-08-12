<template>
  <h1 class="title is-1">Book Store</h1>
  <BookList />
</template>

<script>
import BookList from './components/BookList.vue'

export default {
  name: 'App',
  components: {
    BookList
  }
}
</script>
