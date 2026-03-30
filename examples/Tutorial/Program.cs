// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Tutorial;

// Token aus Kommandozeilenargumenten lesen; Standard: „tvguid01"
// Read token from command-line arguments; default: "tvguid01"
string token = args.Length > 0 ? args[0] : "tvguid01";

// Tatsächliche Konsolengröße ermitteln; Fallback auf 80×25 falls nicht verfügbar.
// Detect actual console size; fall back to 80×25 if unavailable.
int width, height;
try { width = Console.WindowWidth; height = Console.WindowHeight; }
catch { width = 80; height = 25; }
if (width <= 0) width = 80;
if (height <= 0) height = 25;

TRect bounds = new(0, 0, width, height);
TutorialApp launcher = new(token, bounds);
launcher.Run();
