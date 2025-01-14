using System.IO;
using System.Reflection;

namespace Hymnal.Core.Services
{
    public interface IAssetsService
    {
        Stream GetResourceStream(Assembly assembly, string resourceFileName);
        string GetResourceString(Assembly assembly, string resourceFileName);
        string GetResourceString(string resourceFileName);
    }
}
