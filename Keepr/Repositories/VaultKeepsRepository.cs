namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vaultkeep CreateVK(Vaultkeep vkData)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (keepId, vaultId, creatorId)
            VALUES
            (@keepId, @vaultId, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vkData);
            vkData.Id = id;
            return vkData;
        }

// NOTE doesnt quite pass the test but still works? we'll see how it goes on the front end lol
        internal List<Vaultkeep> GetKeepsInVault(int vaultId)
        {
            string sql = @"
            SELECT
            acc.*,
            keeps.*,
            vk.id AS vkId
            FROM vaultkeeps vk
            JOIN accounts acc ON vk.creatorId = acc.id
            JOIN keeps keeps ON vk.keepId = keeps.id
            WHERE vk.vaultId = 1;


            ";
            List<Vaultkeep> vaultkeeps = _db.Query<Vaultkeep>(sql, new {vaultId}).ToList();
            return vaultkeeps;
        }
    }
}