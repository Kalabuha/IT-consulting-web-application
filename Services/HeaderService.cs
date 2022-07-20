using Repositories.Interfaces;
using Resources.Entities;
using Resources.Models;
using Services.Interfaces;
using Services.Common;

namespace Services
{
    internal class HeaderService : DefaultDataService, IHeaderService
    {
        private readonly IHeaderMenuSetRepository _headerMenuSetRepository;
        private readonly IHeaderSloganRepository _headerSloganRepository;

        private readonly Random _random;

        public HeaderService(IHeaderMenuSetRepository headerMenuSetRepository, IHeaderSloganRepository headerSloganRepository)
        {
            _headerMenuSetRepository = headerMenuSetRepository;
            _headerSloganRepository = headerSloganRepository;

            _random = new Random();
        }

        public async Task<HeaderModel> GetPublishedHeaderModelAsync()
        {
            var postedMenuSet = await _headerMenuSetRepository.GetPostedHeaderMenuEntitiesAsync();
            postedMenuSet ??= new HeaderMenuSetEntity
            {
                Id = 0,
                Main = nameof(HeaderModel.Main),
                Services = nameof(HeaderModel.Services),
                Projects = nameof(HeaderModel.Projects),
                Blogs = nameof(HeaderModel.Blogs),
                Contacts = nameof(HeaderModel.Contacts),
                IsPublishedOnMainPage = true
            };

            var usedHeaderSlogans = await _headerSloganRepository.GetUsedHeaderSloganEntitiesAsync();
            var randomHeaderSlogan = usedHeaderSlogans.Length > 0 ? usedHeaderSlogans[_random.Next(usedHeaderSlogans.Length)] : null;
            randomHeaderSlogan ??= new HeaderSloganEntity
            {
                Id = 0,
                Slogan = await GetDefaultTextFromFileAsync("slogan.txt"),
                IsUsed = true
            };

            return new HeaderModel
            {
                Id = postedMenuSet.Id,
                Main = postedMenuSet.Main,
                Services = postedMenuSet.Services,
                Projects = postedMenuSet.Projects,
                Blogs = postedMenuSet.Blogs,
                Contacts = postedMenuSet.Contacts,

                Slogan = randomHeaderSlogan.Slogan
            };
        }
    }
}
