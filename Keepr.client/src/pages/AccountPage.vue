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
      <div class="col-md-2 d-flex justify-content-center">
        <h1>
          {{ account.name }} <button type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample"
            aria-controls="offcanvasExample" title="Edit Account" class="btn btn-success">Edit<i class="mdi mdi-note"
              title="Edit Account"></i></button></h1>
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
      <div class="col-md-3" v-for="v in vaults">
        <VaultCard :vault="v" />
      </div>
      <div class="col-md-3" v-if="keeps.length == 0">
        No Vaults!
      </div>
    </section>
    <section class="row">
      <div class="col-12">
        <h2>My Keeps</h2>
      </div>
      <div class="col-md-3" v-for="k in keeps">
        <KeepCardAcc :keep="k" />
      </div>
      <div class="col-md-3" v-if="keeps.length == 0">
        No Keeps!
      </div>
    </section>
  </div>

  <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasExampleLabel">Offcanvas</h5>
      <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body">
      <form @submit.prevent="editAccount()">
        <div class="mb-3">
          <label for="name" class="form-label">Name</label>
          <input required v-model="editable.name" type="text" minLength="3" maxlength="15" class="form-control" id="name"
            name="name">
        </div>
        <div>
          <label for="picture" class="form-label">Picture URL</label>
          <input required v-model="editable.picture" class="form-control" id="picture" type="text">
        </div>
        <div>
          <label for="coverImg" class="form-label">CoverImg</label>
          <input required v-model="editable.coverImg" class="form-control" id="coverImg" type="text">
        </div>
        <button data-bs-dismiss="offcanvas" class="btn btn-success" type="submit">Save Changes</button>
      </form>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import KeepCard from '../components/KeepCard.vue'
import { accountService } from '../services/AccountService'

export default {
  setup() {
    const editable = ref({})
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
      keeps: computed(() => AppState.keeps),
      editable,
      async editAccount() {
        try {
          const formData = editable.value
          await accountService.editAccount(formData)
          Pop.success('Account edited')
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

<style scoped>
.lengths {
  color: rgb(0, 0, 0);
}

.coverImg {
  background-position: center;
  object-fit: cover;
  height: 45vh;
  width: 100%;
}

.accImg {
  border-radius: 50% !important;
  transform: translate(0rem, -5.5vh) !important;
  height: 120px !important;
  width: 120px !important;
  /* border: 1px solid whitesmoke; */
}
</style>
