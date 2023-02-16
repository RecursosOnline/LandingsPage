<p align="center">
    <img alt="dotnet" src="https://img.shields.io/badge/.net-%3E=6.0-brightgreen"/>
    <img alt="os-require" src="https://img.shields.io/badge/OS-%3E%3D%20Windows%2010%20Build%201809-orange"/>
    <img alt="IDE-version" src="https://img.shields.io/badge/IDE-vs2022-red"/>
    <img alt="csharp-require" src="https://img.shields.io/badge/CSharp-Latest-yellow"/>
    <a href="https://github.com/WinUICommunity">
        <img alt="projects" src="https://img.shields.io/badge/WinUICommunity-Projects-green"></img>
    </a> 
    <a href="https://www.nuget.org/profiles/WinUICommunity">
        <img alt="WinuiCommunity Nugets" src="https://img.shields.io/badge/WinUICommunity-Nugets-green"></img>
    </a> 
    <a href="https://www.nuget.org/packages/WinUICommunity.LandingsPage">
        <img alt="nuget-version" src="https://img.shields.io/nuget/v/WinUICommunity.LandingsPage.svg"></img>
    </a> 
    <a href="https://www.nuget.org/packages/WinUICommunity.LandingsPage">
        <img alt="Installed" src="https://img.shields.io/nuget/dt/WinUICommunity.LandingsPage?color=brightgreen&label=Installs"></img>
    </a> 
    <a href="https://ghost1372.github.io/winUICommunity/">
        <img alt="Docs" src="https://img.shields.io/badge/Document-Here-critical"></img>
    </a> 
</p>

<br>
<p align="center">
	<b>🙌 Donate Bitcoin with <a href="https://link.trustwallet.com/send?coin=0&address=bc1qzs4kt4aeqym6gsde669g5rksv4swjhzjqqp23a">Trust</a>🙌</b><br>
	<b>🙌 Donate ETH with <a href="https://link.trustwallet.com/send?coin=60&address=0x40Db4476c1D498b167f76A2c7ED9D45b65eb5d0C">Trust</a>🙌</b><br><br>
	<b>🙌 Bitcoin: bc1qzs4kt4aeqym6gsde669g5rksv4swjhzjqqp23a<br></b>
	<b>🙌 ETH: 0x40Db4476c1D498b167f76A2c7ED9D45b65eb5d0C</b>
</p>
<br>

# LandingsPage
 
### Create a landing page in the style of WinUI 3 and WinUI-Gallery very quickly and easily

> **_NOTE:_** LandingsPage is based on `WindowsAppSDK` version `1.2.230118.102` stable and `Microsoft.Windows.SDK.BuildTools` version `10.0.22621.755`

## Dependencies

This package is based on the following packages

- CommunityToolkit.WinUI.UI
- CommunityToolkit.WinUI.UI.Animations
- Microsoft.Graphics.Win2D

## Install
```
Install-Package WinUICommunity.LandingsPage
```

After installing, add the following resources to app.xaml

```xml
xmlns:themes="using:WinUICommunity.LandingsPage.Themes"

<themes:ItemTemplates/>
<ResourceDictionary Source="ms-appx:///LandingsPage/Themes/Generic.xaml"/>
```

now:

```xml
xmlns:controls="using:WinUICommunity.LandingsPage.Controls"
```

use MainLandingsPage:

