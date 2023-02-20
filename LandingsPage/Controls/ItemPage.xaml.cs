// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Reflection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUICommunity.Shared.DataModel;
using WinUICommunity.Shared.Navigation;

namespace WinUICommunity.LandingsPage.Controls;

public sealed partial class ItemPage : Page
{
    public ControlInfoDataItem Item
    {
        get { return _item; }
        set { _item = value; }
    }

    private ControlInfoDataItem _item;

    public ItemPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        GetDataAsync(e);
        base.OnNavigatedTo(e);
    }

    public async void GetDataAsync(NavigationEventArgs e)
    {
        NavigationArgs args = (NavigationArgs) e.Parameter;
        var item = await ControlInfoDataSource.Instance.GetItemAsync((String) args.Parameter, args.JsonRelativeFilePath, args.IncludedInBuildMode);

        if (item != null)
        {
            Item = item;

            // Load control page into frame.
            if (!string.IsNullOrEmpty(item.ApiNamespace))
            {
                Assembly assembly = Assembly.Load(item.ApiNamespace);
                if (assembly is not null)
                {
                    Type pageType = assembly.GetType(item.UniqueId);
                    if (pageType != null)
                    {
                        this.contentFrame.Navigate(pageType);
                        args.NavigationView.EnsureNavigationSelection(item?.UniqueId);
                    }
                }
            }
        }
    }
}
