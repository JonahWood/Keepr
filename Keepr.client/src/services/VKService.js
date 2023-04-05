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

    // FIXME after this request fires off, find the keep that was just added, and make his kept count go up
}

async removeFromVault(vkId){
    const res = await api.delete('api/vaultkeeps/'+vkId)
    const vkIndex = AppState.keeps.findIndex(v => v.vaultKeepId == vkId)
    if (vkIndex >= 0) {
        AppState.keeps.splice(vkIndex, 1)
    }
}

setActiveVK(id){
    const AVK = AppState.keeps.find(k => k.id == id)
    AppState.activeVK = AVK
}
}


export const vkService = new VKService();