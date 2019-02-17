using Mehr.Setting;

namespace IranSoftjo.Package.WebUi.Configs
{
    public class PackageSettings
    {
        public readonly static PackageSettings Active;
        public readonly static PackSettingReader SettingReader = new PackSettingReader("Packaging");

        static PackageSettings()
        {
            Active = new PackageSettings()
            {
                Version = SettingReader.Get("Version", "13940204.18"),
                SiteTheme = SettingReader.Get("SiteTheme", "IranSoftjo"),
                SiteTypeCms = SettingReader.Get("SiteTypeCms", false),
                SiteTypeEShop = SettingReader.Get("SiteTypeEShop", false),
                SiteTypePortal = SettingReader.Get("SiteTypePortal", false),
                PhoneBooks = SettingReader.Get("PhoneBooks", false),
                ProductThumbnailImageUrlHeight = SettingReader.Get("ProductThumbnailImageUrlHeight", 150),
                ProductThumbnailImageUrlWidth = SettingReader.Get("ProductThumbnailImageUrlWidth", 150),
                PageThumbnailImageUrlWidth = SettingReader.Get("PageThumbnailImageUrlHeight", 100),
                PageThumbnailImageUrlHeight = SettingReader.Get("PageThumbnailImageUrlWidth", 100),
            };
        }

        public int ProductThumbnailImageUrlHeight { get; private set; }
        public int PageThumbnailImageUrlHeight { get; private set; }
        public int ProductThumbnailImageUrlWidth { get; private set; }
        public int PageThumbnailImageUrlWidth { get; private set; }
        public bool SiteTypeCms { get; private set; }
        public bool SiteTypeEShop { get; private set; }
        public bool SiteTypePortal { get; private set; }
        public bool PhoneBooks { get; private set; }
        public string SiteTheme { get; private set; }
        public string Version { get; private set; }
        
    }
}
