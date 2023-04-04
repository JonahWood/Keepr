import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VKService{
async getVaultKeeps(vaultId){
    AppState.keeps = []
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    logger.log('ze keeps in this vault:', res.data)
    AppState.keeps = res.data
}

async addVaultKeep(VKdata){

    const res = await api.post('api/vaultkeeps', VKdata)
    
    logger.log('add vk', res.data)
}

async removeFromVault(keepId){
    logger.log('vkservice 21:', keepId)
    // const res = await api.get
}
}


export const vkService = new VKService();