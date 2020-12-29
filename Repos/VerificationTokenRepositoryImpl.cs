using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class VerificationTokenRepositoryImpl : IVerificationTokenRepository
    {
        public readonly ApplicationContext dbContext ;
        
        public VerificationTokenRepositoryImpl(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void deleteVerificationToken(VerificationToken verificationToken)
        {
             dbContext.tokens.Remove(verificationToken);
        }

         public bool SaveChanges()
         {
            return (dbContext.SaveChanges() >= 0 );
         }
    }
}