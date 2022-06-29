using Repositories.Interfaces;
using Resources.Models;
using Services.Converters;
using Services.Interfaces;
using System.Text;

namespace Services
{
    internal class TextAndImageService : ITextAndImageService
    {
        private readonly ITextRepository _textRepository;
        private readonly IImageRepository _imageRepository;

        public TextAndImageService(ITextRepository textRepository, IImageRepository imageRepository)
        {
            _textRepository = textRepository;
            _imageRepository = imageRepository;
        }

        //Texts
        public async Task<List<TextModel>> GetAllTextModelsAsync()
        {
            var texts = (await _textRepository.GetAllTextEntitiesAsync())
                .Select(t => t.TextEntityToModel())
                .ToList();

            return texts;
        }

        public async Task<TextModel?> GetPublishedTextModelAsync()
        {
            var publishedText = (await _textRepository.GetAllTextEntitiesAsync())
                .FirstOrDefault(t => t.IsPostedOnThePage == true);

            return publishedText?.TextEntityToModel();
        }

        //Images
        public async Task<List<ImageModel>> GetAllImageModelsAsync()
        {
            var images = (await _imageRepository.GetAllImageEntitiesAsync())
                .Select(i => i.ImageEntityToModel())
                .ToList();

            return images;
        }

        public async Task<ImageModel?> GetPublishedImageModelAsync()
        {
            var publishedImage = (await _imageRepository.GetAllImageEntitiesAsync())
                .FirstOrDefault(i => i.IsPostedOnThePage == true);

            return publishedImage?.ImageEntityToModel();
        }

        public async Task<string> GetDefaultHomePageTextAsync()
        {
            string defaultHomePageText;

            string textFile = @"wwwroot\txt\default-texts\home-page-text.txt";
            if (File.Exists(textFile))
            {
                using (FileStream stream = new FileStream(textFile, FileMode.OpenOrCreate))
                {
                    byte[] buffer = new byte[stream.Length];
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                    defaultHomePageText = Encoding.Default.GetString(buffer);
                }
            }
            else
            {
                defaultHomePageText = "Дефольного текста тоже нет. Ничего нет.";
            }

            return defaultHomePageText;
        }

        public string GetDefaultHomePageImage()
        {
            string defaultHomePageImage = @"..\img\default-images\it-consulting-default-1.jpg";

            return defaultHomePageImage;
        }
    }
}
