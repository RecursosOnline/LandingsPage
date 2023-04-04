using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace WinUICommunity;
public sealed partial class MainLandingsPage : ItemsPageBase
{
    public string NewGroupText { get; set; } = "Recently added";
    public string UpdatedGroupText { get; set; } = "Recently updated";
    public string PreviewGroupText { get; set; } = "Preview";

    public string HeaderText
    {
        get { return (string) GetValue(HeaderTextProperty); }
        set { SetValue(HeaderTextProperty, value); }
    }

    public string HeaderSubtitleText
    {
        get { return (string) GetValue(HeaderSubtitleTextProperty); }
        set { SetValue(HeaderSubtitleTextProperty, value); }
    }

    public object HeaderContent
    {
        get { return (object) GetValue(HeaderContentProperty); }
        set { SetValue(HeaderContentProperty, value); }
    }

    public string HeaderImage
    {
        get { return (string) GetValue(HeaderImageProperty); }
        set { SetValue(HeaderImageProperty, value); }
    }

    public double HeaderImageHeight
    {
        get { return (double) GetValue(HeaderImageHeightProperty); }
        set { SetValue(HeaderImageHeightProperty, value); }
    }
    public Thickness HeaderMargin
    {
        get { return (Thickness) GetValue(HeaderMarginProperty); }
        set { SetValue(HeaderMarginProperty, value); }
    }
    public object FooterContent
    {
        get { return (object) GetValue(FooterContentProperty); }
        set { SetValue(FooterContentProperty, value); }
    }
    public double FooterHeight
    {
        get { return (double) GetValue(FooterHeightProperty); }
        set { SetValue(FooterHeightProperty, value); }
    }
    public Thickness FooterMargin
    {
        get { return (Thickness) GetValue(FooterMarginProperty); }
        set { SetValue(FooterMarginProperty, value); }
    }
    public Brush HeaderForeground
    {
        get { return (Brush) GetValue(HeaderForegroundProperty); }
        set { SetValue(HeaderForegroundProperty, value); }
    }
    public Brush HeaderSubtitleForeground
    {
        get { return (Brush) GetValue(HeaderSubtitleForegroundProperty); }
        set { SetValue(HeaderSubtitleForegroundProperty, value); }
    }
    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderSubtitleTextProperty = DependencyProperty.Register("HeaderSubtitleText", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(MainLandingsPage), new PropertyMetadata(null));
    public static readonly DependencyProperty HeaderImageHeightProperty = DependencyProperty.Register("HeaderImageHeight", typeof(double), typeof(MainLandingsPage), new PropertyMetadata(396.0));
    public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(MainLandingsPage), new PropertyMetadata(new Thickness(-24, 0, -24, 0)));
    public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register("FooterContent", typeof(object), typeof(MainLandingsPage), new PropertyMetadata(null));
    public static readonly DependencyProperty FooterHeightProperty = DependencyProperty.Register("FooterHeight", typeof(double), typeof(MainLandingsPage), new PropertyMetadata(200.0));
    public static readonly DependencyProperty FooterMarginProperty = DependencyProperty.Register("FooterMargin", typeof(Thickness), typeof(MainLandingsPage), new PropertyMetadata(new Thickness(16,34,48,0)));
    public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(MainLandingsPage), new PropertyMetadata(Application.Current.Resources["TextFillColorPrimaryBrush"] as Brush));
    public static readonly DependencyProperty HeaderSubtitleForegroundProperty = DependencyProperty.Register("HeaderSubtitleForeground", typeof(Brush), typeof(MainLandingsPage), new PropertyMetadata(Application.Current.Resources["TextFillColorPrimaryBrush"] as Brush));

    public MainLandingsPage()
    {
        this.InitializeComponent();
    }

    public async void GetDataAsync(string JsonFilePath, PathType pathType, IncludedInBuildMode IncludedInBuildMode = IncludedInBuildMode.CheckBasedOnIncludedInBuildProperty)
    {
        var dataSource = new ControlInfoDataSource();
        await dataSource.GetGroupsAsync(JsonFilePath, pathType, IncludedInBuildMode);
        Items = dataSource.Groups.Where(g => !g.HideGroup).SelectMany(g => g.Items.Where(i => i.BadgeString != null && !i.HideItem)).OrderBy(i => i.Title).ToList();
        GetCollectionViewSource().Source = FormatData();
    }

    public CollectionViewSource GetCollectionViewSource()
    {
        return itemsCVS;
    }

    public ObservableCollection<GroupInfoList> FormatData()
    {
        var query = from item in this.Items
                    group item by item.BadgeString into g
                    orderby g.Key
                    select new GroupInfoList(g) { Key = g.Key };

        ObservableCollection<GroupInfoList> groupList = new ObservableCollection<GroupInfoList>(query);

        if (groupList.Any())
        {
            //Move Preview to the back of the list
            foreach (var item in groupList?.ToList())
            {
                if (item?.Key.ToString() == "Preview")
                {
                    groupList?.Remove(item);
                    groupList?.Insert(groupList.Count, item);
                }
            }
            

            foreach (var item in groupList)
            {
                switch (item.Key.ToString())
                {
                    case "New":
                        item.Title = NewGroupText;
                        break;
                    case "Updated":
                        item.Title = UpdatedGroupText;
                        break;
                    case "Preview":
                        item.Title = PreviewGroupText;
                        break;
                }
            }

            return groupList;
        }
        return null;
    }

    protected override bool GetIsNarrowLayoutState()
    {
        return LayoutVisualStates.CurrentState == NarrowLayout;
    }
}
