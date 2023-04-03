import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService{
async getMyVaults(){
    const res = await api.get('account/vaults')
    AppState.vaults = res.data
}
async createVault(formData){
logger.log('creating vault')
}

async getVaultById(vaultId){
AppState.activeVault = null
const res = await api.get('api/vaults/' + vaultId)
AppState.activeVault = res.data
logger.log('vault:', res.data)
}
}

export const vaultsService = new VaultsService();