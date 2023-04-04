import { logger } from "../utils/Logger";
import { api } from "./AxiosService";
import { AppState } from "../AppState";

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
        AppState.activeCreator = res.data.creator
    }

    async GetMyKeeps(){
    AppState.keeps = []
        const res = await api.get('api/keeps')
        const keeps = res.data.filter(k => k.creatorId == AppState.account.id)
        AppState.keeps = keeps
        logger.log(AppState.keeps)
    }
    async createKeep(formData){
const res = await api.post('api/keeps', formData)
AppState.keeps.push(res.data)
    }
}

export const keepsService = new KeepsService();