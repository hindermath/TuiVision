// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Dateimetadaten als managed Gegenstück zu <c>TFileInfo</c> aus Turbo Vision
/// (<c>tfileinf.cc</c>).
///
/// File metadata as a managed counterpart to <c>TFileInfo</c> from Turbo Vision
/// (<c>tfileinf.cc</c>).
/// </summary>
/// <param name="FileName">Der Dateiname. / The file name.</param>
/// <param name="Size">Die Dateigröße in Bytes. / The file size in bytes.</param>
/// <param name="LastModified">Der Änderungszeitpunkt. / The last modification timestamp.</param>
public readonly record struct TFileInfo(string FileName, long Size, DateTime LastModified);
