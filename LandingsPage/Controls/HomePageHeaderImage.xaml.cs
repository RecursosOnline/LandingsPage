﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Composition;
using CommunityToolkit.WinUI.UI.Animations;
using System;
using CommunityToolkit.WinUI.UI;
using WinUICommunity.LandingsPage.Internal;

namespace WinUICommunity.LandingsPage.Controls;
// ATTRIBUTION: @RykenApps
internal sealed partial class HomePageHeaderImage : UserControl
{
    public string HeaderImage
    {
        get { return (string) GetValue(HeaderImageProperty); }
        set { SetValue(HeaderImageProperty, value); }
    }
    public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage", typeof(string), typeof(HomePageHeaderImage), new PropertyMetadata(default(string)));

    private Compositor _compositor;
    private CompositionLinearGradientBrush _imageGridBottomGradientBrush;
    private CompositionEffectBrush _imageGridEffectBrush;
    private ExpressionAnimation _imageGridSizeAnimation;
    private ExpressionAnimation _bottomGradientStartPointAnimation;
    private SpriteVisual _imageGridSpriteVisual;
    private CompositionSurfaceBrush _imageGridSurfaceBrush;
    private Visual _imageGridVisual;
    private CompositionVisualSurface _imageGridVisualSurface;
    private const string GradientSizeKey = "GradientSize";
    public HomePageHeaderImage()
    {
        this.InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _imageGridVisual = ElementCompositionPreview.GetElementVisual(ImageGrid);
        _compositor = _imageGridVisual.Compositor;

        _imageGridSizeAnimation = _compositor.CreateExpressionAnimation("Visual.Size");
        _imageGridSizeAnimation.SetReferenceParameter("Visual", _imageGridVisual);

        _imageGridVisualSurface = _compositor.CreateVisualSurface();
        _imageGridVisualSurface.SourceVisual = _imageGridVisual;
        _imageGridVisualSurface.StartAnimation(nameof(CompositionVisualSurface.SourceSize), _imageGridSizeAnimation);

        _imageGridSurfaceBrush = _compositor.CreateSurfaceBrush(_imageGridVisualSurface);
        _imageGridSurfaceBrush.Stretch = CompositionStretch.UniformToFill;

        _bottomGradientStartPointAnimation = CreateExpressionAnimation(nameof(CompositionLinearGradientBrush.StartPoint), $"Vector2(0.5, Visual.Size.Y - this.{GradientSizeKey})");
        SetBottomGradientStartPoint();

        _imageGridBottomGradientBrush = _compositor.CreateLinearGradientBrush();
        _imageGridBottomGradientBrush.MappingMode = CompositionMappingMode.Absolute;
        _imageGridBottomGradientBrush.StartAnimation(_bottomGradientStartPointAnimation);
        _imageGridBottomGradientBrush.StartAnimation(CreateExpressionAnimation(nameof(CompositionLinearGradientBrush.EndPoint), "Vector2(0.5, Visual.Size.Y)"));
        _imageGridBottomGradientBrush.CreateColorStopsWithEasingFunction(EasingType.Sine, EasingMode.EaseInOut, 0f, 1f);

        var alphaMask = new AlphaMaskEffect
        {
            Source = new CompositionEffectSourceParameter("ImageGrid"),
            AlphaMask = new CompositionEffectSourceParameter("Gradient")
        };

        var effectFactory = _compositor.CreateEffectFactory(alphaMask);
        _imageGridEffectBrush = effectFactory.CreateBrush();
        _imageGridEffectBrush.SetSourceParameter("ImageGrid", _imageGridSurfaceBrush);
        _imageGridEffectBrush.SetSourceParameter("Gradient", _imageGridBottomGradientBrush);

        _imageGridSpriteVisual = _compositor.CreateSpriteVisual();
        _imageGridSpriteVisual.RelativeSizeAdjustment = Vector2.One;
        _imageGridSpriteVisual.Brush = _imageGridEffectBrush;

        ElementCompositionPreview.GetElementVisual(ImageGridSurfaceRec).ParentForTransform = _imageGridVisual;

        ElementCompositionPreview.SetElementChildVisual(ImageGridSurfaceRec, _imageGridSpriteVisual);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ElementCompositionPreview.SetElementChildVisual(ImageGridSurfaceRec, null);
        _imageGridSpriteVisual?.Dispose();
        _imageGridEffectBrush?.Dispose();
        _imageGridSurfaceBrush?.Dispose();
        _imageGridVisualSurface?.Dispose();
        _imageGridSizeAnimation?.Dispose();
        _bottomGradientStartPointAnimation?.Dispose();
        _bottomGradientStartPointAnimation = null;
    }
    private void OnLoading(FrameworkElement sender, object args)
    {
        if (HeroImage.Source == null)
        {
            HeroImage.GetVisual().Opacity = 0;
        }
        else
        {
            AnimateImage();
        }
    }
    private void SetBottomGradientStartPoint()
    {
        _bottomGradientStartPointAnimation?.Properties.InsertScalar(GradientSizeKey, 180);
    }

    private void OnImageOpened(object sender, RoutedEventArgs e)
    {
        AnimateImage();
    }

    private void AnimateImage()
    {
        AnimationBuilder.Create()
            .Opacity(1, 0, duration: TimeSpan.FromMilliseconds(300), easingMode: EasingMode.EaseOut)
            .Scale(1, 1.1f, duration: TimeSpan.FromMilliseconds(400), easingMode: EasingMode.EaseOut)
            .Start(HeroImage);

        AnimationBuilder.Create()
            .Opacity(0.5, 0, duration: TimeSpan.FromMilliseconds(300), easingMode: EasingMode.EaseOut)
            .Scale(1, 1.1f, duration: TimeSpan.FromMilliseconds(400), easingMode: EasingMode.EaseOut)
            .Start(HeroOverlayImage);
    }


    private ExpressionAnimation CreateExpressionAnimation(string target, string expression)
    {
        var ani = _compositor.CreateExpressionAnimation(expression);
        ani.SetReferenceParameter("Visual", _imageGridVisual);
        ani.Target = target;
        return ani;
    }
}
