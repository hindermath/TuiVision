// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.XTerm;

/// <summary>Unveränderlicher historischer XTerm-Eintrag. / Immutable historical XTerm entry.</summary>
public sealed record XTermManifestEntry(string Key, string Value, string Category, string SourceId);

/// <summary>
/// Zeigt ein begrenztes XTerm-Ressourcenmanifest ohne X-Resource-Ausführung.
/// Shows a bounded XTerm resource manifest without X resource execution.
/// </summary>
public sealed class XTermApp : Wave4Application
{
    /// <summary>Wählt den nächsten Eintrag. / Selects the next entry.</summary>
    public const ushort CmNextEntry = 22401;

    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 22402;

    private readonly bool _requestUnsupported;
    private TWindow? _manifestWindow;

    /// <summary>Initialisiert die XTerm-Ansicht. / Initializes the XTerm view.</summary>
    public XTermApp(TRect bounds, bool headless = false, bool requestUnsupported = false) : base(bounds, headless)
    {
        Entries =
        [
            new XTermManifestEntry("Insert", "ESC[2~", "Sequence", "Xterm.res"),
            new XTermManifestEntry("Color1", "#a80000", "Resource", "Xterm.res"),
            new XTermManifestEntry("metaSendsEscape", "true", "Capability", "Xterm.res")
        ];
        EnsureUniqueEntries(Entries);
        _requestUnsupported = requestUnsupported;
        RenderManifest();
    }

    /// <summary>Kontrollierte Manifest-Einträge. / Controlled manifest entries.</summary>
    public IReadOnlyList<XTermManifestEntry> Entries { get; }

    /// <summary>Ausgewählter Index. / Selected index.</summary>
    public int SelectedIndex { get; private set; }

    /// <summary>Ausgewählter Eintrag. / Selected entry.</summary>
    public XTermManifestEntry SelectedEntry => Entries[SelectedIndex];

    /// <summary>Ob der sichtbare Fallback aktiv ist. / Whether the visible fallback is active.</summary>
    public bool FallbackVisible { get; private set; }

    /// <summary>Die X-Resource-Datenbank wird nie verwendet. / The X resource database is never used.</summary>
    public bool XResourceDatabaseUsed => false;

    /// <summary>Externe Kommandos werden nie ausgeführt. / External commands are never executed.</summary>
    public bool ExternalCommandUsed => false;

    /// <summary>Explizite Wiederverwendungsgrenze. / Explicit reuse boundary.</summary>
    public string BoundaryText => "Compatibility input and the Feature-021 session parser remain the only runtime parsing boundaries.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~X~Term",
            0,
            Wave4Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
            new TMenuItem("~N~ächster Eintrag / ~N~ext entry", CmNextEntry))
    };

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmNextEntry)
        {
            SelectedIndex = (SelectedIndex + 1) % Entries.Count;
            RenderManifest();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave4Description(
                "XTerm",
                "XTerm description: Ressourcen und Sequenzen sind ein unveränderliches Manifest; keine X-Datenbank oder Hostmutation wird ausgeführt. / " +
                "Resources and sequences are an immutable manifest; no X database or host mutation is executed.");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private static void EnsureUniqueEntries(IEnumerable<XTermManifestEntry> entries)
    {
        // Nur exakte belegte Schlüssel werden veröffentlicht; native X-Ressourcen bleiben außerhalb des Subsets.
        // Only exact evidenced keys are published; native X resources remain outside the subset.
        if (entries.GroupBy(entry => entry.Key, StringComparer.Ordinal).Any(group => group.Count() != 1))
        {
            throw new InvalidOperationException("XTerm manifest keys must be unique and case-sensitive.");
        }
    }

    private void RenderManifest()
    {
        TGroup desktop = Desktop!;
        if (_manifestWindow is TWindow previous && previous.Owner == desktop)
        {
            desktop.Remove(previous);
        }

        TRect region = Wave4Runtime.MainRegion(desktop, 66, 15);
        TWindow window = new("XTerm", region.A.X, region.A.Y, region.Width, region.Height);
        string text;
        if (_requestUnsupported)
        {
            FallbackVisible = true;
            text = "XTerm Resource Fallback\nUnsupported: native X resource is outside the immutable manifest\nNo X database, terminfo, or external command";
        }
        else
        {
            FallbackVisible = false;
            text = "XTerm immutable manifest\n" + string.Join(
                "\n",
                Entries.Select((entry, index) => $"{(index == SelectedIndex ? '>' : ' ')} {entry.Key} = {entry.Value} [{entry.Category}; {entry.SourceId}]"));
        }

        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), text));
        desktop.Insert(window);
        desktop.SetFocus(window);
        _manifestWindow = window;
        SetWave4Visible(window, "TWindow");
        SetWave4Status("XTerm", _requestUnsupported ? "Unsupported native-resource boundary" : $"selected={SelectedEntry.Key}");
    }
}
