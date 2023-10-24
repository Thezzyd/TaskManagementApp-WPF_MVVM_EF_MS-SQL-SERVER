# Task Management App - (Aplikacja do zarz¹dzania zadaniami)
Aplikacja do zarz¹dzania list¹ zadañ utworzonych przez u¿ytkownika.

## Zastosowane technologie
- WPF (.Net Framework 4.8),
- Entity Framework (3.1.32), podejœcie "Code first",
- Microsoft SQL Server,
- Wzorzec MVVM,
- Microsoft Visual Studio 2022.

## Funkcjonalnoœci
- Dodawanie nowych zadañ (nazwa, opis, termin koñcowy, pozosta³e dni do koñcowego terminu, priorytet, status, widocznoœæ),
- Wyœwietlanie listy zadañ (powi¹zanych z obecnie zalogowanym u¿ytkownikiem / kontem),
- Mo¿liwoœæ sortowania listy zadañ po wskazanej kolumnie przez u¿ytkownika (nale¿y klikn¹æ nag³ówek kolumny),
  domyœlne sortowanie odbywa siê po wartoœci priorytetu,
- Edytowanie wskazanego przez u¿ytkownika zadania,
- Usuwanie zadania,
- Mo¿liwoœæ szybkiej edycji statusu i widocznoœci zadania,
- Filtrowanie listy zadañ wskazuj¹c po¿¹dany status lub widocznoœæ,
- Mo¿liwoœæ przeszukania listy podan¹ fraz¹ (obejmuje: nazwê, opis, priorytet, termin koñcowy, pozosta³e dni do koñcowego terminu),
- Tworzenie nowych kont u¿ytkownika (username, email, password),
- Mo¿liwoœæ zalogowania siê na konto i wylogowania.

## Diagram ERD
|![ERD](/ReadmeImages/ERD.drawio.png) | 
|:--:| 
| *Rysunek 1. Diagram ERD obrazuj¹cy strukturê bazy danych.* |

## U¿ytkowanie aplikacji
### 1. Populacja bazy danych
Po uruchomieniu, aplikacja sprawdzi czy wymagana baza danych istnieje (na podstawie podanej w kodzie wartoœci sta³ej CONNECTION_STRING w dwóch miejscach: w pliku App.xaml.cs, oraz Data/AppDesignTimeDbContextFactory.cs), je¿eli nie istnieje, zostanie utworzona, a nastêpnie uruchomiona zostanie metoda populacji bazy danych zawieraj¹ca dwóch u¿ytkowników:
- Username: **Test1** | Password: **qwerty** (posiada 5 zadañ),
- Username: **Test2** | Password: **qwerty** (posiada 0 zadañ).
  
### 2. Pierwszy widok po uruchomieniu
Pierwszym widokiem aplikacji jaki siê pojawi bêdzie okno logowania. Nale¿y siê zalogowaæ na istniej¹ce konto (np. Test1) lub utworzyæ nowe klikaj¹c w odnoœnik "Click here to create account".

|![LoginView](/ReadmeImages/LoginView.PNG) | 
|:--:| 
| *Rysunek 2. Widok logowania utworzonej aplikacji* |

### 3. Widok rejestracji
Po klikniêciu w odnoœnik "Click here to create account" z poziomu widoku logowania. Aplikacja przeskoczy do widoku rejestracji z poziomu, którego istnieje mo¿liwoœæ za³o¿enia konta, gdzie nale¿y podaæ wszystkie z trzech pól:
- Username (wartoœæ unikalna, 3-15 znaków, znaki specjalne zabronione),
- Email (wartoœæ unikalna, musi spe³niaæ ogólne wymogi adresu email),
- Password (przynajmniej 8 znaków, w tym przynajmniej 1 znak du¿ej litery, 1 znak ma³ej litery, 1 znak cyfry oraz 1 znak specjalny).

Z poziomu okna rejestracji istnieje mo¿liwoœæ przejœcia do okna logowania klikaj¹c w odnoœnik "click here to login".

|![RegisterView](/ReadmeImages/RegisterView.PNG) | 
|:--:| 
| *Rysunek 3. Widok rejestracji utworzonej aplikacji* |

### 4. G³ówny widok z list¹ zadañ u¿ytkownika
Po pomyœlnym zalogowaniu siê na istniej¹ce konto pojawi siê widok z list¹ zadañ przypisanych do zalogowanego u¿ytkownika.

