<template>
    <div v-if="book">
        <b-card :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2">
            <b-button variant="primary" @click="toggleEditMode();" v-if="!isHidden">Edit</b-button>
            <b-button variant="primary" @click="saveDescription();" v-if="isHidden">Save</b-button>
            <b-card-text v-if="!isHidden">
                {{ book.descr }}
            </b-card-text>
            <b-form-textarea id="bookDescription"
                             v-model="book.descr"
                             max-rows="10"
                             v-if="isHidden">
            </b-form-textarea>

            <b-button to="/" variant="primary">Back</b-button>
        </b-card>
    </div>
</template>

<script>
    import axios from 'axios';

    const BASE_URL = 'https://localhost:5001';

    export default {
        name: 'Book',
        props: ["id"],
        data: () => ({
            book: null,
            isHidden: false
        }),
        mounted() {
            axios.get(`${BASE_URL}/books/${this.id}`)
                .then(response => {
                    this.book = response.data;
                });
        },
        methods: {
            toggleEditMode() {
                this.isHidden = !this.isHidden;
            },
            saveDescription() {
                let desc = document.getElementById('bookDescription').value;
                axios.put(`${BASE_URL}/books/${this.id}`, {
                        descr: desc
                    })
                    .then(response => {
                        this.book = response.data;
                        this.toggleEditMode();
                    });
            }
        }
    }
</script>