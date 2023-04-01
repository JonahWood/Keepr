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
          <router-link :to="{ name: 'Home' }" class="btn home-button selectable text-uppercase">
            Home
          </router-link>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
.home-button {
  background-color: #E9D8D6;
  border-bottom: 0px solid black !important;
  border-radius: 40%;
  border-bottom-left-radius: 40% !important;
  border-bottom-right-radius: 40% !important;
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
