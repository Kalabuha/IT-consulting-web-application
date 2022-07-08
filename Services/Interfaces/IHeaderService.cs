using Resources.Models;

namespace Services.Interfaces
{
    public interface IHeaderService
    {
        public Task<HeaderModel> GetHeaderModelAsync();
    }
}
