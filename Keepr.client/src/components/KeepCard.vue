<!-- FIXME something wacky is going on when trying to see keep details when youre not logged in -->
<!-- LINK fixed -->
<!-- FIXME also theres some horizontal scroll because of some goofy thing i did -->
<!-- FIXME the spacing of the last part of the modal is kinda being funky, gonna need to take a look at that -->
<!-- LINK fixed -->
<template>
    <div @click="setActive(keep?.id)" class="selectable">
        <div>
            <img class="img-fluid" :src="keep?.img" alt="">
        </div>
        <div class="d-flex justify-content-between mt-1 move-stuff vTitle-2 ms-md-1">
            {{ keep?.name }}
            <img :title="keep?.creator.name" class="profile-picture" :src="keep?.creator.picture" alt="">
        </div>
    </div>

    <div class="modal fade no-border" id="KeepCardModal" tabindex="-1" aria-labelledby="KeepCardModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-5 d-flex justify-content-center align-items-center">
                                <img class="img-fluid keep-img" :src="activeKeep?.img" alt="">
                            </div>
                            <div class="col-md-7 d-flex align-items-center justify-content-center">
                                <div class="row">

                                </div>
                                <div class="row mt-md-5">
                                    <div class="col-md-12">
                                        <span>
                                            <div class="col-md-12 d-flex justify-content-center">
                                                <h6 class="views"><i class="mdi mdi-eye" title="views"></i> {{
                                                    activeKeep?.views }}
                                                    | <i class="mdi mdi-alpha-k-box-outline" title="kept"></i> {{
                                                        activeKeep?.kept
                                                    }}</h6>
                                            </div>
                                            <span title="Remove keep from vault"
                                                v-if="activeVault?.creatorId == account?.id && keepBool" class="selectable"
                                                @click="removeFromVault(activeVK?.vaultKeepId)"><i
                                                    class="mdi mdi-minus-box"></i>Remove Keep</span>
                                            <h1 class="keep-title d-flex justify-content-center">{{ activeKeep?.name }}<span
                                                    title="Delete Keep" v-if="activeKeep?.creatorId == account?.id"
                                                    class="selectable" @click="deleteKeep(activeKeep?.id)"><i
                                                        class="mdi mdi-delete-forever"></i></span>
                                            </h1>
                                            <h5 class="mx-4">{{ activeKeep?.description }}</h5>
                                        </span>
                                    </div>
                                    <div class="col-md-12 d-flex justify-content-between">
                                        <div v-if="account.id">
                                            <form @submit.prevent="addKeep()">
                                                <select v-model="editable.vaultId" class="form-select mb-3" required>
                                                    <option v-for="v in myVaults" :value="v.id">{{ v.name }}</option>
                                                </select>
                                                <div class="text-end">
                                                    <button data-bs-dismiss="offcanvas" class="btn btn-success">Add</button>
                                                </div>
                                            </form>
                                        </div>
                                        <h6 @click="goToProfilePage(creator?.id)" title="`Go to this persons page?`'"
                                            class="selectable"><img class="profile-picture" :src="creator?.picture" alt="">
                                            {{
                                                creator?.name }}
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
import { onMounted, computed, watchEffect, ref, onUnmounted } from 'vue';
import { AppState } from "../AppState";
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService'
import { Modal } from 'bootstrap';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import { vkService } from '../services/VKService';


export default {
    props: { keep: { type: Object, required: true } },
    setup() {
        const router = useRouter()
        const route = useRoute()
        const editable = ref({})

        function setBoolFalse() {
            AppState.keepBool = false
        }
        async function setActiveVault() {
            try {
                await vaultsService.setActiveVault(route.params.vaultId)
            }
            catch (error) {
                Pop.error(error.message)
                logger.error(error)
            }
        }

        // onMounted(() => {
        //     setBoolTrue()
        // })
        // onUnmounted(() => {
        //     setBoolFalse()
        // })
        return {
            activeKeep: computed(() => AppState.activeKeep),
            editable,
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            creator: computed(() => AppState.activeCreator),
            myVaults: computed(() => AppState.vaults),
            keepBool: computed(() => AppState.keepBool),
            activeVault: computed(() => AppState.activeVault),
            activeVK: computed(() => AppState.activeVK),
            async setActive(id) {
                try {
                    await keepsService.setActive(id)
                    Modal.getOrCreateInstance("#KeepCardModal").show()
                    if (AppState.account.id) {
                        await vaultsService.getMyVaults()
                    }
                    logger.log('vaults:', AppState.vaults)
                    await vkService.setActiveVK(id)
                    logger.log('THIS IS THE ACTIVE VK:', AppState.activeVK)
                }
                catch (error) {
                    Pop.error('[SET ACTIVE KEEP]', error.message)
                    logger.error(error)
                }
            },
            async getProfile(id) {
                try {
                    await profilesService.getProfile(id)
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async goToProfilePage(id) {
                try {
                    await profilesService.getProfile(id)
                    router.push({ name: 'Profile', params: { profileId: id } })
                    Modal.getOrCreateInstance('#KeepCardModal').hide()
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async addKeep() {
                try {

                    const keepId = this.activeKeep.id
                    await vkService.addVaultKeep({
                        ...editable.value,
                        keepId: keepId,
                    })
                    Pop.success(`Added ${this.activeKeep.name} to your vault.`)
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
                        Pop.success('Keep deleted')
                    }
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async removeFromVault(vkId) {
                try {
                    logger.log('vkId', vkId)
                    if (await Pop.confirm("Are you sure you'd like to remove this keep from this album?")) {
                        await vkService.removeFromVault(vkId)
                        Modal.getOrCreateInstance('#KeepCardModal').hide()
                        Pop.success('Keep removed from vault')
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
.vTitle {
    min-height: 20vh;
    width: 100%;
    font-family: 'Marko One', serif;
    text-shadow: 2px 2px 2px black;
}

// .vTitle-2 {
//     font-family: 'Marko One', serif;
//     text-shadow: 2px 2px 2px black;
// }

.move-stuff {
    transform: translateY(-5vh);
    color: white;
    text-shadow: 2px 2px 1px black;
    position: absolute;
}

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
    color: #000000;
}

.keep-img {
    max-height: 85vh;
}
</style>