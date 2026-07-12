using TuiVision.Core;
using TuiVision.Examples.A11yFramework;

int width;
int height;
try { width = Console.WindowWidth; height = Console.WindowHeight; }
catch { width = 80; height = 25; }
if (width <= 0) width = 80;
if (height <= 0) height = 25;

new A11yFrameworkApp(new TRect(0, 0, width, height)).Run();
