import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService{
async getMyVaults(){
    const res = await api.get('account/vaults')
    AppState.vaults = res.data
}
}

export const vaultsService = new VaultsService();