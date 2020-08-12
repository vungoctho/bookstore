import { createStore } from 'vuex'

const state = {
    bookList: null,
    loading: true,
    error: null,
    boughtBookList: []
}

const mutations = {
    // synchronous
    setBookList(state, payload) {
        state.bookList = payload;
    },
    setBoughtBook(state, payload) {
        state.boughtBookList.push(payload);
    },
    setLoading(state, loadingState) {
        state.loading = loadingState;
    },
    setError(state, error) {
        state.error = error;
    }
}

const actions = {
    //asynchronous
    async setBookList(state, query) {
        try {
            // hard-code the url for quick demonstration
            const headers = { Accept: "application/json", ApiKey: "123" }
            let apiBaseUrl = "http://localhost:5000";
            let url = apiBaseUrl + '/api/Book/GetBooks';
            if (!!query) {
                url += '?query=' + query;
            }
            const res = await fetch(url, headers);
            const json = await res.json();
            state.commit('setBookList', json);
            return json;
        } catch (err) {
            state.commit('setError', err);
        }
        finally {
            state.commit('setLoading', false);
        }
    },

    async buyBook(state, book) {
        try {
            // hard-code the url for quick demonstration
            // POST request using fetch with async/await
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                ApiKey: "123",
                body: JSON.stringify(book)
            };

            let apiBaseUrl = "http://localhost:5000";
            let url = apiBaseUrl + '/api/Book/BuyBook';

            const res = await fetch(url, requestOptions);
            const json = await res.json();
            state.commit('setBoughtBook', book);
            return json;
        } catch (err) {
            state.commit('setError', err);
        }
        finally {
            state.commit('setLoading', false);
        }
    }
}

const getters = {
    getBookList: state => state.bookList,
    getLoadingState: state => state.loading
}

export default createStore({
    state,
    mutations,
    actions,
    getters
})