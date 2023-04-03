<template>
    <h4 class="vTitle text-light rounded elevation-5 selectable text-uppercase"
        :style="`background-image: url(${vault?.img})`">{{
            vault?.name }} <i v-if="vault?.isPrivate" class="mdi mdi-lock text-warning" title="This vault is private."></i></h4>
</template>


<script>
import { AppState } from '../AppState'

export default {
    props: { vault: { type: Object, required: true } },
    setup() {
        return {

        }
    }
}
</script>


<style lang="scss" scoped>
.vTitle {
    letter-spacing: 2px;
    min-height: 20vh;
    width: 100%;
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}
</style>