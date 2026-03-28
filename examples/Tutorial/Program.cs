// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Tutorial;

// Token aus Kommandozeilenargumenten lesen; Standard: „tvguid01"
// Read token from command-line arguments; default: "tvguid01"
string token = args.Length > 0 ? args[0] : "tvguid01";

TRect bounds = new(0, 0, 80, 25);
TutorialApp launcher = new(token, bounds);
launcher.Run();
