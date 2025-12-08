# Rakenne
---
- ## LoginScene
    - #### Main Camera
        ^Renderöi valikon
    - #### Canvas
        - __Login__-näppäin, logiikka puuttuu
        <br>
            - __Label__
            ^Login-näppäimen teksti (UI TextMeshPro)
            <br>
        - __Register__-näppäin, logiikka puuttuu
        <br>
            - __Label__
            ^Register-näppäimen teksti (UI TextMeshPro)
            <br>
        - __Play as Quest__
        <br>
            - __Label__
            ^Play as Quest-näppäimen teksti (UI TextMeshPro)
    - #### EventSystem
        ^Tuli automaattisesti Canvas mukana, en tiedä mitä tekee

    - #### MenuManager
        ^Sulkee LoginScenen ja aloittaa StartScenen OnClick toiminnolla. OnClick:iin on liitetty MenuManager objekti, joka sisältää aloitusvalikon painikkeiden logiikan. Tällä hetkellä Unity tuhoaa StartScenen tiedot pelin käynnistyessä.
---
- ## StartScene
    - #### Main Camera
        ^Renderöi valikon

    - #### Canvas
        - __StartButton__
        ^ Sulkee StartScenen ja aloittaa MainScenen OnClick toiminnolla. OnClick:iin on liitetty MenuManager objekti, joka sisältää aloitusvalikon painikkeiden logiikan. Tällä hetkellä Unity tuhoaa StartScenen tiedot pelin käynnistyessä. Jos pelistä halutaan takaisin valikkoon niin katso Additive Mode tai DontDestroyOnLoad
        <br>
            - __Label__
            ^StartButtonin teksti (UI TextMeshPro)
        - __ExitButton__ sulkee sovelluksen
            - __Label__
            ^ExitButtonin teksti (UI TextMeshPro)
    - #### EventSystem
        ^Tuli automaattisesti Canvas mukana, en tiedä mitä tekee
    - #### MenuManager
        ^Sisältää scriptitiedoston aloitusvalikon painikkeiden toimintoihin
---
- ## MainScene
    - #### Map
        ^Kenttänä pelkkä neliö, jonka väri ja skaala vaihdettu
    - #### Player
        ^__HUOM__ Order in Layer 4, jotta renderöityy kentän ja massapallojen päälle. __Sisältää liikkeen ja törmäyksen scriptit, Colliderin ja Rigibodyn.__ Rigibody Gravity Scale > 0 ja Constraints > Freeze Rotation Z.
        <br>
        - __Main Camera__
            ^Seuraa Playeriä koska perii automaattisesti äitielementtinsä sijaintitiedot. Projection Size > 20 muutettu kameran etäisyyttä
        - __Label__
            ^Playerin teksti (3D TextMeshPro, koska 3D:llä ei tarvitse Canvasta). __HUOM__ Extra settings > Order in Layer 5 jotta näkyy Player (Order in Layer 4) päällä.
    - #### CircleSpawner
        ^ Generoi liitetyllä scriptillä 100 MassCell-prefabea pelin alussa, ja aina kun kentältä syödään yksi massasolu niin yksi solu generoituu tilalle. Skripti kommunikoi massCell-prefabiin liitetyn CircleBehavior-skriptin kanssa.
    - #### EnemyAi
        ^__HUOM__ Order in Layer 4 (renderöityy samalle tasolle kuin pelaaja). Liike-skripti ohjaa automaattisesti syömään massapalloja, paitsi jos se näkee pelaajan ja on sitä isompi, niin tällöin se lähtee pelaajan perään. Sisältää Rigibodyn ja Colliderin. Rigibody Gravity Scale > 0 ja Constraints Freeze Rotation Z.
        - __Label__ 
            ^EnemyAi teksti (3D TextMeshPro, koska 3D:llä ei tarvitse Canvasta). __HUOM__ Extra settings > Order in Layer 5 jotta näkyy Player (Order in Layer 4) päällä.
___
# Kehitysehdotukset

## GUI
Login ja Register näppäimiin toiminto. Ohjaavat painettaessa ehkä uuteen Sceneen - tai - renderöivät samaan Sceneen input kentät?

Kirjautuneille uusi Scene kuin nykyinen StartScene, jossa näkyy pelkästään Start ja Exit näppäimet.

Oma ja Global Highscore kirjautuneille (ja mahdollinen achievement/kauppasysteemi) kirjautuneiden Sceneen.

Global Highscore vieraille.

## Pelaajan mekaniikat

Kameraan zoomi koon mukaan.

Sopivan liikenopeuden säätäminen.

Massapallojen generointilogiikan säätäminen.

Koon vertaaminen __Rigibodyn__ massaan. (Tällä hetkellä koon vertailu perustuu pelaajan <=> vihollisen __Transformin__ skaalojen vertaamiseen. Voi olla helpompi tallentaa highscoret massan perusteella)

Pickup toimintojen toteuttaminen

Colliderin säätäminen (nyt "syöminen" tapahtuu kun pelaaja <=> vihollinen ympyrän reunat osuvat toisiinsa)

## AI mekaniikat

AI objektiin (EnemyAI) logiikka jolla eri AI:t reagoivat myös toisiinsa, mikäli kentällä useampi AI. (Tällä hetkellä AI reagoi vain pelaajaan mikäli näkee pelaajan ja on isompi kuin pelaaja)

Logiikan parannus. (Ei lähde karkuun jos pelaaja on isompi)

AI objektista (EnemyAI) prefabin tekeminen.

Colliderin säätäminen (nyt "syöminen" tapahtuu kun pelaaja <=> vihollinen ympyrän reunat osuvat toisiinsa)

## Powerupit
Ideointi.
Tekeminen.
(Luultavasti saman tyylinen toteutus kuin Massapalloilla)

## Grafiikat

Ulkoasu yleisesti hienommaksi.

Efektejä eri toimintoihin.

## Äänet

Taustamusiikki?

Ääniefekti kun:
- syöt massapallon
- syöt pelaajan
- tulet syödyksi
- powerup ilmestyy lähelle
- painaa valikon painiketta

## API

Tietokannan alustaminen.

Login logiikka.

Register logiikka.

Highscore tallennus ja haku.

(Achievementit/raha per käyttäjä)

## Muut

Mappi isommaksi. (ehkä jopa skaalautuen isoimman pelaajan koon mukaan > Massapallojen spawnaus alue myös)

Esteitä tai muita ominaisuuksia kentälle.

Peleistä / massasta / highscoreista / mistä vaan saa rahaa jolla voi ostaa > Perkit/upgradet (pysyvyiä / seuraavaan x peliä / tms)?
