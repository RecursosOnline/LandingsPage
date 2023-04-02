using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

namespace WinUICommunity;
public sealed partial class SectionPage : ItemsPageBase
{
    public PathType PathType
    {
        get { return (PathType) GetValue(PathTypeProperty); }
        set { SetValue(PathTypeProperty, value); }
    }
    public static readonly DependencyProperty PathTypeProperty =
       DependencyProperty.Register("PathType", typeof(PathType), typeof(SectionPage), new PropertyMetadata(PathType.Relative));

    public SectionPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        GetDataAsync(e);
    }

    public async void GetDataAsync(NavigationEventArgs e)
    {
        NavigationArgs args = (NavigationArgs) e.Parameter;
        var navigationView = args.NavigationView;

        var group = await ControlInfoDataSource.Instance.GetGroupAsync((string) args.Parameter, args.JsonFilePath, PathType, args.IncludedInBuildMode);

        var menuItem = (Microsoft.UI.Xaml.Controls.NavigationViewItemBase) navigationView.MenuItems.Single(i => (string) ((Microsoft.UI.Xaml.Controls.NavigationViewItemBase) i).Tag == group.UniqueId);
        menuItem.IsSelected = true;
        TitleTxt.Text = group.Title;
        Items = group.Items.Where(i => !i.HideItem).OrderBy(i => i.Title).ToList();
    }
    protected override bool GetIsNarrowLayoutState()
    {
        return LayoutVisualStates.CurrentState == NarrowLayout;
    }
}
