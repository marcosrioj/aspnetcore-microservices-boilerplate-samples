namespace MicroserviceSample.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly MicroserviceSampleDbContext _context;

        public InitialHostDbBuilder(MicroserviceSampleDbContext context)
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
