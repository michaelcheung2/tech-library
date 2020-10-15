<template>
    <div>
        <div>
            <b-button variant="primary" @click="previousClick();">Previous</b-button>
            <span style="padding:5px;"></span>
            <b-button variant="primary" @click="nextClick();">Next</b-button>
        </div>
        <div class="home">
            <h1>{{ msg }}</h1>

            <b-table striped hover :items="results" :fields="fields" responsive="sm">
                <template v-slot:cell(thumbnailUrl)="data">
                    <b-img :src="data.value" thumbnail fluid></b-img>
                </template>
                <template v-slot:cell(title_link)="data">
                    <b-link :to="{ name: 'book_view', params: { 'id' : data.item.bookId } }">{{ data.item.title }}</b-link>
                </template>
            </b-table>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    const BASE_URL = 'https://localhost:5001';

    export default {
        name: 'Home',
        props: {
            msg: String
        },
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }

            ],
            items: [],
            currentOffset: 0,
            results: []
        }),

        mounted() {
            this.dataContext();
        },

        methods: {
            dataContext() {
                axios.get(BASE_URL + "/books")
                    .then(response => {
                        this.results = response.data;
                    });
            },
            previousClick: function () {
                if (this.currentOffset >= 10) {
                    this.currentOffset = this.currentOffset - 10;
                }
                axios.get(BASE_URL + "/books?currentOffset=" + this.currentOffset)
                    .then(response => {
                        this.results = response.data;
                    });
            },
            nextClick: function () {
                this.currentOffset = this.currentOffset + 10;
                axios.get(BASE_URL + "/books?currentOffset=" + this.currentOffset)
                    .then(response => {
                        this.results = response.data;
                    });
            },

        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

