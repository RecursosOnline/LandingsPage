// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Linq;
using Microsoft.UI.Xaml;
using WinUICommunity.Shared.DataModel;

namespace WinUICommunity.LandingsPage.Controls;
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

    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(AllLandingsPage), new PropertyMetadata("All"));
    public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage", typeof(string), typeof(AllLandingsPage), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty HeaderImageHeightProperty = DependencyProperty.Register("HeaderImageHeight", typeof(double), typeof(AllLandingsPage), new PropertyMetadata(200.0));

    public AllLandingsPage()
    {
        this.InitializeComponent();
    }

    public async void GetDataAsync(string JsonRelativeFilePath, IncludedInBuildMode IncludedInBuildMode = IncludedInBuildMode.CheckBasedOnIncludedInBuildProperty)
    {
        await ControlInfoDataSource.Instance.GetGroupsAsync(JsonRelativeFilePath, IncludedInBuildMode);
        Items = ControlInfoDataSource.Instance.Groups.Where(g => !g.IsSpecialSection && !g.HideGroup).SelectMany(g => g.Items.Where(i => !i.HideItem)).OrderBy(i => i.Title).ToList();
    }
}
