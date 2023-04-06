<template>
    <div class="container">
        <section class="row justify-content-center mt-2">
            <div class="col-md-6 rounded makecover" :style="`background-image: url(${vault?.img})`">
                <h1 class="text-uppercase vTitle text-light d-flex justify-content-center align-items-end">{{ vault?.name }}
                </h1>
                <h6 class="creator text-light d-flex justify-content-center align-items-end">by {{
                    vault?.creator.name }}</h6>
                <h6 v-if="vault?.creatorId == account.id"
                    class="text-light vTitle d-flex justify-content-center align-items-end">
                    <div class="align-items-center d-flex">
                        <button title="Make private" v-if="!vault?.isPrivate" class="btn big-btn"
                            @click="makePrivate(vault?.id)"><i
                                class="mdi mdi-lock-open-variant text-warning lock-shadow"></i></button>
                        <button title="Vault is already private" v-if="vault?.isPrivate" class="btn big-btn" disabled><i
                                class="mdi mdi-lock text-warning lock-shadow"></i></button>
                    </div>
                </h6>
            </div>
        </section>
        <section class="row mt-2 justify-content-center">
            <div class="col-md-6 keepCount">
                <h6 class=" d-flex justify-content-center">{{ keeps.length }} Keeps</h6>
                <div class="d-flex justify-content-center" v-if="vault?.creatorId == account.id">
                    <h3 title="Delete Vault"><i class="mdi mdi-delete-forever text-danger selectable"
                            @click="deleteVault(vault?.id)"></i></h3>
                </div>
            </div>
        </section>
        <div class="row mt-2">
            <section class="grid-container rounded">
                <div v-for="k in keeps" class="col-md-3">
                    <VKeepCard :keep="k" />
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
import { onMounted, onUnmounted, ref } from 'vue';
import { vkService } from '../services/VKService'
import KeepCard from '../components/KeepCard.vue';
import VKeepCard from '../components/VKeepCard.vue';

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
                Pop.error('This vault is private');
                logger.error(error);
                router.push({ name: "Home" });
            }
        }

        function setBoolTrue() {
            AppState.keepBool = true
        }
        function setBoolFalse() {
            AppState.keepBool = false
        }

        async function getVaultKeeps() {
            try {
                const vaultId = route.params.vaultId;
                await vkService.getVaultKeeps(vaultId);
                logger.log('Appstate keeps:', AppState.keeps)
            }
            catch (error) {
                Pop.error(error.message);
                logger.error(error);
            }
        }
        onMounted(() => {
            setBoolTrue()
            if (AppState.account != {}) {
                getVaultById();
                getVaultKeeps();
            }
        });
        onUnmounted(() => {
            setBoolFalse()
        })
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            editable,
            async makePrivate(vaultId) {
                try {
                    if (await Pop.confirm("Are you sure you want to make this vault private?", "This cannot be undone!")) {
                        const isPrivate = { isPrivate: true }
                        await vaultsService.makePrivate(vaultId, isPrivate)
                        Pop.toast(`${AppState.activeVault.name} has been made private.`)
                    }
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async deleteVault(vaultId) {
                try {
                    if (await Pop.confirm("Are you sure you'd like to delete this vault?")) {
                        await vaultsService.deleteVault(vaultId)
                        router.push({ name: "Home" });
                        Pop.success('Vault deleted')
                    }
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            }
        };
    },
    components: { KeepCard, VKeepCard }
}
</script>


<style lang="scss" scoped>
.lock-shadow {
    text-shadow: 2px 2px 2px #000000;
}

.makecover {
    background-position: center;
    background-size: cover;
    min-height: 500px;
}

.big-btn {
    font-size: 48px;
}

.keepCount {
    font-family: 'Marko One', serif;
    color: #000000;
}

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

@media screen and (max-width: 900px) {
    .grid-container {
        columns: 2 100px;
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
}
</style>