using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace WinUICommunity;
public sealed partial class AllLandingsPage : ItemsPageBase
{
    public string HeaderImage
    {
        get { return (string) GetValue(HeaderImageProperty); }
        set { SetValue(HeaderImageProperty, value); }
    }

    public string HeaderText
    {
        get { return (string) GetValue(HeaderTextProperty); }
        set { SetValue(HeaderTextProperty, value); }
    }

    public double HeaderImageHeight
    {
        get { return (double) GetValue(HeaderImageHeightProperty); }
        set { SetValue(HeaderImageHeightProperty, value); }
    }
    public Brush HeaderForeground
    {
        get { return (Brush) GetValue(HeaderForegroundProperty); }
        set { SetValue(HeaderForegroundProperty, value); }
    }

    public ImageSource PlaceholderSource
    {
        get { return (ImageSource) GetValue(PlaceholderSourceProperty); }
        set { SetValue(PlaceholderSourceProperty, value); }
    }
    public bool IsCacheEnabled
    {
        get { return (bool) GetValue(IsCacheEnabledProperty); }
        set { SetValue(IsCacheEnabledProperty, value); }
    }
    public bool EnableLazyLoading
    {
        get { return (bool) GetValue(EnableLazyLoadingProperty); }
        set { SetValue(EnableLazyLoadingProperty, value); }
    }
    public double LazyLoadingThreshold
    {
        get { return (double) GetValue(LazyLoadingThresholdProperty); }
        set { SetValue(LazyLoadingThresholdProperty, value); }
    }

    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(AllLandingsPage), new PropertyMetadata("All"));
    public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage", typeof(string), typeof(AllLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderImageHeightProperty = DependencyProperty.Register("HeaderImageHeight", typeof(double), typeof(AllLandingsPage), new PropertyMetadata(200.0));
    public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(AllLandingsPage), new PropertyMetadata(Application.Current.Resources["TextFillColorPrimaryBrush"] as Brush));
    public static readonly DependencyProperty PlaceholderSourceProperty = DependencyProperty.Register("PlaceholderSource", typeof(ImageSource), typeof(AllLandingsPage), new PropertyMetadata(default(ImageSource)));
    public static readonly DependencyProperty IsCacheEnabledProperty = DependencyProperty.Register("IsCacheEnabled", typeof(bool), typeof(AllLandingsPage), new PropertyMetadata(true));
    public static readonly DependencyProperty EnableLazyLoadingProperty = DependencyProperty.Register("EnableLazyLoading", typeof(bool), typeof(AllLandingsPage), new PropertyMetadata(true));
    public static readonly DependencyProperty LazyLoadingThresholdProperty = DependencyProperty.Register("LazyLoadingThreshold", typeof(double), typeof(AllLandingsPage), new PropertyMetadata(300.0));

    public AllLandingsPage()
    {
        this.InitializeComponent();
    }

    public async void GetDataAsync(string JsonFilePath, PathType pathType, IncludedInBuildMode IncludedInBuildMode = IncludedInBuildMode.CheckBasedOnIncludedInBuildProperty)
    {
        var dataSource = new ControlInfoDataSource();
        await dataSource.GetGroupsAsync(JsonFilePath, pathType, IncludedInBuildMode);
        Items = dataSource.Groups.Where(g => !g.IsSpecialSection && !g.HideGroup).SelectMany(g => g.Items.Where(i => !i.HideItem)).OrderBy(i => i.Title).ToList();
    }
}
