# Event-Loop, Ereignisse und Commands / Event Loop, Events, and Commands

## Deutsch

### Lernziel und Voraussetzungen

Du solltest den [Architekturüberblick](../architecture.md) gelesen haben. Danach
kannst du erklären, warum ein Test den echten `Run()`-Pfad und nicht nur eine
Hilfsmethode ausführen muss.

### Ablauf

1. `TProgram.Run()` markiert den View-Baum als sichtbar und zeichnet den ersten
   Frame.
2. `GetEvent()` liefert ein `TEvent`. `Nothing` führt zu `Idle()` und einer
   begrenzten CPU-Freigabe.
3. `HandleEvent()` leitet Tastatur, Maus oder Command durch die View-Hierarchie.
4. Ein behandeltes Ereignis wird geleert. Nicht behandelte Commands können zur
   Shell weiterlaufen.
5. Nach einem echten Ereignis zeichnet und präsentiert die Anwendung den neuen
   Zustand.
6. Ein Quit-Command beendet die Schleife und löst den geordneten Shutdown aus.

`TGroup` verarbeitet Ereignisse in den Phasen PreProcess, fokussierte View und
PostProcess. Die Reihenfolge verhindert, dass eine überdeckte oder deaktivierte
View denselben Befehl unbemerkt ausführt.

### Proof-Grenze

Ein primärer Smoke-Test injiziert Events oder Commands und führt `app.Run()`
oder den gleichwertigen echten Loop aus. Eine direkte Helper-Methode darf Setup
oder Zusatzprüfung sein, beweist aber allein weder Dispatch noch Draw.

### Übung

Starte `examples/MsgCls`, löse einen Menübefehl aus und beobachte die
Statuszeile. Suche anschließend den Command im zugehörigen Smoke-Test. Nächster
Schritt: [View-Hierarchie](view-hierarchy.md).

## English

### Learning goal and prerequisites

Read the architecture overview first. You should then be able to explain why a
test must exercise the real `Run()` path rather than only a helper method.

### Flow and proof boundary

`TProgram.Run()` exposes and draws the tree, receives one `TEvent`, routes it,
and redraws after real work. `Nothing` runs bounded idle processing. `TGroup`
uses pre-process, focused-view, and post-process phases. A quit command leaves
the loop through the ordered shutdown path.

A primary smoke injects events or commands through the actual app loop. Direct
helpers may prepare state or add assertions, but they do not prove dispatch and
rendering on their own.

### Exercise

Launch `examples/MsgCls`, trigger a menu command, and observe the status line.
Then locate that command in its smoke test and continue with the view hierarchy.
