using Resources.Models;

namespace WebAppForAdmins.Models
{
    public class MainViewModel
    {
        public MainPageModel MainPageModel { get; set; } = default!;
        public IList<PresetInfo> PresetInfos { get; set; } = default!;
    }
}
