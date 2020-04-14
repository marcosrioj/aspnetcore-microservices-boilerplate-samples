namespace MicroserviceBaseProject.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly MicroserviceBaseProjectDbContext _context;

        public InitialHostDbBuilder(MicroserviceBaseProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Create your Default Host Seed

            _context.SaveChanges();
        }
    }
}
