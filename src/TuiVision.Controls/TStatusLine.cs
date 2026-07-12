// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Statuszeile am unteren Bildschirmrand.
/// Zeigt kontextabhängige Hinweise und Shortcuts an.
/// Wenn eine <see cref="TStatusDef"/>-Kette konfiguriert ist, wird die erste passende
/// Definition per First-Match-Wins ausgewählt; bei keiner Übereinstimmung bleibt die
/// Zeile neutral (<see cref="Items"/> == <c>null</c>).
/// Ohne <see cref="TStatusDef"/> greift der Kompatibilitäts-Fallback auf
/// <see cref="TView.GetStatusHints"/> der fokussierten View zurück.
///
/// The status line at the bottom of the screen.
/// Displays context-sensitive hints and shortcuts.
/// When a <see cref="TStatusDef"/> chain is configured, the first matching definition
/// is selected using first-match-wins ordering; when no definition matches, the line
/// shows a neutral state (<see cref="Items"/> == <c>null</c>).
/// Without a <see cref="TStatusDef"/> the compatibility fallback uses
/// <see cref="TView.GetStatusHints"/> from the focused view.
/// </summary>
public class TStatusLine : TView, IAccessibleShortcutProvider
{
    /// <summary>
    /// Die aktuell angezeigten Status-Einträge. / The currently displayed status items.
    /// </summary>
    public TStatusItem? Items { get; private set; }

    // Die optionale Statuszeilen-Definitionskette / The optional status-definition chain
    private readonly TStatusDef? _defs;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TStatusLine"/>-Klasse
    /// ohne explizite Statuszeilen-Definitionen (Kompatibilitätsmodus).
    ///
    /// Initializes a new instance of the <see cref="TStatusLine"/> class
    /// without explicit status-line definitions (compatibility mode).
    /// </summary>
    /// <param name="bounds">Die Grenzen der Statuszeile. / The bounds of the status line.</param>
    public TStatusLine(TRect bounds) : base(bounds)
    {
    }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TStatusLine"/>-Klasse
    /// mit einer expliziten <see cref="TStatusDef"/>-Kette.
    ///
    /// Initializes a new instance of the <see cref="TStatusLine"/> class
    /// with an explicit <see cref="TStatusDef"/> chain.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Statuszeile. / The bounds of the status line.</param>
    /// <param name="defs">
    /// Die erste Definition der Statusketten. Darf <c>null</c> sein (dann Kompatibilitätsmodus).
    /// The first definition in the status chain. May be <c>null</c> (then compatibility mode).
    /// </param>
    public TStatusLine(TRect bounds, TStatusDef? defs) : base(bounds)
    {
        _defs = defs;
    }

    /// <summary>
    /// Zeichnet die Statuszeile mit Hintergrundfarbe und dem Alt+X-Hinweis.
    ///
    /// Draws the status line with background colour and the Alt+X hint.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer == null || Size.X <= 0)
        {
            return;
        }

