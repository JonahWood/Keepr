<!-- FIXME something wacky is going on when trying to see keep details when youre not logged in -->
<!-- FIXME also theres some horizontal scroll because of some goofy thing i did -->
<!-- FIXME the spacing of the last part of the modal is kinda being funky, gonna need to take a look at that -->
<template>
    <div @click="setActive(keep?.id)" type="button" data-bs-toggle="modal" data-bs-target="#KeepCardModal">
        <div>
            <img class="img-fluid" :src="keep?.img" alt="">
        </div>
        <div class="d-flex justify-content-between mt-1">
            {{ keep?.name }}
            <img :title="keep?.creator.name" class="profile-picture" :src="keep?.creator.picture" alt="">
        </div>
    </div>

    <!-- SECTION Modal -->
    <div class="modal fade no-border" id="KeepCardModal" tabindex="-1" aria-labelledby="KeepCardModalLabel"
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
.profile-picture {
    height: 4vh;
    width: 4vh;
    object-fit: cover;
    border-radius: 50%;
}

.keep-title {
    font-family: 'Marko One', serif;
}

.no-border {
    border-top: 0px solid black !important;
    border-bottom: 0px solid black !important;
    border-left: 0px solid black !important;
    border-right: 0px solid black !important;
    padding-top: 0px !important;
    padding-bottom: 0px !important;
    padding-left: 0px !important;
    padding-right: 0px !important;
    margin-top: 0px !important;
    margin-bottom: 0px !important;
    margin-left: 0px !important;
    margin-right: 0px !important;
}

.views {
    color: #636E72;
}

.keep-img {
    // height: 40vh;
    min-width: 100% !important;
}
</style>