```xml
<controls:MainLandingsPage x:Name="mainLandingsPage" Loaded="mainLandingsPage_Loaded"
                    HomePageHeaderImage="ms-appx:///Assets/GalleryHeaderImage.png"
                    Title="WinUI 3 Gallery"
                    Description="WinAppSDK 1.2"
                    OnItemClick="mainLandingsPage_OnItemClick">
    <controls:MainLandingsPage.HeaderContent>
        <StackPanel Orientation="Horizontal" Spacing="10">
            <controls:HeaderTile
                OnItemClick="HeaderTile_OnHeaderItemClick"
                Title="Getting started"
                Description="An overview of app development options, tools, and samples.">
                <controls:HeaderTile.Source>
                    <Image Source="/Assets/HomeHeaderTiles/Header-WinUIGallery.png" />
                </controls:HeaderTile.Source>
            </controls:HeaderTile>
            <controls:HeaderTile
                Title="Windows design"
                Description="Design guidelines and toolkits for creating native app experiences."
                Link="https://docs.microsoft.com/windows/apps/design/">
                <controls:HeaderTile.Source>
                    <Image Source="/Assets/HomeHeaderTiles/Header-WindowsDesign.png" />
                </controls:HeaderTile.Source>
            </controls:HeaderTile>
            <controls:HeaderTile
                Title="Community Toolkit"
                Description="A collection of helper functions, custom controls, and app services."
                Link="https://apps.microsoft.com/store/detail/windows-community-toolkit-sample-app/9NBLGGH4TLCQ">
                <controls:HeaderTile.Source>
                    <Image Source="/Assets/HomeHeaderTiles/Header-Toolkit.png" />
                </controls:HeaderTile.Source>
            </controls:HeaderTile>
            <controls:HeaderTile
                Title="Code samples"
                Description="Find samples that demonstrate specific tasks, features, and APIs."
                Link="https://docs.microsoft.com/windows/apps/get-started/samples">
                <controls:HeaderTile.Source>
                    <FontIcon
                        Margin="0,8,0,0"
                        FontSize="44"
                        Foreground="{ThemeResource TextFillColorPrimaryBrush}"
                        Glyph="&#xE943;" />
                </controls:HeaderTile.Source>
            </controls:HeaderTile>
        </StackPanel>
    </controls:MainLandingsPage.HeaderContent>
</controls:MainLandingsPage>
```

in code-behind:
```cs
private void mainLandingsPage_Loaded(object sender, RoutedEventArgs e)
{
    mainLandingsPage.GetControlInfoDataAsync("DataModel/ControlInfoData.json");
}

private void mainLandingsPage_OnItemClick(object sender, RoutedEventArgs e)
{
}

private void HeaderTile_OnHeaderItemClick(object sender, RoutedEventArgs e)
{
}
```

now create `ControlInfoData.json` in DataModel (Build Action = Content)

```json
{
  "Groups": [
    {
      "UniqueId": "Design_Guidance",
      "Title": "Design guidance",
      "ApiNamespace": "",
      "Subtitle": "",
      "ImagePath": "",
      "ImageIconPath": "",
      "Description": "",
      "IsSpecialSection": true,
      "Items": [
        {
          "UniqueId": "Typography",
          "Title": "Typography",
          "ApiNamespace": "",
          "Subtitle": "Typography",
          "ImagePath": "ms-appx:///Assets/ControlImages/AppBarSeparator.png",
          "ImageIconPath": "ms-appx:///Assets/ControlIcons/AppBarSeparatorIcon.png",
          "Description": "",
          "Content": "",
          "IsNew": false,
          "IsUpdated": true,
          "IncludedInBuild": true,
          "HideSourceCodeAndRelatedControls": true,
          "Docs": [
            {
              "Title": "Typography in Windows Apps",
              "Uri": "https://learn.microsoft.com/windows/apps/design/style/typography"
            },
            {
              "Title": "XAML theme resources",
              "Uri": "https://learn.microsoft.com/windows/apps/design/style/xaml-theme-resources#the-xaml-type-ramp"
            },
            {
              "Title": "Typography in Windows 11",
              "Uri": "https://learn.microsoft.com/windows/apps/design/signature-experiences/typography"
            }
          ],
          "RelatedControls": []
        },
        {
          "UniqueId": "AccessibilityScreenReader",
          "Title": "Screen reader support",
          "ApiNamespace": "",
          "Subtitle": "Screen reader support",
          "ImagePath": "ms-appx:///Assets/ControlImages/AppBarSeparator.png",
          "ImageIconPath": "ms-appx:///Assets/ControlIcons/AppBarSeparatorIcon.png",
          "Description": "",
          "Content": "",
          "IsNew": false,
          "IsUpdated": false,
          "IsPreview": true,
          "IncludedInBuild": true,
          "HideSourceCodeAndRelatedControls": true,
          "Docs": [
            {
              "Title": "Accessibility",
              "Uri": "https://learn.microsoft.com/windows/apps/design/accessibility/accessibility"
            },
            {
              "Title": "Accessible text requirements",
              "Uri": "https://learn.microsoft.com/windows/apps/design/accessibility/accessible-text-requirements"
            }
          ],
          "RelatedControls": []
        }
      ]
    }
  ]
}
```

See the [Demo](https://github.com/winUICommunity/DemoApp) app to see how to use it

## Documentation

See Here for Online [Documentation](https://ghost1372.github.io/winUICommunity/)

![LandingsPage](https://raw.githubusercontent.com/ghost1372/Resources/main/LandingsPage/0.png)
