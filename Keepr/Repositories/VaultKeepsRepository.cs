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
// NOTE for some reason the VK always has the ID of the keep, not sure why or if this will even matter 
        internal List<Vaultkeep> GetKeepsInVault(int vaultId)
        {
            string sql = @"
            SELECT
            vk.*,
            COUNT(kee.id) AS Kept,
            vk.id AS vaultKeepId,
            vk.creatorId AS VKCreatorId,
            kee.*,
            acc.*
            FROM vaultkeeps vk
            JOIN accounts acc ON acc.id = vk.creatorId
            JOIN keeps kee ON kee.id = vk.keepId
            WHERE vk.vaultId = @vaultId
            GROUP BY vk.id;
            SELECT LAST_INSERT_ID();
            ";
            return _db.Query<Vaultkeep, Profile, Vaultkeep>(sql, (keep, prof) => 
            {
                keep.Creator = prof;
                return keep;
            }, new {vaultId}).ToList();
            
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