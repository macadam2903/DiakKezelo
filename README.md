# Diákok Adatainak Kezelése – WinForms Alkalmazás


## Projekt Leírása
Ez a Windows Forms alkalmazás diákok adatainak kezelésére készült.  
A program képes CSV fájl beolvasására, az adatok deszerializálására, MySQL adatbázisba történő mentésére, valamint az adatok megjelenítésére egy DataGridView segítségével.  
Emellett lehetőséget biztosít új diákok manuális rögzítésére egy regisztrációs űrlapon keresztül.


## Funkciók


### 1. CSV fájl beolvasása
- A felhasználó kiválaszthat egy CSV fájlt.
- A CSV fájl elvárt formátuma:

Id,Name,Email,Age

1,Alice,alice@example.com,20

2,Bob,bob@example.com,22

- A beolvasás hibakezeléssel van ellátva (hibás fájl, hiányzó adatok).


### 2. Deszerializálás
- A program a **CsvHelper** könyvtár segítségével olvassa be a CSV fájlt.
- Az adatok egy `Student` osztály példányaivá alakulnak.
- A `Student` osztály mezői:
- `Id`
- `Name`
- `Email`
- `Age`


### 3. MySQL adatbázis kezelés
- A beolvasott adatok egy MySQL adatbázisban kerülnek tárolásra.
- A szükséges tábla automatikusan létrejön, ha még nem létezik.
- A program külön függvényt használ az adatok mentésére.


### 4. DataGridView megjelenítés
- A diákok adatai egy DataGridView komponensben jelennek meg.
- Az adatok közvetlenül a MySQL adatbázisból kerülnek betöltésre.
- Az adatok frissülnek új rekord hozzáadásakor.


### 5. Regisztrációs űrlap
- Új diák manuális felvétele:
- Név
- Email
- Életkor
- A "Submit" gomb megnyomásakor:
- Az adatok ellenőrzésre kerülnek
- Elmentésre kerülnek az adatbázisba
- A DataGridView automatikusan frissül


## Használt technológiák
- **C#**
- **Windows Forms**
- **MySQL**
- **CsvHelper**
- **DataGridView**


## Hibakezelés
- Hibás CSV fájl kezelése
- Érvénytelen vagy hiányzó adatok ellenőrzése
- Adatbázis-kapcsolati hibák kezelése
- Felhasználóbarát hibaüzenetek megjelenítése


## Követelmények
- .NET Framework / .NET
- MySQL Server
- MySql.Data NuGet csomag
- CsvHelper NuGet csomag
