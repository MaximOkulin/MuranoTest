using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.Business.Interfaces
{
    public interface IEngine
    {
        Task<IResponse> ExecuteAsync(string query, CancellationToken token);
        string EngineName { get; }
    }
}
