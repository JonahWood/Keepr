<template>
    <h3 class="vTitle text-light rounded elevation-5 selectable text-lowercase makecover"
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
                                        <h6 class="views"><i class="mdi mdi-eye" title="views"></i> {{ activeKeep?.views }}
                                            | <i class="mdi mdi-alpha-k-box-outline" title="kept"></i> {{ activeKeep?.kept
                                            }}</h6>
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
                                <!-- <div class="row d-flex align-self-end">
                                    <div class="col-md-12 d-flex justify-content-between">
                                        <h6><img class="profile-picture" :src="account?.picture" alt=""> {{ account?.name }}
                                        </h6>
                                    </div>
                                </div> -->
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
import { Modal } from 'bootstrap';


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
            },
            async deleteKeep(keepId) {
                try {
                    if (await Pop.confirm("Are you sure you'd like to delete this keep?")) {
                        await keepsService.deleteKeep(keepId)
                        Modal.getOrCreateInstance('#KeepCardModal').hide()
                    }
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
.profile-picture {
    height: 4vh;
    width: 4vh;
    object-fit: cover;
    border-radius: 50%;
}

.vTitle {
    min-height: 20vh;
    width: 100%;
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}

.keep-title {
    font-family: 'Marko One', serif;
}
</style>