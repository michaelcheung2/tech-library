<template>
    <div>
        <div>
            <b-input-group>
                <b-button variant="primary" @click="previousClick();">Previous</b-button>
                <span style="padding:5px;"></span>
                <b-button variant="primary" @click="nextClick();">Next</b-button>
                <span style="padding:20px;"></span>
                <b-input id="searchInput" placeholder="Search title/desc"></b-input>
                <b-button variant="primary" @click="searchClick();">Search</b-button>
            </b-input-group>
        </div>
        <div class="home">
            <h1>{{ msg }}</h1>

            <b-table striped hover :items="items" :fields="fields" responsive="sm">
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
            searchInput: ''
        }),

        mounted() {
            this.dataContext();
        },

        methods: {
            dataContext() {
                axios.get(BASE_URL + "/books")
                    .then(response => {
                        this.items = response.data;
                    });
            },
            previousClick: function () {
                if (this.currentOffset >= 10) {
                    this.currentOffset = this.currentOffset - 10;
                }
                axios.get(BASE_URL + "/books?currentOffset=" + this.currentOffset)
                    .then(response => {
                        this.items = response.data;
                    });
            },
            nextClick: function () {
                this.currentOffset = this.currentOffset + 10;
                axios.get(BASE_URL + "/books?currentOffset=" + this.currentOffset)
                    .then(response => {
                        this.items = response.data;
                    });
            },
            searchClick: function () {
                let inputText = document.getElementById('searchInput').value;
                axios.get(BASE_URL + "/books?currentOffset=" + this.currentOffset + "&search=" + inputText)
                    .then(response => {
                        this.items = response.data;
                    });
            },

        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

