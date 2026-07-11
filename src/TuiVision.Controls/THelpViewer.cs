// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls;

/// <summary>
/// Scrollbarer Laufzeit-Viewer fuer kontextbezogene Hilfethemen.
///
/// Scrollable runtime viewer for context-sensitive help topics.
/// </summary>
public sealed class THelpViewer : TScroller
{
    private readonly Stack<int> _navigationHistory = new();

    /// <summary>
    /// Initialisiert einen neuen Help-Viewer.
    ///
    /// Initializes a new help viewer.
    /// </summary>
    /// <param name="bounds">Die Bounds des Viewers. / The viewer bounds.</param>
    /// <param name="helpFile">Die zugrunde liegende Hilfedatei. / The underlying help file.</param>
    public THelpViewer(TRect bounds, THelpFile helpFile) : base(bounds)
    {
        HelpFile = helpFile ?? throw new ArgumentNullException(nameof(helpFile));
        Options |= TViewOptions.Selectable;
    }

    /// <summary>
    /// Die zugrunde liegende Hilfedatei.
    ///
    /// The underlying help file.
    /// </summary>
    public THelpFile HelpFile { get; }

    /// <summary>
    /// Das aktuell angezeigte Thema.
    ///
    /// The currently displayed topic.
    /// </summary>
    public THelpTopic? CurrentTopic { get; private set; }

    /// <summary>
    /// Der aktuell markierte Querverweis.
    ///
    /// The currently selected cross reference.
    /// </summary>
    public int SelectedReferenceIndex { get; private set; }

    /// <summary>
    /// Oeffnet ein Thema anhand seines Kontexts.
    ///
    /// Opens a topic by its context.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    public void OpenContext(int context)
    {
        CurrentTopic = HelpFile.GetTopicOrFallback(context);
        SelectedReferenceIndex = 0;
        SetLimit(new TPoint(0, Math.Max(1, CurrentTopic.Paragraphs.Count + CurrentTopic.CrossReferences.Count + 1)));
        ScrollTo(new TPoint(0, 0));
        DrawView();
    }

    /// <summary>
    /// Waehlt einen Querverweis aus.
    ///
    /// Selects a cross reference.
    /// </summary>
    /// <param name="index">Der Zielindex. / The target index.</param>
    public void SelectReference(int index)
    {
        if (CurrentTopic is null || CurrentTopic.CrossReferences.Count == 0)
        {
            SelectedReferenceIndex = 0;
            return;
        }

        SelectedReferenceIndex = Math.Max(0, Math.Min(CurrentTopic.CrossReferences.Count - 1, index));
        DrawView();
    }

    /// <summary>
    /// Aktiviert den aktuell markierten Querverweis.
    ///
    /// Activates the currently selected cross reference.
    /// </summary>
    /// <returns><c>true</c>, wenn navigiert wurde. / <c>true</c> if navigation occurred.</returns>
    public bool ActivateSelectedReference()
    {
        if (CurrentTopic is null || CurrentTopic.CrossReferences.Count == 0)
        {
            return false;
        }

        // Der Ausgangskontext wird vor der Fallback-Auflösung gesichert, damit Zurück immer zum sichtbaren Thema führt.
        // The source context is saved before fallback resolution so Back always returns to the visible topic.
        _navigationHistory.Push(CurrentTopic.Context);
        OpenContext(CurrentTopic.CrossReferences[SelectedReferenceIndex].TargetContext);
        return true;
    }

    /// <summary>
    /// Navigiert zum vorherigen Thema zurueck.
    ///
    /// Navigates back to the previous topic.
    /// </summary>
    /// <returns><c>true</c>, wenn ein Ruecksprung erfolgte. / <c>true</c> if a back navigation occurred.</returns>
    public bool GoBack()
    {
        if (_navigationHistory.Count == 0)
        {
            return false;
        }

        OpenContext(_navigationHistory.Pop());
        return true;
    }

    /// <summary>
    /// Zeichnet Titel, Abschnitte und Querverweise.
    ///
    /// Draws title, paragraphs, and cross references.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0 || CurrentTopic is null)
        {
            return;
        }

        List<string> lines = [CurrentTopic.Title];
        lines.AddRange(CurrentTopic.Paragraphs);
        for (int index = 0; index < CurrentTopic.CrossReferences.Count; index++)
        {
            THelpCrossReference reference = CurrentTopic.CrossReferences[index];
            string prefix = index == SelectedReferenceIndex ? ">" : " ";
            lines.Add($"{prefix}{reference.Label}");
        }

        for (int row = 0; row < Size.Y; row++)
        {
            int sourceIndex = Delta.Y + row;
            string line = sourceIndex < lines.Count ? lines[sourceIndex] : string.Empty;
            buffer.WriteText(Origin.X, Origin.Y + row, line.PadRight(Size.X).AsSpan(0, Size.X));
        }
    }
}
