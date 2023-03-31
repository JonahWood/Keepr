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

        internal List<Vaultkeep> GetKeepsInVault(int vaultId)
        {
            List<Vaultkeep> vaultkeeps = _repo.GetKeepsInVault(vaultId);
            return vaultkeeps;
        }
    }
}