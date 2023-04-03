import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VKService{
async getVaultKeeps(vaultId){
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    logger.log('ze keeps in this vault:', res.data)
    AppState.keeps = []
}
}


export const vkService = new VKService();