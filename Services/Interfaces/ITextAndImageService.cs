using Resources.Models;

namespace Services.Interfaces
{
    public interface ITextAndImageService
    {
        public Task<List<TextModel>> GetAllTextModelsAsync();
        public Task<TextModel?> GetPublishedTextModelAsync();
        public Task<List<ImageModel>> GetAllImageModelsAsync();
        public Task<ImageModel?> GetPublishedImageModelAsync();
        public Task<string> GetDefaultHomePageTextAsync();
        public string GetDefaultHomePageImage();

    }
}
