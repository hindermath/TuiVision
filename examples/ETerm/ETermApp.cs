// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.ETerm;

/// <summary>Unveränderlicher historischer ETerm-Eintrag. / Immutable historical ETerm entry.</summary>
public sealed record ETermManifestEntry(string Key, string Value, string Category, string SourceId);

/// <summary>
/// Zeigt ein begrenztes ETerm-Ressourcenmanifest ohne Legacy-Parser oder Hostmutation.
/// Shows a bounded ETerm resource manifest without a legacy parser or host mutation.
/// </summary>
public sealed class ETermApp : Wave4Application
{
    /// <summary>Wählt den nächsten Eintrag. / Selects the next entry.</summary>
    public const ushort CmNextEntry = 22301;

    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 22302;

    private readonly bool _requestUnsupported;
    private TWindow? _manifestWindow;

    /// <summary>Initialisiert die ETerm-Ansicht. / Initializes the ETerm view.</summary>
    public ETermApp(TRect bounds, bool headless = false, bool requestUnsupported = false) : base(bounds, headless)
    {
        Entries =
        [
            new ETermManifestEntry("Font3", "8x16", "Font", "menus.cfg"),
            new ETermManifestEntry("Foreground", "#aaaaaa", "Theme", "theme.cfg"),
            new ETermManifestEntry("Version", "ESC[8n", "Presentation", "menus.cfg")
        ];
        EnsureUniqueEntries(Entries);
        _requestUnsupported = requestUnsupported;
        RenderManifest();
    }

    /// <summary>Kontrollierte Manifest-Einträge. / Controlled manifest entries.</summary>
    public IReadOnlyList<ETermManifestEntry> Entries { get; }

    /// <summary>Ausgewählter Index. / Selected index.</summary>
    public int SelectedIndex { get; private set; }

    /// <summary>Ausgewählter Eintrag. / Selected entry.</summary>
    public ETermManifestEntry SelectedEntry => Entries[SelectedIndex];

    /// <summary>Ob der sichtbare Fallback aktiv ist. / Whether the visible fallback is active.</summary>
    public bool FallbackVisible { get; private set; }

    /// <summary>Der historische Parser wird nie ausgeführt. / The historical parser is never executed.</summary>
    public bool NativeParserUsed => false;

    /// <summary>Host-Theme-Mutation wird nie ausgeführt. / Host theme mutation is never executed.</summary>
    public bool HostThemeMutationUsed => false;

    /// <summary>Explizite Ressourcen- und Ausführungsgrenze. / Explicit resource and execution boundary.</summary>
    public string BoundaryText => "ETerm uses an immutable manifest; no native parser, script action, spawn, save, exit, or host theme mutation.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~E~Term",
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
                "ETerm",
                "ETerm description: Historische Werte werden als unveränderliches Manifest gezeigt; kein Parser oder Host-Theme wird ausgeführt. / " +
                "Historical values are shown as an immutable manifest; no parser or host theme is executed.");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private static void EnsureUniqueEntries(IEnumerable<ETermManifestEntry> entries)
    {
        // Exakte Schlüssel verhindern, dass ein Alias oder Duplikat eine nicht belegte Capability vortäuscht.
        // Exact keys prevent an alias or duplicate from implying an unproven capability.
        if (entries.GroupBy(entry => entry.Key, StringComparer.Ordinal).Any(group => group.Count() != 1))
        {
            throw new InvalidOperationException("ETerm manifest keys must be unique and case-sensitive.");
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
        TWindow window = new("ETerm", region.A.X, region.A.Y, region.Width, region.Height);
        string text;
        if (_requestUnsupported)
        {
            FallbackVisible = true;
            text = "ETerm Resource Fallback\nUnsupported: native config entry is outside the immutable manifest\nNo parser or host theme action";
        }
        else
        {
            FallbackVisible = false;
            text = "ETerm immutable manifest\n" + string.Join(
                "\n",
                Entries.Select((entry, index) => $"{(index == SelectedIndex ? '>' : ' ')} {entry.Key} = {entry.Value} [{entry.Category}; {entry.SourceId}]"));
        }

        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), text));
        desktop.Insert(window);
        desktop.SetFocus(window);
        _manifestWindow = window;
        SetWave4Visible(window, "TWindow");
        SetWave4Status("ETerm", _requestUnsupported ? "Unsupported immutable-manifest boundary" : $"selected={SelectedEntry.Key}");
    }
}
