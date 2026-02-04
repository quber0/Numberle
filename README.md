Numberle - dokumentacja projektu

Opis projektu

Numberle to gra logiczna napisana inspirowana popularnymi grami typu Wordle, w której zamiast słów użytkownik próbuje odgadnąć ukryty numer. Projekt jest rozwijany w języku C# w technologii WPF.

Funkcjonalności

Interaktywna gra zgadywania liczb lub sekwencji numerycznej

Ograniczona liczba prób na odgadnięcie

Kolorowanie 

Tryb dzienny z pseudolosowo generowaną liczbą na podstawie obecnej daty

Tryb losowy z losowo generowaną liczbą

Instalacja i uruchomienie

Wymagania wstępne

.NET SDK (wersja kompatybilna z projektem, np. .NET 6 lub nowszy)

Visual Studio 2022 z zainstalowaną obsługą technologii WPF (zalecane) / Visual Studio Code / Rider / wybrane IDE obsługujące technologię WPF

Git (opcjonalnie, do klonowania repo)

Krok po kroku

1. Sklonuj repozytorium

git clone https://github.com/quber0/Numberle.git

cd Numberle

2. Otwórz rozwiązanie w Visual Studio

3. Otwórz Numberle.sln.

4. Zbuduj projekt

W IDE wybierz Build → Build Solution.

5. Uruchom aplikację.

UI

Logiczny interfejs graficzny, który:

wyświetla instrukcje

przyjmuje dane z klawiatury

prezentuje okno dialogowe z liczbą do zgadnięcia przy zgadnięciu liczby bądź zużyciu wszystkich prób
