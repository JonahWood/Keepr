namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, img, isPrivate)
            VALUES
            (@creatorId, @name, @description, @img, @isPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal void DeleteVault(int id)
        {
            string sql = @"
            DELETE FROM vaults
            WHERE id = @id;
            ";
            _db.Execute(sql, new {id});
        }

        internal int EditVault(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @name,
            description = @description,
            img = @img,
            isPrivate = @isPrivate

            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, original);
            return rows;
        }

        internal Vault GetOneVault(int id)
        {
            string sql = @"
            SELECT 
            vau.*,
            acc.*
            FROM vaults vau
            JOIN accounts acc ON vau.creatorId = acc.id
            WHERE vau.id = @id
            ";
            Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, prof) => 
            {
                vault.Creator = prof;
                return vault;
            }, new {id}).FirstOrDefault();
            return vault;
        }

        internal List<Vault> GetUserVaults(string userId)
        {
            string sql = @"
            SELECT
            vau.*,
            acc.*
            FROM vaults vau
            JOIN accounts acc ON vau.creatorId = acc.id
            WHERE creatorId = @userId;
            ";
            List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) => 
            {
                vault.Creator = account;
                return vault;
            }, new {userId}).ToList();
            return vaults;
        }

        internal List<Vault> GetVaultsByProfile(string id)
        {
            string sql = @"
            SELECT
            *
            FROM vaults
            WHERE creatorId = @id AND isPrivate = 0;
            ";
            List<Vault> vaults = _db.Query<Vault>(sql, new {id}).ToList();
            return vaults;
        }

        internal List<KV> GetVKByVaultId(int id)
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
            WHERE vk.vaultId = @id
            GROUP BY vk.id;
            ";
            List<KV> keeps = _db.Query<KV, Profile, KV>(sql, (vk, account) => 
            {
                vk.Creator = account;
                return vk;
            }, new {id}).ToList();
            return keeps;
        }
    }
}