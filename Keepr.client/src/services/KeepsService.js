import { logger } from "../utils/Logger";
import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { applyStyles } from "@popperjs/core";
import { useRouter } from "vue-router";

class KeepsService{

    async getAllKeeps(){
    AppState.keeps = []
        const res = await api.get('api/keeps')
        // logger.log(res.data)
        AppState.keeps = res.data
    }

    async setActive(id){
        const res = await api.get('api/keeps/' + id)
        // logger.log('active:', res.data)
        AppState.activeKeep = res.data
        // logger.log('creator', res.data.creator)
        if (res.data.creator) {
            AppState.activeCreator = res.data.creator
        }
    }

    async GetMyKeeps(){
    AppState.keeps = []
        const res = await api.get('api/keeps')
        const keeps = res.data.filter(k => k.creatorId == AppState.account.id)
        AppState.keeps = keeps
        logger.log(AppState.keeps)
    }
    async createKeep(formData){
        AppState.createdKeep = {}
const res = await api.post('api/keeps', formData)
AppState.keeps.push(res.data)
AppState.createdKeep = res.data
logger.log('createdKeep:', AppState.createdKeep)

    }

    async getProfileKeeps(profileId){
        const res = await api.get('api/profiles/' + profileId + '/keeps')
        AppState.keeps = res.data
    }

    async deleteKeep(keepId){
        const res = await api.delete('api/keeps/' + keepId)
        const keepIndex = AppState.keeps.findIndex(k => k.id == keepId)
        if (keepIndex >= 0) {
            AppState.keeps.splice(keepIndex, 1)
        }
    }
}

export const keepsService = new KeepsService();