<template>
  <div class="container">
    <section class="row justify-content-center">
      <div class="col-md-10 mt-md-3 d-flex justify-content-center">
        <img class="coverImg img-fluid rounded" :src="account.coverImg" alt="">
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-md-2 d-flex justify-content-center">
        <img class="elevation-3 accImg" :src="account.picture" alt="">
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-md-1 d-flex justify-content-center">
        <h1>{{ account.name }}</h1>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-md-4 d-flex justify-content-center">
        <h4 class="lengths">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h4>
      </div>
    </section>
    <section class="row mb-5">
      <div class="col-12">
        <h2>My Vaults</h2>
      </div>
      <div class="col-3" v-for="v in vaults">
        <VaultCard :vault="v" />
      </div>
    </section>
    <section class="row">
      <div class="col-12">
        <h2>My Vaults</h2>
      </div>
      <div class="col-3" v-for="k in keeps">
        <KeepCardAcc :keep="k" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import KeepCard from '../components/KeepCard.vue'

export default {
  setup() {
    async function getMyVaults() {
      try {
        AppState.vaults = [];
        await vaultsService.getMyVaults();
      }
      catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    }
    async function getMyKeeps() {
      try {
        AppState.keeps = [];
        await keepsService.GetMyKeeps();
      }
      catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    }
    onMounted(() => {
      getMyVaults();
      getMyKeeps();
    });
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped>
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
