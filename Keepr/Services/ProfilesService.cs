namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfile(string id)
        {
            Profile prof = _repo.GetProfile(id);
            return prof;
        }
    }
}