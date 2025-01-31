# Pokemon Version ASCII

[Release Link](https://github.com/Dranemo/PokemonASCII/releases/tag/ReleasePortfolio)


## /!\ <ins>Pour lancer le programme (Windows 11)</ins> /!\ 
Mettez la console en plein écran et dézoomez avant de lancer le jeu, sinon le programme va planter.  
## /!\ <ins>In Order to launch the program (Windows 11)</ins> /!\ 
Please make the console fullscreen and zoom out a little before playing, otherwise it will crash.



## Table of Content

1. [Français](#Francais)
   1. [Touches](#InputFr)
   2. [Fonctionnalités de base par zone](#FoncBaseFr)
   3. [Fonctionnalités spécifiques](#FoncSpeFr)
   4. [Bug connus](#GlitchFr)
   5. [Lancer le programme avec la solution](#RunFr)
2. [English](#Anglais)
   1. [Input](#InputEn)
   2. [Basic Features per zone](#FoncBaseEn)
   3. [Specific Features](#FoncSpeEn)
   4. [Known Glitches](#GlitchEn)
   5. [Run program using the solution](#RunEn)
3. [Credits](#Credits)




## Français <a name="Francais"></a>

***Projet réalisé en deux semaines dans la cadre de mes études au [Gaming Campus](https://gamingcampus.fr) en Fevrier 2024.***  

Projet réalisé en **C# en console**.  
Francais uniquement  
*Actuellement non continué.*  

Le projet recrée une petite partie du jeu Pokemon Rouge et Bleu initialement sorti en 1998 par GameFreak.  
La ville de Bourg-Palette ainsi que la Route 1 sont inclus dans le jeu. L'introduction du professeur Chen est également présente.  

### Touches <a name="InputFr"></a>
| Touches | Action |
|---|---|
| Flèches directionnelles | Déplacement |
| Entrer | Selectionner |
| Echap (Sur la carte) | Quitter le jeu |
| Echap (Dans un menu) | Quitter le menu |

### Fonctionnalités par zone <a name="FoncBaseFr"></a>

1. Bourg-Palette : 

   * Soin des Pokémons dans la maison, en interragissant avec la mère du joueur représentée par un 'M'.
   * PC inutilisable, mais permettant de voir les pokemon capturés si on en a plus de 6 dans son équipe.
   * Petite quête, en interragissant avec la soeur du rival dans la maison voisine, représentée par un 'N'.
   * Récupération du pokemon de départ dans la laboratoire du professeur Chen.
   * Combat contre le rival, représenté par un 'R' dans le laboratoire du professeur Chen.

2. Route 1 :

   * Rencontre aléatoire de pokemon sauvages parmis les 151 premiers.
   * PNJ donnant une potion au joueur si on interragit avec lui.

3. Introduction : 

   * Possibilité de nommer le joueur et le rival.



### Fonctionnalités spécifiques <a name="FoncSpeFr"></a>

- Système de combat très similaire à celui présent dans la première génération des jeux Pokémon.
  * Formule de dégats issue des jeux.[^1]
  * Formule de capture issue des jeux.[^2]
  * Changement de Pokémon possibles pendant le tour du joueur et à la mort de son Pokémon.
  * Utilisation d'objets possible.
- Système de Pokémon similaire aux jeux originaux.
  * Génération de statistiques (DV) aléatoires.
  * Gain de statistiques pour chaque Pokémon mis KO (EV).
  * Gain d'experience et montée de niveau pour chaque pokemon mis KO.
  * Génération des attaques correspondant à leur liste d'apprentissage de la première génération.
  * Evolution par montée de niveau.
  * Apprentissage d'attaque par montée de niveau.
  * Sprite de face[^3] et de dos[^4] de la première génération.
- Musiques et Sons de la première génération.[^5]



### Bug connus (issus d'un manque de temps) <a name="GlitchFr"></a>

* Plantage lorsque l'on tente d'utiliser un objet sans avoir de Pokémon.
* Problèmes d'affichage lors des combats avec tout qui se décale. 
* Bug qui freeze le programme lorsque le joueur pousse le PNJ de la route 1 contre le mur car la fonction de mouvement du PNJ ne contient pas de vérif s'il y en a un disponible.
* Plantage sur Windows 11 si la console est trop petite pour tout afficher



### Lancer le programme avec la solution <a name="RunFr"></a>
Dans le fichier Program.cs, trouver la classe "AdresseFile" et le string "FileDirection" en bas du fichier, et modifier l'adresse du fichier GameFiles.



### La license Pokemon, les sprites, les musiques, les sons et l'UI utilisés dans ce projet appartiennent à Nintendo et à GameFreak Company.  




## English <a name="Anglais"></a>

***Project completed in two weeks as part of my studies at [Gaming Campus](https://gamingcampus.fr) in February 2024.***  

Project implemented in **C# console application.**  
Only in french  
*Currently not continued.*  

The project recreates a small part of the Pokemon Red and Blue game originally released in 1998 by GameFreak.  
Pallet Town and Route 1 are included in the game. Professor Oak's introduction is also present.


### Input <a name="InputEn"></a>
| Input | Action |
|---|---|
| Directionnal Arrows | Movement |
| Enter | Select |
| Escape (On the Map) | Quit the Game |
| Escape (In a Menu) | Quit the Menu |


### Basic Features per zone <a name="FoncBaseEn"></a>

1. Pallet Town: 

   * Healing of Pokemon in the house by interacting with the player's mother represented by an 'M'.
   * PC is unusable but allows viewing of captured Pokemon if the player has more than 6 in their team.
   * Small quest by interacting with the rival's sister in the neighboring house, represented by an 'N'.
   * Obtaining the starter Pokemon in Professor Oak's laboratory.
   * Battle against the rival, represented by an 'R' in Professor Oak's laboratory.

2. Route 1:

   * Random encounter of wild Pokemon among the first 151.
   * NPC giving a potion to the player if interacted with.

3. Introduction:

   * Ability to name the player and the rival.
  


### Specific Features <a name="FoncSpeEn"></a>

- Combat system very similar to the first generation of Pokemon games.
  * Damage formula from the games.[^1]
  * Catch rate formula from the games.[^2]
  * Switching Pokemon possible during the player's turn and when their Pokemon faints.
  * Use of items is possible.
- Pokemon system similar to the original games.
  * Generation of random statistics (DV).
  * Stat gain for each Pokemon fainted (EV).
  * Experience gain and level-up for each Pokemon knocked out.
  * Generation of attacks corresponding to their first-generation movepool.
  * Evolution by level-up.
  * Learning attacks by level-up.
  * Front sprite[^3] and back sprite[^4] from the first generation.
- Music and sounds from the first generation.[^5]



### Known Glitches (due to time constraints) <a name="GlitchEn"></a>

* Crash when trying to use an item without having a Pokemon.
* Display issues during battles with everything shifting.
* Bug that freezes the program when the player pushes the NPC on Route 1 against the wall because the NPC's movement function lacks a check if there is an obstacle.
* Crash in windows 11 if there isnt enough space to show everything.



### Run the program with the solution <a name="RunEn"></a>
In the Program.cs file, find the "AddressFile" class and the "FileDirection" string at the bottom of the file, and modify the file's address in GameFiles.



### The Pokemon license, sprites, music, sounds, and UI used in this project belong to Nintendo and GameFreak Company.




## Credits <a name="Credits"></a>

* Caillot Yanaël - [LinkedIn](https://www.linkedin.com/in/ycaillot/) - [GitHub](https://github.com/Dranemo)
* Guellaf Melvin - [LinkedIn](https://www.linkedin.com/in/melvin-guellaff-353628202/) - [GitHub](https://github.com/MGuellaf)
* Gathelier Axel - [LinkedIn](https://www.linkedin.com/in/axel-gathelier-13198b252/) - [GitHub](https://github.com/GolfOcean334)
[^1]: [Source](https://www.pokebip.com/page/jeuxvideo/guide_tactique_strategie_pokemon/formules_mathematiques)
[^2]: [Source](https://www.pokepedia.fr/Capture_de_Pokémon)
[^3]: [Source](https://www.pokencyclopedia.info/fr/index.php?id=sprites/gen1/spr_red-blue_gb) 
[^4]: [Source](https://www.pokencyclopedia.info/fr/index.php?id=sprites/gen1/spr-b_red-blue_gb)
[^5]: [Source](https://www.zophar.net/music/gameboy-gbs/pokemon-red)
