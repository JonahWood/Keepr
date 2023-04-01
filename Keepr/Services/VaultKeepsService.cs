namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal Vaultkeep CreateVK(Vaultkeep vkData)
        {
            Vaultkeep vaultkeep = _repo.CreateVK(vkData);
            return vaultkeep;
        }

        internal string DeleteVK(int id, string userId)
        {
            Vaultkeep vaultkeep = _repo.GetOne(id);
            if (vaultkeep == null) throw new Exception($"There is no VaultKeep at the id of {id}");
            if (vaultkeep.CreatorId != userId) throw new Exception("You do not have permissions to remove this VaultKeep");
            _repo.DeleteVK(id);
            return $"VaultKeep with the id of {id} has been neutralized sir.";
        }

        internal List<Vaultkeep> GetKeepsInVault(int vaultId)
        {
            List<Vaultkeep> vaultkeeps = _repo.GetKeepsInVault(vaultId);
            return vaultkeeps;
        }
    }
}