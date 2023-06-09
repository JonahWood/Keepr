namespace Keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetProfile(string id)
        {
            string sql = @$"
            SELECT
            *
            FROM accounts
            WHERE id = @id;
            ";
            Profile prof = _db.Query<Profile>(sql, new {id}).FirstOrDefault();
            return prof;
        }
    }
}