<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <img class="img-fluid rounded elevation-5" :src="keep?.img" alt="">
            </div>
            <div class="col-md-6 d-flex justify-content-center align-items-center">
                <h1 class="keep-title">{{ keep?.name }}
                    <br /><span class="k-desc">{{ keep?.description }}</span>
                    <div class="on-hover creator-name">by {{ keep?.creator.name }}
                        <img class="profile-picture elevation-5" :src="keep?.creator.picture" alt="">
                    </div>
                </h1>
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


<style lang="scss" scoped>
.profile-picture {
    height: 4vh;
    width: 4vh;
    object-fit: cover;
    border-radius: 50%;
}

.keep-title {
    font-family: 'Marko One', serif;
    user-select: none;
}

.k-desc {
    font-size: 24px !important;
}

.creator-name {
    font-size: 16px !important;
}
</style>