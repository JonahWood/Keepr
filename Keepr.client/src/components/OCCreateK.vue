<template>
    <div class="modal fade" id="CreateKeep" tabindex="-1" aria-labelledby="CreateKeepLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content modal-bg">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="CreateKeepLabel">Add Your Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="mb-2">
                            <label for="name">Name</label>
                            <input maxlength="25" min-length="3" required v-model="editable.name" type="text"
                                class="form-control" id="name">
                        </div>
                        <div class="mb-2">
                            <label for="Description">Description</label>
                            <input maxlength="200" min-length="3" required v-model="editable.Description" type="text"
                                class="form-control" id="Description">
                        </div>
                        <label for="Img">Img</label>
                        <input min-length="20" required v-model="editable.Img" type="text" class="form-control" id="Img">


                        <div class="d-flex justify-content-end">
                            <button class="btn create-button ms-md-2 bg-gradient mt-3" data-bs-dismiss="modal"
                                type="submit">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'

export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        return {
            editable,
            async createKeep() {
                try {
                    const formData = editable.value
                    await keepsService.createKeep(formData)
                    Pop.success('Keep Created')
                    // router.push({ name: 'Keep', params: { keepId: (AppState.createdKeep.id) } })

                    editable.value = {}
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