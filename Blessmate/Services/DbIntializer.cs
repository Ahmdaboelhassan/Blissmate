using Blessmate.Data;
using Blessmate.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Blessmate.Helpers
{
    public class DbIntializer : IDbIntializer
    {   
        private readonly AppDbContext _db;
        private readonly  ILogger _logger;
        public DbIntializer(AppDbContext db , ILogger<DbIntializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Init(){

            try{
                if(_db.Database.GetPendingMigrations().Count() > 0){
                    _db.Database.Migrate();
                }
                _logger.LogInformation("DbIntializer Worked Will");
            }
            catch(Exception ex){
                _logger.LogError("Something Wrong in DbIntializer");
                _logger.LogInformation(ex.Message);
            }

        }
        
    }
}