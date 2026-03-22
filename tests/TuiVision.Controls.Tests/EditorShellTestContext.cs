// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Testhilfen fuer Editor-, Datei- und Help-Shell-Szenarien.
///
/// Test helpers for editor, file, and help shell scenarios.
/// </summary>
public static class EditorShellTestContext
{
    /// <summary>
    /// Erstellt eine Standardanwendung fuer Shell-Integrationstests.
    ///
    /// Creates a standard application for shell integration tests.
    /// </summary>
    /// <returns>Eine testbare Anwendung. / A testable application.</returns>
    public static TestApplication CreateApplication()
    {
        return new TestApplication(ShellTestSupport.CreateStandardBounds());
    }

    /// <summary>
    /// Erstellt ein temporaeres Arbeitsverzeichnis und loescht es beim Freigeben.
    ///
    /// Creates a temporary working directory and deletes it on dispose.
    /// </summary>
    /// <returns>Ein temporaeres Verzeichnisobjekt. / A temporary directory handle.</returns>
    public static TemporaryDirectory CreateTemporaryDirectory()
    {
        string path = Path.Combine(Path.GetTempPath(), $"tuivision-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(path);
        return new TemporaryDirectory(path);
    }

    /// <summary>
    /// Test-Anwendungsableitung mit kontrollierbarer Event-Queue.
    ///
    /// Test application derivative with a controllable event queue.
    /// </summary>
    public sealed class TestApplication : TApplication
    {
        private readonly Queue<TEvent> _events = new();

        /// <summary>
        /// Initialisiert eine neue Testanwendung.
        ///
        /// Initializes a new test application.
        /// </summary>
        /// <param name="bounds">Die Bounds der Anwendung. / The application bounds.</param>
        public TestApplication(TRect bounds) : base(bounds)
        {
        }

        /// <summary>
        /// Stellt Ereignisse fuer die naechsten Run-Schritte bereit.
        ///
        /// Queues events for the next run steps.
        /// </summary>
        /// <param name="events">Die einzureihenden Ereignisse. / The events to enqueue.</param>
        public void Enqueue(params TEvent[] events)
        {
            foreach (TEvent @event in events)
            {
                _events.Enqueue(@event);
            }
        }

        /// <summary>
        /// Fuegt eine View in den Desktop ein und fokussiert sie.
        ///
        /// Inserts a view into the desktop and focuses it.
        /// </summary>
        /// <param name="view">Die einzufuegende View. / The view to insert.</param>
        public void ShowOnDesktop(TView view)
        {
            Desktop!.Insert(view);
            Desktop.SetFocus(view);
        }

        /// <summary>
        /// Liefert das naechste Ereignis aus der Queue oder beendet die Anwendung.
        ///
        /// Returns the next queued event or quits the application.
        /// </summary>
        /// <param name="event">Das gelieferte Ereignis. / The event that was delivered.</param>
        public override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0
                ? _events.Dequeue()
                : TEvent.CreateCommand(ShellCommandIds.cmQuit);
        }
    }

    /// <summary>
    /// Disposable-Wrapper fuer temporaere Testverzeichnisse.
    ///
    /// Disposable wrapper for temporary test directories.
    /// </summary>
    public sealed class TemporaryDirectory : IDisposable
    {
        /// <summary>
        /// Initialisiert das temporaere Verzeichnis.
        ///
        /// Initializes the temporary directory.
        /// </summary>
        /// <param name="path">Der absolute Verzeichnispfad. / The absolute directory path.</param>
        public TemporaryDirectory(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Der absolute Verzeichnispfad.
        ///
        /// The absolute directory path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Loescht das Verzeichnis rekursiv.
        ///
        /// Deletes the directory recursively.
        /// </summary>
        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, recursive: true);
            }
        }
    }
}
