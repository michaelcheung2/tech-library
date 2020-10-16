<template>
    <b-card>
        <b-form-input v-model="title" placeholder="Title..."></b-form-input>
        <b-form-input v-model="isbn" placeholder="ISBN..."></b-form-input>
        <b-form-input v-model="imageUrl" placeholder="Image URL..."></b-form-input>
        <b-textarea v-model="description" placeholder="Description..." rows="10" max-rows="10"></b-textarea>
        <b-button to="/" variant="primary">Back</b-button>
        <span style="padding:5px;"></span>
        <b-button @click="createBook();" variant="outline-primary">Save </b-button>

    </b-card>
</template>

<script>
    import axios from 'axios';

    const BASE_URL = 'https://localhost:5001';

    export default {
        name: 'Book',
        props: ["id"],
        data: () => ({
            title: '',
            isbn: '',
            imageUrl: '',
            description: ''
        }),
        methods: {
            createBook() {
                axios.post(`${BASE_URL}/books/create-book`, {
                    title: this.title,
                    isbn: this.isbn,
                    imageUrl: this.imageUrl,
                    description: this.description
                })
                    .then(
                        this.$router.push("/")
                    );
            }
        }
    }
</script>