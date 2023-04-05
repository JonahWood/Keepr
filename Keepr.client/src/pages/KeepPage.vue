<template>
    a
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { onMounted, onUnmounted, ref } from 'vue';
import { vkService } from '../services/VKService'
import KeepCard from '../components/KeepCard.vue';
import VKeepCard from '../components/VKeepCard.vue';
import { keepsService } from '../services/KeepsService';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const editable = ref({})
        onMounted(() => {
            getKeepById()
        })

        async function getKeepById() {
            try {
                const keepId = route.params.keepId;
                await keepsService.setActive(keepId);
            }
            catch (error) {
                Pop.error(error.message)
                logger.error(error);
            }
        }
        return {
            keep: computed(() => AppState.activeKeep)
        }
    }
}
</script>


<style lang="scss" scoped></style>