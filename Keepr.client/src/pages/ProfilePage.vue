<template>
    <div class="container">
        <section class="row justify-content-center">
            <div class="col-md-10 mt-md-3 d-flex justify-content-center">
                <img class="coverImg img-fluid rounded"
                    :src="profile.coverImg ? profile.coverImg : 'https://images.unsplash.com/photo-1521459467264-802e2ef3141f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80'"
                    alt="">
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-md-2 d-flex justify-content-center">
                <img class="elevation-3 accImg" :src="profile?.picture" alt="">
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-md-1 d-flex justify-content-center">
                <h1>{{ profile?.name }}</h1>
            </div>
        </section>
        <section class="row justify-content-center">
            <div class="col-md-4 d-flex justify-content-center">
                <h4 class="lengths">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h4>
            </div>
        </section>
        <section class="row mb-5">
            <div class="col-12">
                <h2>Vaults</h2>
            </div>
            <div class="col-3" v-for="v in vaults">
                <VaultCard :vault="v" />
            </div>
        </section>
        <section class="row">
            <div class="col-12">
                <h2>Keeps</h2>
            </div>
            <div class="col-3" v-for="k in keeps">
                <KeepCardAcc :keep="k" />
            </div>
        </section>
    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { keepsService } from '../services/KeepsService';
import { onMounted } from 'vue';

export default {
    setup() {
        async function getProfileVaults() {
            try {
                AppState.vaults = []
                await vaultsService.getProfileVaults()
            } catch (error) {
                Pop.error(error.message)
                logger.error(error)
            }
        }

        async function getProfileKeeps() {
            try {
                AppState.keeps = []
                await keepsService.getProfileKeeps()
            }
            catch (error) {
                Pop.error(error.message)
                logger.error(error)
            }
        }

        onMounted(() => {
            getProfileKeeps()
            getProfileVaults()
        })
        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            activeKeep: computed(() => AppState.activeKeep)
        }
    }
}
</script>


<style lang="scss" scoped>
.lengths {
    color: rgb(117, 117, 117);
}

.coverImg {
    background-position: center;
    object-fit: cover;
    height: 45vh;
    width: 100%;
}

.accImg {
    border-radius: 50%;
    transform: translate(0rem, -5.5vh);
    /* border: 1px solid whitesmoke; */
}
</style>