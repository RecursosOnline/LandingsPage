﻿using System;

namespace WinUICommunity;
/// <summary>
/// A delegate for <see cref="ImageEx"/> opened.
/// </summary>
/// <param name="sender">The sender.</param>
/// <param name="e">The event arguments.</param>
internal delegate void ImageExOpenedEventHandler(object sender, ImageExOpenedEventArgs e);

/// <summary>
/// Provides data for the <see cref="ImageEx"/> ImageOpened event.
/// </summary>
internal class ImageExOpenedEventArgs : EventArgs
{
}
