# NADND
## Der Titel
Um die Frage zu klären, der Titel steht für **N**ot **A**nother **D**ungeons a**n**d **D**ragons Adventure, wobei das _Adventure_ am Ende nicht im Titel auftauch.

## Hintergrund
Dieses Projekt ist im Rahmen einer Fachpraxisarbeit an meiner Schule entstanden. Es ist ein Textadventure, was starke Inspirationen aus den "Live your own Adventure"-Büchern zieht. Hier ist _ganz ganz viel_ Verzweiflung reingeflossen. Warum ist Unity einfach manchmal echt weird. Immerhin haben wir alle was gelernt und hatten vielleicht auch ein bisschen Spaß daran...
## Eingener Content
Das Spiel lädt den Gesamten Inhalt beim Spielstart dynamisch. Dadurch kann man auch als Spieler ohne große Programmierkenntnisse eigenen Inhalt schreiben. Dazu muss man eine Json Datei in den StoryParts Ordner packen und dann das Spiel neu starten. Der StoryParts Ordner ist im StreamingAssets Ordner, welcher sich im Assets Ordner befindet.
### Die Parameter der Datei
Als Beispiel dient hier die [Vorlage.json](https://github.com/TeilzeitTodesengel/NADND/blob/main/Assets/StreamingAssets/StoryParts/Vorlage.json).
- roomID: Die RoomID ist eine **einzigartige** ID, über die der Raum angesprochen wird. Jeder Raum muss so eine haben.
- roomName: Dieser Parameter ist überflüssig, muss aber aus Kompatibilitäsgründen in der Datei vorhanden sein.
- Description: Die Description ist der Text, welcher im Spiel angezeigt wird, wenn man den Part erreicht. Er darf alphanumerische Zeichen enthalten. Wegen den Limitationen von kann man allerdings keine neue Zeile über die Entertaste machen. Dafür muss man die Zeichenkombination "\n" benutzen.
- musicID: Dieser Parameter ist der Soundtrack, welcher im Hintergrund laufen soll. Er muss der Dateiname der Musik sein, aber darf die Dateiendung nicht enthalten. (Bsp.: "Musik.mp3", ist die Datei, also ist die musicID "Musik"). Die Datei mit dem Soundtrack muss im Music Ordner sein. Es werden die Formate MP3, WAV und OGG unterstüzt.
- isFight: Bestimmt ob der Part ein Kampf ist. (true = Kampf, false != Kampf). Bei einem Kampf wird der nächste Parameter fight wichtig. Nur wenn der Raum ein Kampf ist wird das Monster auch geladen und angezeigt.
- fight: Hier ist alles relevante für den Kampf drin. S. Abschnitt Kampf.
- abzweigungen: Hier werden die einzelnen möglichkeiten definiert, was man nach dem Raum machen kann. Weitere Infos unter Abzweigungen
### Fight
Ein Fight beinhaltet immer ein Monster. Die Eigenschaften des Monsters werden hier festgelegt.
- Name: Der Name des Monsters, dass angezeigt wird. Gleichzeitig wird hierrüber auch das Artwork des Monsters geladen. Das Artwork muss im Ordner MonsterImages sein und muss den Dateitypen PNG haben.
- Staerke: Der Schade, den dass  Monster verursacht. Nur Ganzzahlen
- Wiederstandskraft: Die Lebenspunkte vom Monster, wenn diese auf null sinken ist der Kampf zu ende. Nur Ganzzahlen
### Abzweigungen
Ein StoryPart kann 0-4 Abzweigungen haben. Alle restlichen Abzweigungen werden ignoriert.
- description: Dies ist der Text, wass im Knopf angezeigt wird.
- targetID: Das ist eine roomID, wo diese Abzeigung hinführen soll. Sie muss exakt mit der des  Zielraumes übereinstimmen. Kapitalisierung ist relevant.
- loot: siehe Loot
### Loot
Loot beschreibt die Items, welcher ein Spieler als Belohung erhält, wenn er diese Abzweigung auswählt. Jede Abzweigung muss immer Loot enthalten, wenn der Spieler allerdings kein Item bekommen soll, muss der Name ein leerer String sein (als Referenz "").
- Name: Der Name der Waffe. Dieser wird im Inventar angezeigt
- Description: Ein längerer Text, welcher das Item genauer beschreibt. Wird im Inventar angezeigt.
- Damage: Der Schaden der Waffe. Nur Ganzzahlen.
