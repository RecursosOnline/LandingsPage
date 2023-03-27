// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUICommunity.Shared.DataModel;
using WinUICommunity.Shared.Navigation;

namespace WinUICommunity.LandingsPage.Controls;

public sealed partial class ItemPage : Page
{
    public Visibility PageHeaderVisibility
    {
        get { return (Visibility) GetValue(PageHeaderVisibilityProperty); }
        set { SetValue(PageHeaderVisibilityProperty, value); }
    }
    public string DocumentationDropDownText
    {
        get { return (string) GetValue(DocumentationDropDownTextProperty); }
        set { SetValue(DocumentationDropDownTextProperty, value); }
    }
    public IconElement DocumentationDropDownIconElement
    {
        get { return (IconElement) GetValue(DocumentationDropDownIconElementProperty); }
        set { SetValue(DocumentationDropDownIconElementProperty, value); }
    }
    public object HeaderLeftContent
    {
        get { return (object) GetValue(HeaderLeftContentProperty); }
        set { SetValue(HeaderLeftContentProperty, value); }
    }
    public object HeaderRightContent
    {
        get { return (object) GetValue(HeaderRightContentProperty); }
        set { SetValue(HeaderRightContentProperty, value); }
    }
    public static readonly DependencyProperty PageHeaderVisibilityProperty =
        DependencyProperty.Register("PageHeaderVisibility", typeof(Visibility), typeof(ItemPage), new PropertyMetadata(Visibility.Visible));
    public static readonly DependencyProperty DocumentationDropDownTextProperty =
        DependencyProperty.Register("DocumentationDropDownText", typeof(string), typeof(ItemPage), new PropertyMetadata("Documentation"));
    public static readonly DependencyProperty DocumentationDropDownIconElementProperty =
        DependencyProperty.Register("DocumentationDropDownIconElement", typeof(IconElement), typeof(ItemPage), new PropertyMetadata(new FontIcon { Glyph = "\xE130" }));
    public static readonly DependencyProperty HeaderLeftContentProperty =
        DependencyProperty.Register("HeaderLeftContent", typeof(object), typeof(ItemPage), new PropertyMetadata(null));
    public static readonly DependencyProperty HeaderRightContentProperty =
        DependencyProperty.Register("HeaderRightContent", typeof(object), typeof(ItemPage), new PropertyMetadata(null));

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
                Assembly assembly;
                if (string.IsNullOrEmpty(item.ApiNamespace))
                {
                    assembly = Application.Current.GetType().Assembly;
                }
                else
                {
                    assembly = Assembly.Load(item.ApiNamespace);
                }
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
