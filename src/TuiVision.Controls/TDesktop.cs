// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Benennt eine begrenzte Desktop-Stack-Operation.
///
/// Names a bounded desktop-stack operation.
/// </summary>
public enum TDesktopOperationKind
{
    /// <summary>Fenster einfügen. / Insert a window.</summary>
    Insert,
    /// <summary>Zum nächsten Fenster wechseln. / Select the next window.</summary>
    Next,
    /// <summary>Fenster kacheln. / Tile windows.</summary>
    Tile,
    /// <summary>Fenster kaskadieren. / Cascade windows.</summary>
    Cascade,
    /// <summary>Alle schließbaren Fenster schließen. / Close all closeable windows.</summary>
    CloseAll
}

/// <summary>
/// Beschreibt das unveränderliche Ergebnis einer Desktop-Operation mit expliziten
/// Teilnehmer-, Abschluss-, Veto- und Skip-Zählern.
///
/// Describes the immutable result of a desktop operation with explicit
/// participant, completion, veto, and skip counts.
/// </summary>
public sealed class TDesktopOperationResult
{
    internal TDesktopOperationResult(
        TDesktopOperationKind kind,
        int candidateCount,
        int participatingCount,
        TView? selectedView = null,
        int closedCount = 0,
        int vetoedCount = 0,
        int skippedCount = 0)
    {
        Kind = kind;
        CandidateCount = candidateCount;
        ParticipatingCount = participatingCount;
        SelectedView = selectedView;
        ClosedCount = closedCount;
        VetoedCount = vetoedCount;
        SkippedCount = skippedCount;
    }

    /// <summary>Die ausgeführte Operation. / The executed operation.</summary>
    public TDesktopOperationKind Kind { get; }

    /// <summary>Die Zahl geprüfter direkter Kinder. / Number of direct children considered.</summary>
    public int CandidateCount { get; }

    /// <summary>Die Zahl tatsächlicher Teilnehmer. / Number of actual participants.</summary>
    public int ParticipatingCount { get; }

    /// <summary>Die ausgewählte View oder <c>null</c>. / The selected view or <c>null</c>.</summary>
    public TView? SelectedView { get; }

    /// <summary>Die Zahl geschlossener Views. / Number of closed views.</summary>
    public int ClosedCount { get; }

    /// <summary>Die Zahl abgelehnter Close-Anfragen. / Number of vetoed close requests.</summary>
    public int VetoedCount { get; }

    /// <summary>Die Zahl nicht teilnehmender Views. / Number of skipped views.</summary>
    public int SkippedCount { get; }
}

