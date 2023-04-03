<template>
  <nav class="navbar navbar-expand-lg navbar-dark px-3 kHeader">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo"
          src="https://static.vecteezy.com/system/resources/previews/016/316/977/original/gold-text-effect-letter-k-3d-render-png.png"
          height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'Home' }" class="btn home-button selectable bg-gradient">
            Home
          </router-link>
          <button class="btn create-button ms-md-2 bg-gradient" type="button" data-bs-toggle="modal"
            data-bs-target="#CreateVault">Create Keep</button>
          <button class="btn create-button ms-md-2 bg-gradient">Create Vault</button>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>

  <OCCreateK />
</template>

<script>
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import Login from './Login.vue'
import OCCreateK from '../components/OCCreateK.vue'

export default {
  setup() {
    return {

    }
  },
  components: { Login }
}
</script>

<style scoped>
.home-button {
  background-color: #E9D8D6;
  border-bottom: 0px solid black !important;
  border-radius: 40%;
  border: solid 1px '#d4af37k';
  border-bottom-left-radius: 40% !important;
  border-bottom-right-radius: 40% !important;
  border-color: #d4af37;
  box-shadow: 2px 2px 2px #4d4015c1;
  font-family: 'Oxygen', sans-serif;
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

.kHeader {
  border-bottom: 2px solid #E9D8D6;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
