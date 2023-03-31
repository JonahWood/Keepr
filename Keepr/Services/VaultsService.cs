namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
private readonly VaultKeepsService _vaultKeepsService;

        public VaultsService(VaultsRepository repo, VaultKeepsService vaultKeepsService)
        {
            _repo = repo;
            _vaultKeepsService = vaultKeepsService;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repo.CreateVault(vaultData);
            return vault;
        }

        internal string DeleteVault(int id, string userId)
        {
            Vault vault = _repo.GetOneVault(id);
            if(vault == null) throw new Exception("There is no vault with that id");
            if(vault.CreatorId != userId) throw new Exception("This vault does not belong to you");
            _repo.DeleteVault(id);
            return $"{vault.Name} has been compressed into dust.";
        }

        internal Vault EditVault(Vault updateData)
        {
            Vault original = this.GetOneVault(updateData.Id, updateData.CreatorId);
            original.Name = updateData.Name != null ? updateData.Name : original.Name;
            original.Description = updateData.Description != null ? updateData.Description : original.Description;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.IsPrivate = updateData.IsPrivate ? true : false;
            _repo.EditVault(original);
            return original;
        }

        internal List<Vaultkeep> GetKeepsInVault(int id, string userId)
        {
            Vault vault = this.GetOneVault(id, userId);
            List<Vaultkeep> vaultkeeps = _vaultKeepsService.GetKeepsInVault(id);
            return vaultkeeps;
            }

        internal Vault GetOneVault(int id, string userId)
        {
            Vault vault = _repo.GetOneVault(id);
            if(vault == null) throw new Exception("There is no vault with that id");
            if(vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("This album has been made private");
            return vault;
        }
    }
}