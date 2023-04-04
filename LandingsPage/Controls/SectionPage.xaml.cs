using System.Linq;
using Microsoft.UI.Xaml.Navigation;

namespace WinUICommunity;
public sealed partial class SectionPage : ItemsPageBase
{
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

        var group = await new ControlInfoDataSource().GetGroupAsync((string) args.Parameter, args.JsonFilePath, args.PathType, args.IncludedInBuildMode);

        if (group != null)
        {
            var menuItem = (Microsoft.UI.Xaml.Controls.NavigationViewItemBase) navigationView.MenuItems.Single(i => (string) ((Microsoft.UI.Xaml.Controls.NavigationViewItemBase) i).Tag == group.UniqueId);
            menuItem.IsSelected = true;
            TitleTxt.Text = group.Title;
            Items = group.Items.Where(i => !i.HideItem).OrderBy(i => i.Title).ToList();
        }
    }
    protected override bool GetIsNarrowLayoutState()
    {
        return LayoutVisualStates.CurrentState == NarrowLayout;
    }
}
