// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Desklogo;

// Startet die Desklogo-Anwendung mit einer 80×25-Konsolenoberfläche.
// Starts the Desklogo application with an 80×25 console surface.
TRect bounds = new(0, 0, 80, 25);
DesklogoApp app = new(bounds);
app.Run();
