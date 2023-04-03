<template>
    <div class="modal fade" id="CreateVault" tabindex="-1" aria-labelledby="CreateVaultLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content modal-bg">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="CreateVaultLabel">Open Your Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="mb-2">
                            <label for="name">Name</label>
                            <input required v-model="editable.name" type="text" class="form-control" id="name">
                        </div>
                        <div class="mb-2">
                            <label for="Description">Description</label>
                            <input required v-model="editable.Description" type="text" class="form-control"
                                id="Description">
                        </div>
                        <label for="Img">Img</label>
                        <input required v-model="editable.Img" type="text" class="form-control" id="Img">


                        <div class="d-flex justify-content-end">
                            <button class="btn create-button ms-md-2 bg-gradient mt-3"
                                data-bs-dismiss="modal">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { router } from '../router';
import { useRouter } from 'vue-router';

export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        return {
            editable,
            async createVault() {
                try {
                    const formData = editable.value
                    const vault = await vaultsService.createVault(formData)
                    router.push({ name: 'Vault', params: { vaultId: vault.id } })
                    Pop.success('Vault Opened')
                    editable.value = []
                } catch (error) {
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