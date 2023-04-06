<template>
  <div class="mb-2 d-flex justify-content-between" v-if="account.id">
    <button class="btn create-button ms-md-2 bg-gradient" type="button" data-bs-toggle="modal"
      data-bs-target="#CreateKeep">Create Keep</button>
    <button class="btn create-button ms-md-2 bg-gradient" type="button" data-bs-toggle="modal"
      data-bs-target="#CreateVault">Create Vault</button>
  </div>
  <div class="Kbody">
    <div class="row no-pad-marge">
      <section class="grid-container rounded">
        <div class="col-6 col-md-3" v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
      </section>
    </div>
  </div>

  <OCCreateK />
  <OCCreateV />
</template>

<script>
import { onMounted, computed } from 'vue';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { AppState } from "../AppState";
import OCCreateK from '../components/OCCreateK.vue';
import OCCreateV from '../components/OCCreateV.vue';

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
    });
    async function getAllKeeps() {
      try {
        AppState.keeps = [];
        await keepsService.getAllKeeps();
      }
      catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    };
  },
  components: { OCCreateK, OCCreateV }
}
</script>

<style scoped lang="scss">
.no-pad-marge {
  padding: 0px 0px 0px 0px !important;
  margin: 0px 0px 0px 0px !important;

}

.create-button {
  background-color: #d5d6a8;
  border-bottom: 0px solid black !important;
  border: solid 1px '#d4af37k';
  border-bottom-left-radius: 10% !important;
  border-bottom-right-radius: 10% !important;
  border-color: #d4af37;
  box-shadow: 2px 2px 2px #4d4015c1;
  font-family: 'Oxygen', sans-serif;
}

.Kbody {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.grid-container {
  columns: 4 200px;
  column-gap: 1.5rem;
  width: 98%;
  margin: 0;

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

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
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
