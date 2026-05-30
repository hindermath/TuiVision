# Welle-2 Guide-Review-Notizen / Wave 2 Guide Review Notes

## Deutsch

Jeder Welle-2-Guide muss den historischen Zweck, den erwarteten
Interaktionspfad, den Smoke- oder Startbefehl und eine kurze
Barrierefreiheitsnotiz nennen. Der Nachweis muss text-first funktionieren:
sichtbare Zustandswerte duerfen nicht nur ueber Farbe, Maus oder Layout
verstanden werden.

012-Review: Alle elf Welle-2-Guides nennen nun den normalen
`dotnet run --project examples/<Name>`-Start, den sichtbaren Menue- oder
Befehlspfad, das erwartete Feedback und die App-Loop-Smoke-Spur. File- und
Dialog-Designer-Pfade bleiben auf source-controlled Fixtures oder Metadaten
beschraenkt. Maus-only Bedienung ist nicht erforderlich.

013-Review: Alle elf Welle-2-Guides nennen jetzt die sichtbare
Hauptkomponente, den Bedienpfad, echte `TStatusLine`-Rueckmeldung,
`Help -> Description`, A11Y-/Text-first-Nachweis, historische Quelle und
bewusste Abweichung. Die Guides bleiben Deutsch zuerst und Englisch danach.
Primaere Smokes belegen nicht nur Text-History, sondern View-Baum und
gerenderte Buffer-/Cell-Regionen.

## English

Every wave-2 guide must name the historical purpose, the expected interaction
path, the smoke or launch command, and a short accessibility note. Evidence
must work text-first: visible state must not depend only on color, mouse input,
or layout.

012 review: all eleven wave-2 guides now name the normal
`dotnet run --project examples/<Name>` startup, the visible menu or command
path, the expected feedback, and the app-loop smoke trace. File and dialog
designer paths stay limited to source-controlled fixtures or metadata. Mouse-only
operation is not required.

013 review: all eleven wave-2 guides now name the visible main component,
operation path, real `TStatusLine` feedback, `Help -> Description`,
A11Y/text-first proof, historical source, and intentional deviation. The guides
remain German first and English second. Primary smokes prove not only text
history but also view tree and rendered buffer/cell regions.
