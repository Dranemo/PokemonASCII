# Pokemon Version ASCII

## Projet réalisé en deux semaines dans la cadre de mes études au [Gaming Campus](https://gamingcampus.fr).
### Projet réalisé en C# en console.
### *Acuellement non continué.*


Le projet recrée une petite partie du jeu Pokemon Rouge et Bleu initialement sorti en 1998 par GameFreak.
La ville de Bourg-Palette ainsi que la Route 1 sont inclus dans le jeu. L'introduction du professeur Chen est également présente.

### Bourg-Palette :

* Soin des Pokémons dans la maison, en interragissant avec la mère du joueur représentée par un 'M'.
* PC inutilisable, mais permettant de voir les pokemon capturés si on en a plus que 6.
* Petite quête, en interragissant avec la soeur du rival dans la maison voisine, représentée par un 'N'.
* Récupération du pokemon de départ dans la laboratoire du professeur Chen.
* Combat contre le rival, représenté par un 'R' dans le laboratoire du professeur Chen.

### Route 1 :

* Rencontre aléatoire de pokemon sauvages parmis les 151 premiers.
* PNJ donnant une potion au joueur si on interragit avec lui.

### Introduction : 

* Possibilité de nommer le joueur et le rival.



### Fonctionnalités spécifiques :

* Système de combat très similaire à celui présent dans la première génération des jeux Pokémon.
  * Formule de dégats issue des jeux.[^1]
  * Formule de capture issue des jeux.[^2]
  * Changement de Pokémon possibles pendant notre tour et à la mort de notre Pokémon.
  * Utilisation d'objets possible.
* Système de Pokémon similaire aux jeux originaux.
  * Génération de statistiques (DV) aléatoires.
  * Gain de statistiques pour chaque Pokémon mis KO (EV).
  * Gain d'experience et montée de niveau pour chaque pokemon mis KO.
  * Génération des attaques correspondant à leur liste d'apprentissage de la première génération.
  * Evolution par montée de niveau.
  * Apprentissage d'attaque par montée de niveau.
  * Sprite de face et de dos de la première génération.[^3]
* Musiques et Sons de la première génération.[^4]



### Bug connus (issus d'un manque de temps) : 

* Plantage lorsque l'on tente d'utiliser un objet sans avoir de Pokémon.
* Problèmes d'affichage lors des combats avec tout qui se décale. 
* Bug qui freeze le programme lorsque le joueur pousse le PNJ de la route 1 contre le mur car la fonction de mouvement du PNJ ne contient pas de vérif s'il y en a un disponible.

### Lancer le programme avec la solution :
Dans le fichier Program.cs, trouver la classe "AdresseFile" et le string "FileDirection" en bas du fichier, et modifier l'adresse du fichier GameFiles.


### Projet réalisé par :

* Caillot Yanaël : [LinkedIn](https://www.linkedin.com/in/ycaillot/) [GitHub](https://github.com/Dranemo)
* Guellaf Melvin : [LinkedIn](https://www.linkedin.com/in/melvin-guellaff-353628202/) [GitHub](https://github.com/MGuellaf)
* Gathelier Axel : [LinkedIn](https://www.linkedin.com/in/axel-gathelier-13198b252/) [GitHub](https://github.com/GolfOcean334)

### La license Pokemon, les sprites, les musiques, les sons et l'UI utilisés dans ce projet appartiennent à Nintendo et à GameFreak Compagny.


[^1]: [Formule de dégats utilisée](https://www.pokebip.com/page/jeuxvideo/guide_tactique_strategie_pokemon/formules_mathematiques)
[^2]: [Formule de capture utilisée](https://www.pokepedia.fr/Capture_de_Pokémon)
[^3]: [Sprites de face](https://www.pokencyclopedia.info/fr/index.php?id=sprites/gen1/spr_red-blue_gb) [Sprites de dos](https://www.pokencyclopedia.info/fr/index.php?id=sprites/gen1/spr-b_red-blue_gb)
[^4]: [Musiques et sons](https://www.zophar.net/music/gameboy-gbs/pokemon-red)
