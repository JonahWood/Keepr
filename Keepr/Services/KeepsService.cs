namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repo.CreateKeep(keepData);
            return keep;
        }

        internal string DeleteKeep(int id)
        {
            Keep keep = _repo.GetOneKeep(id);
            _repo.DeleteKeep(id);
            return "Keep was unkept";
        }

        internal List<Keep> GetAllKeeps(string userId)
        {
            List<Keep> keeps = _repo.GetAllKeeps();
            return keeps;
        }

        internal Keep GetOneKeep(int id, string userId)
        {
            Keep keep = _repo.GetOneKeep(id);
            if(keep == null) throw new Exception("There is no Keep with this id");
            // if (keep.CreatorId != userId)
            // // NOTE could do views this way
            // keep.Views++;
            return keep;
        }

        internal Keep UpdateKeep(Keep updateData)
        {
            Keep original = this.GetOneKeep(updateData.Id, updateData.CreatorId);
            original.Name = updateData.Name != null ? updateData.Name : original.Name;
            original.Description = updateData.Description != null ? updateData.Description : original.Description;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.Kept = updateData.Kept != 0 ? updateData.Kept : original.Kept;
            _repo.UpdateKeep(original);
            return original;
        }
    }
}