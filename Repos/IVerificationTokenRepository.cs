using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IVerificationTokenRepository
    {
        void deleteVerificationToken(VerificationToken verificationToken);
        public bool SaveChanges();

    }
}