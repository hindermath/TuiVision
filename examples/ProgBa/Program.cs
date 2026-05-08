// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.ProgBa;

int width;
int height;
try { width = Console.WindowWidth; height = Console.WindowHeight; }
catch { width = 80; height = 25; }
if (width <= 0) width = 80;
if (height <= 0) height = 25;

ProgBaApp app = new(new TRect(0, 0, width, height));
app.Run();
