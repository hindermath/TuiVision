// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.MsgCls;

// Startet die MsgCls-Anwendung mit einer 80×25-Konsolenoberfläche.
// Starts the MsgCls application with an 80×25 console surface.
TRect bounds = new(0, 0, 80, 25);
MsgClsApp app = new(bounds);
app.Run();
