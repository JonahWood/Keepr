<template>
    <div class="container">
        <div class="row justify-content-center mt-2">
            <div class="col-6 rounded" :style="`background-image: url(${vault?.img})`">
                <h1 class="text-uppercase vTitle text-light d-flex justify-content-center align-items-end">{{ vault?.name }}
                </h1>
                <h6 class="creator text-light d-flex justify-content-center align-items-end">by {{
                    vault?.creator.name }}</h6>
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { onMounted } from 'vue';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getVaultById(vaultId)
            } catch (error) {
                Pop.error(error.message)
                logger.error(error)
                router.push({ name: 'Home' })
            }
        }
        onMounted(() => {
            getVaultById();
        })
        return {
            vault: computed(() => AppState.activeVault),
        }
    }
}
</script>


<style lang="scss" scoped>
.creator {
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}

.vTitle {
    letter-spacing: 2px;
    min-height: 20vh;
    width: 100%;
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}
</style>