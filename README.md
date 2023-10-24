# Task Management App - (Aplikacja do zarz�dzania zadaniami)
Aplikacja do zarz�dzania list� zada� utworzonych przez u�ytkownika.

## Zastosowane technologie
- WPF (.Net Framework 4.8),
- Entity Framework (3.1.32), podej�cie "Code first",
- Microsoft SQL Server,
- Wzorzec MVVM,
- Microsoft Visual Studio 2022.

## Funkcjonalno�ci
- Dodawanie nowych zada� (nazwa, opis, termin ko�cowy, pozosta�e dni do ko�cowego terminu, priorytet, status, widoczno��),
- Wy�wietlanie listy zada� (powi�zanych z obecnie zalogowanym u�ytkownikiem / kontem),
- Mo�liwo�� sortowania listy zada� po wskazanej kolumnie przez u�ytkownika (nale�y klikn�� nag��wek kolumny),
  domy�lne sortowanie odbywa si� po warto�ci priorytetu,
- Edytowanie wskazanego przez u�ytkownika zadania,
- Usuwanie zadania,
- Mo�liwo�� szybkiej edycji statusu i widoczno�ci zadania,
- Filtrowanie listy zada� wskazuj�c po��dany status lub widoczno��,
- Mo�liwo�� przeszukania listy podan� fraz� (obejmuje: nazw�, opis, priorytet, termin ko�cowy, pozosta�e dni do ko�cowego terminu),
- Tworzenie nowych kont u�ytkownika (username, email, password),
- Mo�liwo�� zalogowania si� na konto i wylogowania.

## Diagram ERD
|![ERD](/ReadmeImages/ERD.drawio.png) | 
|:--:| 
| *Rysunek 1. Diagram ERD obrazuj�cy struktur� bazy danych.* |

## U�ytkowanie aplikacji
### 1. Populacja bazy danych
Po uruchomieniu, aplikacja sprawdzi czy wymagana baza danych istnieje (na podstawie podanej w kodzie warto�ci sta�ej CONNECTION_STRING w dw�ch miejscach: w pliku App.xaml.cs, oraz Data/AppDesignTimeDbContextFactory.cs), je�eli nie istnieje, zostanie utworzona, a nast�pnie uruchomiona zostanie metoda populacji bazy danych zawieraj�ca dw�ch u�ytkownik�w:
- Username: **Test1** | Password: **qwerty** (posiada 5 zada�),
- Username: **Test2** | Password: **qwerty** (posiada 0 zada�).
  
### 2. Pierwszy widok po uruchomieniu
Pierwszym widokiem aplikacji jaki si� pojawi b�dzie okno logowania. Nale�y si� zalogowa� na istniej�ce konto (np. Test1) lub utworzy� nowe klikaj�c w odno�nik "Click here to create account".

|![LoginView](/ReadmeImages/LoginView.PNG) | 
|:--:| 
| *Rysunek 2. Widok logowania utworzonej aplikacji* |

### 3. Widok rejestracji
Po klikni�ciu w odno�nik "Click here to create account" z poziomu widoku logowania. Aplikacja przeskoczy do widoku rejestracji z poziomu, kt�rego istnieje mo�liwo�� za�o�enia konta, gdzie nale�y poda� wszystkie z trzech p�l:
- Username (warto�� unikalna, 3-15 znak�w, znaki specjalne zabronione),
- Email (warto�� unikalna, musi spe�nia� og�lne wymogi adresu email),
- Password (przynajmniej 8 znak�w, w tym przynajmniej 1 znak du�ej litery, 1 znak ma�ej litery, 1 znak cyfry oraz 1 znak specjalny).

Z poziomu okna rejestracji istnieje mo�liwo�� przej�cia do okna logowania klikaj�c w odno�nik "click here to login".

|![RegisterView](/ReadmeImages/RegisterView.PNG) | 
|:--:| 
| *Rysunek 3. Widok rejestracji utworzonej aplikacji* |

### 4. G��wny widok z list� zada� u�ytkownika
Po pomy�lnym zalogowaniu si� na istniej�ce konto pojawi si� widok z list� zada� przypisanych do zalogowanego u�ytkownika.

