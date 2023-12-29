using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;

namespace Experimentum.Domain.Abstractions
{
    public interface IContactable
    {
        public Result<Phone> RemovePhone(Phone phone);
    }
}
