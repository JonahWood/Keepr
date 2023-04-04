<!-- FIXME something wacky is going on when trying to see keep details when youre not logged in -->
<!-- FIXME also theres some horizontal scroll because of some goofy thing i did -->
<!-- FIXME the spacing of the last part of the modal is kinda being funky, gonna need to take a look at that -->
<template>
    <div @click="setActive(keep?.id)" class="selectable">
        <div>
            <img class="img-fluid" :src="keep?.img" alt="">
        </div>
        <div class="d-flex justify-content-between mt-1">
            {{ keep?.name }}
            <img :title="keep?.creator.name" class="profile-picture" :src="keep?.creator.picture" alt="">
        </div>
    </div>
</template>


<script>
import { onMounted, computed } from 'vue';
import { AppState } from "../AppState";
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService'
import { Modal } from 'bootstrap';


export default {
    props: { keep: { type: Object, required: true } },
    setup() {
        return {
            activeKeep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            async setActive(id) {
                try {
                    await keepsService.setActive(id)
                    Modal.getOrCreateInstance("#KeepCardModal").show()
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