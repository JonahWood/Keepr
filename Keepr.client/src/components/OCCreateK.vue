<template>
    <div class="modal fade" id="CreateVault" tabindex="-1" aria-labelledby="CreateVaultLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content modal-bg">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="CreateVaultLabel">Add Your Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">


                        <div class="d-flex justify-content-end">
                            <button class="btn create-button ms-md-2 bg-gradient" data-bs-dismiss="modal">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    setup() {
        return {
            async createVault() {
                try {
                    await vaultsService.createVault()
                } catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async createKeep() {
                try {
                    await keepsService.createKeep()
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
.modal-bg {
    background-color: #F9F6FA;
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
</style>