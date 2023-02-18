// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System.Collections.ObjectModel;
using System.Linq;
using WinUICommunity.LandingsPage.DataModel;

namespace WinUICommunity.LandingsPage.Controls;
public sealed partial class MainLandingsPage : ItemsPageBase
{
    public string NewGroupText { get; set; } = "Recently added";
    public string UpdatedGroupText { get; set; } = "Recently updated";
    public string PreviewGroupText { get; set; } = "Preview";

    public string Title
    {
        get { return (string) GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public string Description
    {
        get { return (string) GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }

    public object HeaderContent
    {
        get { return (object) GetValue(HeaderContentProperty); }
        set { SetValue(HeaderContentProperty, value); }
    }

    public string HomePageHeaderImage
    {
        get { return (string) GetValue(HomePageHeaderImageProperty); }
        set { SetValue(HomePageHeaderImageProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HomePageHeaderImageProperty = DependencyProperty.Register("HomePageHeaderImage", typeof(string), typeof(MainLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(MainLandingsPage), new PropertyMetadata(null));


    public MainLandingsPage()
    {
        this.InitializeComponent();
    }

    public async void GetDataAsync(string JsonRelativeFilePath, IncludedInBuildMode IncludedInBuildMode = IncludedInBuildMode.CheckBasedOnIncludedInBuildProperty)
    {
        await ControlInfoDataSource.Instance.GetGroupsAsync(JsonRelativeFilePath, IncludedInBuildMode);
        Items = ControlInfoDataSource.Instance.Groups.SelectMany(g => g.Items.Where(i => i.BadgeString != null)).OrderBy(i => i.Title).ToList();
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