        // Statuszeilen-Hintergrund füllen / Fill status line background
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ColorScheme.StatusText, ColorScheme.StatusBackground));
        }

        // Quit-Hinweis: ^Q = Ctrl+Q (universell); Alt+X wenn Terminal Meta-Key aktiviert hat.
        // Quit hint: ^Q = Ctrl+Q (universal); Alt+X when terminal has Meta key enabled.
        const string hint = " ^Q Beenden / Quit  Alt+X ";
        buffer.WriteText(Origin.X, Origin.Y, hint.AsSpan(), ColorScheme.StatusText, ColorScheme.StatusBackground);
    }

    /// <summary>
    /// Verarbeitet Ereignisse. Aktualisiert die Hinweise bei Fokuswechseln.
    ///
    /// Processes events. Updates hints on focus changes.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);

        if (@event.What == TEventKind.Broadcast && @event.Message.Command == ShellCommandIds.cmFocusChanged)
        {
            // Der Übergang akzeptiert alte raw-TView-Payloads, aber neue Produzenten liefern den typisierten Snapshot.
            // The transition accepts legacy raw-view payloads, while new producers provide the typed snapshot.
            TView? focusedView = @event.Message.Info switch
            {
                TFocusAnnouncement announcement => announcement.Target,
                TView legacyView => legacyView,
                _ => null
            };
            UpdateHints(focusedView);
        }
    }

    /// <summary>
    /// Gibt explizit registrierte, aktive Statuszeilen-Shortcuts zurück.
    ///
    /// Returns explicitly registered, active status-line shortcuts.
    /// </summary>
    /// <returns>Eine schreibgeschützte Momentaufnahme. / A read-only snapshot.</returns>
    public IReadOnlyList<TAccessibleShortcut> GetAccessibleShortcuts()
    {
        List<TAccessibleShortcut> result = [];
        if (_defs != null)
        {
            for (TStatusDef? definition = _defs; definition != null; definition = definition.Next)
            {
                CollectShortcuts(definition.Items, result);
            }
        }
        else
        {
            CollectShortcuts(Items, result);
        }

        return result.AsReadOnly();
    }

    private static void CollectShortcuts(TStatusItem? item, List<TAccessibleShortcut> result)
    {
        for (TStatusItem? current = item; current != null; current = current.Next)
        {
            if (!current.Disabled && current.KeyCode != 0 && current.Command is > 0 and <= ushort.MaxValue)
            {
                result.Add(new TAccessibleShortcut(
                    current.KeyCode,
                    StripMarkers(current.Name),
                    (ushort)current.Command,
                    nameof(TStatusLine)));
            }
        }
    }

    private static string StripMarkers(string text) => text.Replace("~", string.Empty, StringComparison.Ordinal);

    /// <summary>
    /// Aktualisiert <see cref="Items"/> basierend auf der fokussierten View.
    /// Wenn eine <see cref="TStatusDef"/>-Kette konfiguriert ist, wird First-Match-Wins
    /// auf den <see cref="TView.HelpContext"/> der View angewendet.
    /// Andernfalls greift der Kompatibilitäts-Fallback auf <see cref="TView.GetStatusHints"/>.
    ///
    /// Updates <see cref="Items"/> based on the focused view.
    /// When a <see cref="TStatusDef"/> chain is configured, first-match-wins is applied
    /// to the view's <see cref="TView.HelpContext"/>.
    /// Otherwise the compatibility fallback uses <see cref="TView.GetStatusHints"/>.
    /// </summary>
    /// <param name="focusedView">Die aktuell fokussierte View. / The currently focused view.</param>
    private void UpdateHints(TView? focusedView)
    {
        if (_defs != null)
        {
            int ctx = focusedView?.HelpContext ?? 0;
            Items = ResolveDefinition(ctx);
        }
        else
        {
            // Kompatibilitäts-Fallback / Compatibility fallback
            Items = focusedView?.GetStatusHints();
        }

        DrawView();
    }

    /// <summary>
    /// Sucht in der Definitionskette nach der ersten passenden Definition (First-Match-Wins).
    /// Gibt die Einträgsliste der ersten Übereinstimmung zurück oder <c>null</c>, wenn
    /// keine Definition passt (Neutralzustand).
    ///
    /// Searches the definition chain for the first matching definition (first-match-wins).
    /// Returns the item list of the first match or <c>null</c> when no definition matches
    /// (neutral state).
    /// </summary>
    /// <param name="helpContext">Der aktuelle Hilfekontext. / The current help context.</param>
    /// <returns>Die Items der ersten passenden Definition oder <c>null</c>. / Items of the first match or <c>null</c>.</returns>
    private TStatusItem? ResolveDefinition(int helpContext)
    {
        TStatusDef? def = _defs;
        while (def != null)
        {
            if (def.Matches(helpContext))
            {
                return def.Items;
            }

            def = def.Next;
        }

        return null;
    }
}
