<template>
    a
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';

export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            activeKeep: computed(() => AppState.activeKeep)
        }
    }
}
</script>


<style lang="scss" scoped></style>