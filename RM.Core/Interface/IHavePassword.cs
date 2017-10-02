using System.Security;

namespace RM.Core
{
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
}