#### Po lewej stronie widoku istnieje mo�liwo�� ustawienia filtr�w:
- podaj�c fraz� przeszukuj�c list� zada�,
- podaj�c znak mniejszo�ci/wi�kszo�ci oraz warto�� priorytetu,
- zaznaczaj�c status zada�, kt�re maj� zosta� wy�wietlone,
- zaznaczaj�c widoczno�� zada�, kt�re maj� zosta� wy�wietlone.
  
Po wprowadzeniu po��danych filtr�w nale�y klikn�� przycisk poni�ej "Apply filters".
Aby przywr�ci� filtry do ustawie� domy�lnych nale�y klikn�� "Clear filters".

#### W g�rnej cz�ci okna znajduje si� tekst z nazw� zalogowanego u�ytkownika i trzy przycisk, umo�liwiaj�ce kolejno:
- wylogowanie si� z aplikacji (przej�cie do okna logowania),
- minimalizacja okna,
- zamkni�cie aplikacji.

#### W �rodkowej cz�ci znajduje si� lista z zadaniami przypisanymi do u�ytkownika
Lista domy�lnie jest sortowana po warto�ci priorytetu malej�co. Aby zmieni� kolumn�, po kt�rej lista jest sortowana, nale�y klikn�� w po��dany nag��wek/header listy. Nie mo�na wprowadza� zmian bezpo�rednio na li�cie, jest ona w trybie tylko do odczytu.

Aby zobaczy� opis, dat� utworzenia zadania, oraz otworzy� mo�liwo�� szybkiej zmiany statusu i widoczno�ci zadania, nale�y klikn�� w wybrane zadanie.

|![TaskMenu_1](/ReadmeImages/TaskMenuView.PNG) | 
|:--:| 
| *Rysunek 4. Widok G��wnego okna z list� zada� w utworzonej aplikacji* |

|![TaskMenu_2](/ReadmeImages/TaskMenuView_SelectedTask.png) | 
|:--:| 
| *Rysunek 5. Widok G��wnego okna z zaznaczonym zadaniem w utworzonej aplikacji* |

#### W dolnej cz�ci znajduje si�:
- tekst z logami powiadamiaj�cy u�ytkownika o pomy�lno�ci przeprowadzonej akcji lub o niepo��danym b��dzie, kt�ry uniemo�liwi� wykonanie operacji. 
- przycisk "Create", umo�liwiaj�cy utworzenie nowego zadania, po klikni�ciu pojawi si� nowe okienko. 
- przycisk "Edit", umo�liwiaj�cy edycj� zaznaczonego zadania, po klikni�ciu pojawi si� nowe okienko. Jest on zablokowany gdy �adne z zada� nie jest zaznaczone.
- przycisk "Remove", umo�liwiaj�cy usuni�cie zaznaczone zadanie. 

Wszystkie z powy�szych przycisk�w s� dodatkowo zablokowane gdy istnieje ju� otwarte okno do tworzenia/edycji zadania. 

## 5. Okno tworzenia oraz edycji zadania

Po klikni�ciu w przycisk Create, pojawi si� nowe okienko, w kt�rym nale�y poda� warto�� wszystkich p�l oznaczonych znakiem "*" (Name, Priority). Po podaniu wymaganych warto�ci przycisk "Create task" zostanie odblokowany.

|![TaskMenu_2](/ReadmeImages/TaskMenuView_Create.PNG) | 
|:--:| 
| *Rysunek 6. Widok G��wnego okna wraz z okienkiem tworzenia nowego zadania w utworzonej aplikacji* |

Po klikni�ciu w przycisk "Edit", pojawi si� nowe okienko, w kt�rym nale�y zaktualizowa� warto�ci po��danych p�l, pami�taj�c aby nie pozostawi� pustych p�l oznaczonych znakiem "*" (Name, Priority, Status, Is Hidden). Po podaniu wymaganych warto�ci przycisk "Update task" zostanie odblokowany.

|![TaskMenu_2](/ReadmeImages/TaskMenuView_Update.PNG) | 
|:--:| 
| *Rysunek 6. Widok G��wnego okna wraz z okienkiem edycji zaznaczonego zadania w utworzonej aplikacji* |