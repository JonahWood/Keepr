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
            Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, prof) => 
            {
                vault.Creator = prof;
                return vault;
            }, new {id}).FirstOrDefault();
            return vault;
        }
    }
}