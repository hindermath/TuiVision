// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;
using TuiVision.Serialization;

namespace TuiVision.Examples.TvHc;

/// <summary>Zeigt den begrenzten Help-Compiler als sichtbare Anwendung. / Shows the bounded help compiler as a visible application.</summary>
public sealed class TvHcApp : Wave3Application
{
    /// <summary>Gültige Quelle kompilieren. / Compile valid source.</summary>
    public const ushort CmCompileValid = 19401;
    /// <summary>Ungültige Quelle kompilieren. / Compile invalid source.</summary>
    public const ushort CmCompileInvalid = 19402;
    /// <summary>Gültiges Ergebnis kontrolliert speichern. / Persist a valid result under control.</summary>
    public const ushort CmPersistValid = 19403;
    /// <summary>Beschreibung anzeigen. / Show description.</summary>
    public const ushort CmDescription = 19404;

    private const string ValidSource = ".topic Welcome=1\nWelcome\nOpen {Next:Next}.\n.topic Next=2\nNext topic";
    private const string InvalidSource = ".topic Broken=1\nSee {Missing:Missing}.";
    private readonly THelpSourceCompiler _compiler = new();
    private readonly string? _allowedOutputDirectory;
    private readonly List<string> _visibleHistory = [];

    /// <summary>Initialisiert die Compiler-Demo. / Initializes the compiler demo.</summary>
    public TvHcApp(TRect bounds, bool headless = false, string? allowedOutputDirectory = null) : base(bounds, headless)
    {
        _allowedOutputDirectory = allowedOutputDirectory is null ? null : Path.GetFullPath(allowedOutputDirectory);
        ShowCompilation("valid.topic", ValidSource);
    }

    /// <summary>Sichtbarer Compilerverlauf. / Visible compiler history.</summary>
    public string VisibleHistoryText => string.Join('\n', _visibleHistory);
    /// <summary>Ob die letzte Ablehnung atomar ohne Teilmodell blieb. / Whether the last rejection stayed atomic without a partial model.</summary>
    public bool LastRejectedHadNoPartialResult { get; private set; }
    /// <summary>Letzter akzeptierter Ausgabepfad. / Last accepted output path.</summary>
    public string? LastOutputPath { get; private set; }
    /// <summary>Ob der letzte Persistenzpfad abgelehnt wurde. / Whether the last persistence path was rejected.</summary>
    public bool LastPersistRejected { get; private set; }
    /// <summary>Beschreibungstext. / Description text.</summary>
    public string DescriptionText =>
        "TvHc description: Kontrollierte .topic-Quellen liefern ein atomisches Help-Modell oder stabile Diagnosen; " +
        "Schreibnachweise bleiben im test-eigenen Temp-Verzeichnis. / Controlled .topic sources produce an atomic help model " +
        "or stable diagnostics; write proof stays in the test-owned temp directory.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem("~C~ompiler", 0,
                Wave3Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("Compile ~V~alid", CmCompileValid, null,
                    new TMenuItem("Compile ~I~nvalid", CmCompileInvalid)))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmCompileValid)
        {
            ShowCompilation("valid.topic", ValidSource);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmCompileInvalid)
        {
            ShowCompilation("invalid.topic", InvalidSource);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmPersistValid)
        {
            PersistOwned(@event.Message.Info as string);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave3Description("TvHc", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private THelpCompilationResult ShowCompilation(string sourceName, string source)
    {
        THelpCompilationResult result = _compiler.Compile(source, sourceName);
        if (result.Success)
        {
            string title = result.HelpFile!.Topics[0].Title;
            string state = $"success topic={title} count={result.HelpFile.Topics.Count}";
            _visibleHistory.Add(state);
            ShowWave3Result("TvHc", "TvHc compiler", $"TvHc compiler\nSource: {sourceName}\nResult: {state}\nTopic: {title}", state);
            LastRejectedHadNoPartialResult = false;
            return result;
        }

        string code = result.Diagnostics.Count > 0 ? result.Diagnostics[0].Code : "THC000";
        LastRejectedHadNoPartialResult = result.HelpFile is null && result.Symbols.Count == 0;
        string rejected = $"rejected {code} no-partial={LastRejectedHadNoPartialResult}";
        _visibleHistory.Add(rejected);
        ShowWave3Result("TvHc", "TvHc compiler", $"TvHc compiler\nSource: {sourceName}\nResult: {rejected}", rejected);
        return result;
    }

    private void PersistOwned(string? relativePath)
    {
        LastPersistRejected = true;
        if (_allowedOutputDirectory is null || string.IsNullOrWhiteSpace(relativePath) || Path.IsPathRooted(relativePath))
        {
            SetWave3Status("TvHc", "persist rejected: no owned target");
            return;
        }

        string root = Path.TrimEndingDirectorySeparator(_allowedOutputDirectory);
        string candidate = Path.GetFullPath(Path.Combine(root, relativePath));
        string prefix = root + Path.DirectorySeparatorChar;
        if (!candidate.StartsWith(prefix, StringComparison.Ordinal))
        {
            SetWave3Status("TvHc", "persist rejected: outside owned root");
            return;
        }

        THelpCompilationResult result = _compiler.Compile(ValidSource, "valid.topic");
        // Erst das vollstaendige atomische Modell darf den kontrollierten Persistenzpfad erreichen.
        // Only the complete atomic model may reach the controlled persistence path.
        if (!result.Success)
        {
            SetWave3Status("TvHc", "persist rejected: compile failed");
            return;
        }

        Directory.CreateDirectory(root);
        result.HelpFile!.Save(candidate);
        LastOutputPath = candidate;
        LastPersistRejected = false;
        SetWave3Status("TvHc", $"persisted={Path.GetFileName(candidate)}");
    }
}
