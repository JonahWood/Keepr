import { logger } from "../utils/Logger";
import { api } from "./AxiosService";
import { AppState } from "../AppState";

class KeepsService{
    async getAllKeeps(){
        const res = await api.get('api/keeps')
        // logger.log(res.data)
        AppState.keeps = res.data
    }

    async setActive(id){
        const res = await api.get('api/keeps/' + id)
        // logger.log('active:', res.data)
        AppState.activeKeep = res.data
    }
}

export const keepsService = new KeepsService();