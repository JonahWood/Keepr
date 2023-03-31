namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (creatorId, name, description, img, views, kept)
            VALUES
            (@creatorId, @name, @description, @img, @views, @kept);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        internal void DeleteKeep(int id)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @id;
            ";
            _db.Execute(sql, new {id});
        }

        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            ke.*,
            acc.*
            FROM keeps ke
            JOIN accounts acc ON ke.creatorId = acc.id;
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, prof)=>
            {
                keep.Creator = prof;
                return keep;
            }).ToList();
            return keeps;
        }

        internal Keep GetOneKeep(int id)
        {
            string sql = @"
            UPDATE keeps
            SET
            views = views + 1
            WHERE id = @id;

            SELECT 
            ke.*,
            acc.*
            FROM keeps ke
            JOIN accounts acc ON ke.creatorId = acc.id
            WHERE ke.id = @id;
            ";
            Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) => 
            {
                keep.Creator = prof;
                return keep;
            }, new {id}).FirstOrDefault();
            return keep;
        }

        internal int UpdateKeep(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @name,
            description = @description,
            img = @img,
            views = @views,
            kept = @kept

            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, original);
            return rows;
        }
    }
}