#### Po lewej stronie widoku istnieje mo¿liwoœæ ustawienia filtrów:
- podaj¹c frazê przeszukuj¹c listê zadañ,
- podaj¹c znak mniejszoœci/wiêkszoœci oraz wartoœæ priorytetu,
- zaznaczaj¹c status zadañ, które maj¹ zostaæ wyœwietlone,
- zaznaczaj¹c widocznoœæ zadañ, które maj¹ zostaæ wyœwietlone.
  
Po wprowadzeniu po¿¹danych filtrów nale¿y klikn¹æ przycisk poni¿ej "Apply filters".
Aby przywróciæ filtry do ustawieñ domyœlnych nale¿y klikn¹æ "Clear filters".

#### W górnej czêœci okna znajduje siê tekst z nazw¹ zalogowanego u¿ytkownika i trzy przycisk, umo¿liwiaj¹ce kolejno:
- wylogowanie siê z aplikacji (przejœcie do okna logowania),
- minimalizacja okna,
- zamkniêcie aplikacji.

#### W œrodkowej czêœci znajduje siê lista z zadaniami przypisanymi do u¿ytkownika
Lista domyœlnie jest sortowana po wartoœci priorytetu malej¹co. Aby zmieniæ kolumnê, po której lista jest sortowana, nale¿y klikn¹æ w po¿¹dany nag³ówek/header listy. Nie mo¿na wprowadzaæ zmian bezpoœrednio na liœcie, jest ona w trybie tylko do odczytu.

Aby zobaczyæ opis, datê utworzenia zadania, oraz otworzyæ mo¿liwoœæ szybkiej zmiany statusu i widocznoœci zadania, nale¿y klikn¹æ w wybrane zadanie.

|![TaskMenu_1](/ReadmeImages/TaskMenuView.PNG) | 
|:--:| 
| *Rysunek 4. Widok G³ównego okna z list¹ zadañ w utworzonej aplikacji* |

|![TaskMenu_2](/ReadmeImages/TaskMenuView_SelectedTask.png) | 
|:--:| 
| *Rysunek 5. Widok G³ównego okna z zaznaczonym zadaniem w utworzonej aplikacji* |

#### W dolnej czêœci znajduje siê:
- tekst z logami powiadamiaj¹cy u¿ytkownika o pomyœlnoœci przeprowadzonej akcji lub o niepo¿¹danym b³êdzie, który uniemo¿liwi³ wykonanie operacji. 
- przycisk "Create", umo¿liwiaj¹cy utworzenie nowego zadania, po klikniêciu pojawi siê nowe okienko. 
- przycisk "Edit", umo¿liwiaj¹cy edycjê zaznaczonego zadania, po klikniêciu pojawi siê nowe okienko. Jest on zablokowany gdy ¿adne z zadañ nie jest zaznaczone.
- przycisk "Remove", umo¿liwiaj¹cy usuniêcie zaznaczone zadanie. 

Wszystkie z powy¿szych przycisków s¹ dodatkowo zablokowane gdy istnieje ju¿ otwarte okno do tworzenia/edycji zadania. 

## 5. Okno tworzenia oraz edycji zadania

Po klikniêciu w przycisk Create, pojawi siê nowe okienko, w którym nale¿y podaæ wartoœæ wszystkich pól oznaczonych znakiem "*" (Name, Priority). Po podaniu wymaganych wartoœci przycisk "Create task" zostanie odblokowany.

|![TaskMenu_2](/ReadmeImages/TaskMenuView_Create.PNG) | 
|:--:| 
| *Rysunek 6. Widok G³ównego okna wraz z okienkiem tworzenia nowego zadania w utworzonej aplikacji* |

Po klikniêciu w przycisk "Edit", pojawi siê nowe okienko, w którym nale¿y zaktualizowaæ wartoœci po¿¹danych pól, pamiêtaj¹c aby nie pozostawiæ pustych pól oznaczonych znakiem "*" (Name, Priority, Status, Is Hidden). Po podaniu wymaganych wartoœci przycisk "Update task" zostanie odblokowany.

|![TaskMenu_2](/ReadmeImages/TaskMenuView_Update.PNG) | 
|:--:| 
| *Rysunek 6. Widok G³ównego okna wraz z okienkiem edycji zaznaczonego zadania w utworzonej aplikacji* |