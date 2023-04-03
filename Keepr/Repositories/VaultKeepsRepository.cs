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

        internal Vaultkeep GetOne(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaultkeeps
            WHERE id = @id
            ";
            Vaultkeep vaultkeep = _db.Query<Vaultkeep>(sql, new {id}).FirstOrDefault();
            return vaultkeep;
        }

// NOTE doesnt quite pass the test but still works? we'll see how it goes on the front end lol
        internal List<Vaultkeep> GetKeepsInVault(int vaultId)
        {
            string sql = @"
            SELECT
            acc.*,
            keeps.*,
            vau.*,
            vk.id AS vkId
            FROM vaultkeeps vk
            JOIN accounts acc ON vk.creatorId = acc.id
            JOIN keeps keeps ON vk.keepId = keeps.id
            JOIN vaults vau ON vk.vaultId = vau.id
            WHERE vk.vaultId = @vaultId AND vau.isPrivate != true;
            ";
            List<Vaultkeep> vaultkeeps = _db.Query<Vaultkeep>(sql, new {vaultId}).ToList();
            return vaultkeeps;
        }

        internal void DeleteVK(int id)
        {
            string sql = @"
            DELETE FROM vaultkeeps
            WHERE id = @id;
            ";
            _db.Execute(sql, new {id});
        }
    }
}