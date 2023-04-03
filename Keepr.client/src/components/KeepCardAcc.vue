<template>
    <h3 class="vTitle text-light rounded elevation-5 selectable text-lowercase"
        :style="`background-image: url(${keep?.img})`" @click="setActive(keep?.id)" type="button" data-bs-toggle="modal"
        data-bs-target="#KeepCardModalAcc">{{
            keep?.name }}</h3>




    <!-- SECTION Modal -->
    <div class="modal fade no-border" id="KeepCardModalAcc" tabindex="-1" aria-labelledby="KeepCardModalAccLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-5">
                                <img class="img-fluid keep-img" :src="activeKeep?.img" alt="">
                            </div>
                            <div class="col-md-7">
                                <div class="row">
                                    <div class="col-md-12 d-flex justify-content-center">
                                        <h6 class="views"><i class="mdi mdi-eye"></i> {{ activeKeep?.views }} | <i
                                                class="mdi mdi-alpha-k-box-outline"></i> {{ activeKeep?.kept }}</h6>
                                    </div>
                                </div>
                                <div class="row mt-md-5">
                                    <div class="col-md-12 d-flex align-self-center">
                                        <span>
                                            <h1 class="keep-title d-flex justify-content-center">{{ activeKeep?.name }}</h1>
                                            <h5 class="mx-4">{{ activeKeep?.description }}</h5>
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">&nbsp;</div>
                                </div>
                                <div class="row">
                                    <div class="col-12">&nbsp;</div>
                                </div>
                                <div class="row">
                                    <div class="col-12">&nbsp;</div>
                                </div>
                                <div class="row">
                                    <div class="col-12">&nbsp;</div>
                                </div>
                                <div class="row">
                                    <div class="col-12">&nbsp;</div>
                                </div>
                                <div class="row d-flex align-self-end">
                                    <div class="col-md-12 d-flex justify-content-between">
                                        <div class="dropdown">
                                            <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                                aria-expanded="false">
                                                Add to vault
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li><a class="dropdown-item" href="#">Action</a></li>
                                                <li><a class="dropdown-item" href="#">Another action</a></li>
                                                <li><a class="dropdown-item" href="#">Something else here</a></li>
                                            </ul>
                                        </div>
                                        <h6><img class="profile-picture" :src="account?.picture" alt=""> {{ account?.name }}
                                        </h6>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { onMounted, computed } from 'vue';
import { AppState } from "../AppState";
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';


export default {
    props: { keep: { type: Object, required: true } },
    setup() {
        return {
            activeKeep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            async setActive(id) {
                try {
                    await keepsService.setActive(id)
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.vTitle {
    min-height: 20vh;
    width: 100%;
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}
</style>