/// <summary>
/// Der zentrale Arbeitsbereich der Anwendung.
/// Er verwaltet Kind-Fenster und sorgt für eine korrekte Fokus-Wiederherstellung.
///
/// The central workspace of the application.
/// It manages child windows and ensures correct focus recovery.
/// </summary>
public class TDesktop : TGroup
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TDesktop"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TDesktop"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    public TDesktop(TRect bounds) : base(bounds)
    {
    }

    /// <summary>
    /// Fügt eine View ein und fokussiert sie, wenn sie sichtbar, aktiv und auswählbar ist.
    ///
    /// Inserts a view and focuses it when it is visible, enabled, and selectable.
    /// </summary>
    /// <param name="view">Die einzufügende View. / The view to insert.</param>
    /// <returns>Das Ergebnis der Einfügung. / The insertion result.</returns>
    /// <exception cref="ArgumentNullException">Wird bei <c>null</c> ausgelöst. / Thrown for <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Wird für eine bereits zugeordnete View ausgelöst. / Thrown for a view that already has an owner.</exception>
    public TDesktopOperationResult InsertWindow(TView view)
    {
        Insert(view);
        TView? selected = null;
        if (IsEligibleWindow(view))
        {
            BringChildToFront(view);
            if (TrySetFocus(view) != TFocusTransitionResult.Rejected)
            {
                selected = view;
            }
        }

        return new TDesktopOperationResult(
            TDesktopOperationKind.Insert,
            1,
            selected is null ? 0 : 1,
            selected);
    }

    /// <summary>
    /// Liefert das oberste sichtbare und auswählbare Desktop-Kind.
    ///
    /// Returns the topmost visible and selectable desktop child.
    /// </summary>
    /// <returns>Das oberste Fenster oder <c>null</c>. / The top window or <c>null</c>.</returns>
    public TView? GetTopWindow() =>
        GetChildrenSnapshot().Reverse().FirstOrDefault(IsEligibleWindow);

    /// <summary>
    /// Wählt zyklisch das nächste berechtigte Fenster und bringt es nach vorn.
    ///
    /// Selects the next eligible window cyclically and brings it to front.
    /// </summary>
    /// <param name="forward">Vorwärts oder rückwärts durch den Stack. / Whether to move forward or backward through the stack.</param>
    /// <returns>Das Ergebnis der Auswahl. / The selection result.</returns>
    public TDesktopOperationResult SelectNextWindow(bool forward = true)
    {
        IReadOnlyList<TView> snapshot = GetChildrenSnapshot();
        List<TView> participants = snapshot.Where(IsEligibleWindow).ToList();
        if (participants.Count == 0)
        {
            return new TDesktopOperationResult(TDesktopOperationKind.Next, snapshot.Count, 0);
        }

        int currentIndex = participants.FindIndex(view => ReferenceEquals(view, Current));
        int selectedIndex = currentIndex < 0
            ? (forward ? 0 : participants.Count - 1)
            : forward
                ? (currentIndex + 1) % participants.Count
                : (currentIndex - 1 + participants.Count) % participants.Count;
        TView selected = participants[selectedIndex];
        BringChildToFront(selected);
        TrySetFocus(selected);
        return new TDesktopOperationResult(
            TDesktopOperationKind.Next,
            snapshot.Count,
            participants.Count,
            selected);
    }

    /// <summary>
    /// Kachelt alle sichtbaren, aktiven <see cref="TViewOptions.Tileable"/>-Kinder
    /// innerhalb der Desktop-Grenzen.
    ///
    /// Tiles all visible, enabled <see cref="TViewOptions.Tileable"/> children
    /// within desktop bounds.
    /// </summary>
    /// <returns>Das Geometrieergebnis. / The geometry result.</returns>
    public TDesktopOperationResult TileWindows()
    {
        IReadOnlyList<TView> snapshot = GetChildrenSnapshot();
        List<TView> participants = snapshot.Where(IsGeometryParticipant).ToList();
        if (participants.Count == 0)
        {
            return new TDesktopOperationResult(TDesktopOperationKind.Tile, snapshot.Count, 0, Current);
        }

        TRect extent = GetExtent();
        int columns = (int)Math.Ceiling(Math.Sqrt(participants.Count));
        int rows = (int)Math.Ceiling((double)participants.Count / columns);
        for (int index = 0; index < participants.Count; index++)
        {
            int column = index % columns;
            int row = index / columns;
            int left = extent.A.X + (column * extent.Width / columns);
            int right = extent.A.X + ((column + 1) * extent.Width / columns);
            int top = extent.A.Y + (row * extent.Height / rows);
            int bottom = extent.A.Y + ((row + 1) * extent.Height / rows);
            participants[index].Locate(new TRect(left, top, Math.Max(left + 1, right), Math.Max(top + 1, bottom)));
        }

        return new TDesktopOperationResult(TDesktopOperationKind.Tile, snapshot.Count, participants.Count, Current);
    }

    /// <summary>
    /// Kaskadiert alle sichtbaren, aktiven <see cref="TViewOptions.Tileable"/>-Kinder
    /// mit begrenztem Zeichenraster-Versatz.
    ///
    /// Cascades all visible, enabled <see cref="TViewOptions.Tileable"/> children
    /// with a bounded character-cell offset.
    /// </summary>
    /// <returns>Das Geometrieergebnis. / The geometry result.</returns>
    public TDesktopOperationResult CascadeWindows()
    {
        IReadOnlyList<TView> snapshot = GetChildrenSnapshot();
        List<TView> participants = snapshot.Where(IsGeometryParticipant).ToList();
        if (participants.Count == 0)
        {
            return new TDesktopOperationResult(TDesktopOperationKind.Cascade, snapshot.Count, 0, Current);
        }

        TRect extent = GetExtent();
        int divisor = Math.Max(1, participants.Count - 1);
        int stepX = Math.Min(2, Math.Max(0, (extent.Width - 1) / divisor));
        int stepY = Math.Min(1, Math.Max(0, (extent.Height - 1) / divisor));
        int width = Math.Max(1, extent.Width - (stepX * (participants.Count - 1)));
        int height = Math.Max(1, extent.Height - (stepY * (participants.Count - 1)));
        for (int index = 0; index < participants.Count; index++)
        {
            int left = extent.A.X + (index * stepX);
            int top = extent.A.Y + (index * stepY);
            participants[index].Locate(new TRect(left, top, left + width, top + height));
        }

        return new TDesktopOperationResult(TDesktopOperationKind.Cascade, snapshot.Count, participants.Count, Current);
    }

    /// <summary>
    /// Schließt sichtbare gerahmte Hosts in umgekehrter Z-Reihenfolge und meldet
    /// Veto- sowie Nicht-Teilnehmer explizit.
    ///
    /// Closes visible framed hosts in reverse Z order and explicitly reports vetoed
    /// and non-participating children.
    /// </summary>
    /// <returns>Das Close-All-Ergebnis. / The close-all result.</returns>
    public TDesktopOperationResult CloseAllWindows()
    {
        List<TView> candidates = GetChildrenSnapshot()
            .Where(view => view.GetState(TViewState.Visible))
            .Reverse()
            .ToList();
        int participating = 0;
        int closed = 0;
        int vetoed = 0;
        int skipped = 0;
        foreach (TView candidate in candidates)
        {
            if (candidate is not ICloseableView closeable)
            {
                skipped++;
                continue;
            }

            participating++;
            TCloseDecision decision = closeable.RequestClose(TCloseTrigger.CloseAll).Decision;
            if (decision == TCloseDecision.Closed)
            {
                closed++;
            }
            else if (decision == TCloseDecision.Vetoed)
            {
                vetoed++;
            }
            else
            {
                skipped++;
            }
        }

        return new TDesktopOperationResult(
            TDesktopOperationKind.CloseAll,
            candidates.Count,
            participating,
            Current,
            closed,
            vetoed,
            skipped);
    }

    private static bool IsEligibleWindow(TView view) =>
        view.GetState(TViewState.Visible)
        && !view.GetState(TViewState.Disabled)
        && view.Options.HasFlag(TViewOptions.Selectable);

    private static bool IsGeometryParticipant(TView view) =>
        IsEligibleWindow(view) && view.Options.HasFlag(TViewOptions.Tileable);

    /// <summary>
    /// Füllt den Desktop-Hintergrund mit dem Schraffurzeichen '░' und zeichnet danach
    /// alle eingebetteten Kind-Fenster.
    ///
    /// Fills the desktop background with the hatching character '░' and then draws
    /// all embedded child windows.
    /// </summary>
    public override void Draw()
    {
        // Hintergrund mit '░' auf Blau füllen / Fill background with '░' on blue
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer != null)
        {
            buffer.Clear(new TConsoleCell('░', ConsoleColor.DarkCyan, ConsoleColor.Blue));
        }

        base.Draw();
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich die aktuell fokussierte Ansicht ändert.
    /// Sorgt für einen Fallback zum Desktop selbst, wenn kein Kind mehr fokussierbar ist.
    ///
    /// Called when the currently focused view changes.
    /// Ensures fallback to the desktop itself if no child is focusable.
    /// </summary>
    protected override void CurrentChanged()
    {
        base.CurrentChanged();

        if (Current == null)
        {
            // Fallback: nächstes auswählbares Kind auswählen, sonst bleibt Desktop ohne Fokus.
            // Fallback: select the next eligible child; desktop remains focus target if none exists.
            SelectNext(true);
        }
    }
}
