# Programmierprojekt: Puzzle

* Teammitglieder:
	1. Justus Saringer
	2. Patrick Giese
	3. Dominik Probst
* Team: Gruppe 8
* Semester: Wintersemester 2020/2021
* Zu bewertende Version: master-branch - aktuellste Version(commit:3e84b43 oder neuer)
  ACHTUNG beim Testen. Durch die zufällige Anordung der Puzzleteile, wie nach Absprache, können möglicherweise nicht lösbare Puzzle entstehen. 


## Installationshinweise

Hier Anweisungen hinterlegen die fuer eine Installation notwendig sind. Diese muessen 100% vollstaendig sein.

### Server Installation

Diese Anleitung ist nur für Visual Studio 2019 getestet. Unter anderen Versionen von VS bzw. anderen IDEs kann es zu abweichungen kommen![]([url](url))

1. Klonen oder laden Sie das [Repository](https://gitlab.rz.htw-berlin.de/programmierprojekt/wisi2020/grp08) herunter.
2. Öffnen Sie die PuzzleMaster.sln welche im Server-Pfad [programm/server/PuzzleMaster](https://gitlab.rz.htw-berlin.de/programmierprojekt/wisi2020/grp08/-/blob/cc5b25fddbfd34e67256b6da64cc4abd08f627f0/programm/server/PuzzleMaster/PuzzleMaster.sln) liegt mit Visual Studio oder importieren Sie das Projekt.
3. Veröffentlichen Sie das Projekt
	* In einem **Ordner**
	* Vorhandene Dateien **nicht** löschen
	* Konfiguration auf **Release**
	* Zielruntime **Portierbar**

Der Server ist dabei standardmäßig unter dem Port 5000 erreichbar.

Damit ist die Installation des Servers abgeschlossen. Hinweise zum Start und zur Verwendung des Servers sind unter dem Punkt [Verwendung der Software](https://gitlab.rz.htw-berlin.de/programmierprojekt/wisi2020/grp08#start-des-servers) zu finden.

### Anpassung der Startdard Puzzle-Bilder

Die Startdard Puzzle-Bilder können vor dem erstem Server-Start angepasst werden.
Dies kann in der [app.config](https://gitlab.rz.htw-berlin.de/programmierprojekt/wisi2020/grp08/-/blob/cc5b25fddbfd34e67256b6da64cc4abd08f627f0/programm/server/PuzzleMaster/PuzzleMaster/Properties/app.config) bearbeitet werden. Dafür muss der Wert _databasePictures_ geändert werden.
Wurde die Datenbank bereits beim erstem Server-Start initalisiert, so muss diese gelöscht werden, um die Änderungen wirksam zu machen.

### Client Installation

Diese Anleitung ist nur für Visual Studio 2019 getestet. Unter anderen Versionen von VS bzw. anderen IDEs kann es zu abweichungen kommen![]([url](url))

Für die Installaton der Anwendung entnehmen Sie bitte die .exe-Datei aus dem, für die Serverinstallation genannte, Repository unter \GitHub\grp08\programm\client\PuzzleMaster\bin\Release
Um auf den Code und die Debug-Version von Visual Studio zuzugreifen öffenen Sie bitte im selben Repository \GitHub\grp08\programm\client\PuzzleMaster\PuzzleMaster.sln


## Verwendung der Software

Wie verwendet man diese Software? Welches Programm muss man starten?

Bevor der Client konfiguriert und gestartet werden kann, muss zuerst der Server erreichbar sein.
Benutzerhandbuch: Diese Anwendung ist äußerst selbsterklärend und bedarf keiner weiteren Erklärung.


### Start des Servers

1. Führen Sie die _PuzzleMaster.exe_ aus, welche sich in Ihrem Veröffentlichungs-Ordner befindet.
2. Beim erstem Start öffnet sich eine Windows Firewall-Meldung. Wenn der Server aus dem Netzwerk erreichbar sein soll, muss der Zugriff gewährt oder ggf. die Firewall anderswaltig angepasst werden.
3. Sobald im Konsolen-Fenster anzeigt wird, dass die Anwendung gestartet ist, ist der Server erreichbar und antwortet auf Anfragen.

Da beim erstem Start die Datenbank erst erstellt und initalisiert werden muss, kann dieser etwas länger dauern. 

## Links und Verweise

1. Markdown Syntax: https://docs.gitlab.com/ee/user/markdown.html
2. Git fuer Windows: https://git-scm.com/download/win
