<template>
    <div class="container">
        <section class="row justify-content-center mt-2">
            <div class="col-6 rounded" :style="`background-image: url(${vault?.img})`">
                <h1 class="text-uppercase vTitle text-light d-flex justify-content-center align-items-end">{{ vault?.name }}
                </h1>
                <h6 class="creator text-light d-flex justify-content-center align-items-end">by {{
                    vault?.creator.name }}</h6>
                <h6 v-if="vault?.creatorId == account.id"
                    class="text-light vTitle d-flex justify-content-center align-items-end">
                    <div class="form-check form-switch align-items-center d-flex">
                        <input v-model="editable.isPrivate" @click="makePrivate(vault?.id)" class="form-check-input"
                            type="checkbox" role="switch" id="flexSwitchCheckDefault">
                        <label class="form-check-label" for="flexSwitchCheckDefault">Make Private?</label>
                    </div>
                </h6>
            </div>
        </section>
        <div class="row mt-2">
            <section class="grid-container rounded">
                <div v-for="k in keeps" class="col-md-3">
                    <KeepCard :keep="k" />
                </div>
            </section>
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
import { onMounted, ref } from 'vue';
import { vkService } from '../services/VKService'
import KeepCard from '../components/KeepCard.vue';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const editable = ref({})
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error.message);
                logger.error(error);
                router.push({ name: "Home" });
            }
        }
        async function getVaultKeeps() {
            try {
                const vaultId = route.params.vaultId;
                await vkService.getVaultKeeps(vaultId);
            }
            catch (error) {
                Pop.error(error.message);
                logger.error(error);
            }
        }
        onMounted(() => {
            if (AppState.account != {}) {
                getVaultById();
                getVaultKeeps();
            }
        });
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            editable,
            async makePrivate(vaultId) {
                try {
                    const isPrivate = editable.value
                    await vaultsService.makePrivate(vaultId, isPrivate)
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            }
        };
    },
    components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.grid-container {
    columns: 5 200px;
    column-gap: 1.5rem;
    width: 90%;
    margin: 0 auto;

    div {
        width: 150px;
        margin: 0 1.5rem 1.5rem 0;
        display: inline-block;
        width: 100%;
        border: solid 1px black;
        padding: 5px;
        box-shadow: 2px 2px 2px #00000080;
        border-radius: 5px;
        transition: all .25s ease-in-out;

        &:hover img {
            filter: grayscale(0);
        }

        &:hover {
            border-color: #d4af37;
            box-shadow: 4px 4px 4px #4d4015c1;
        }

        img {
            width: 100%;
            filter: grayscale(100%);
            border-radius: 5px;
            transition: all .25s ease-in-out;
        }

        p {
            margin: 5px 0;
            padding: 0;
            text-align: center;
            font-style: italic;
        }
    }
}

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