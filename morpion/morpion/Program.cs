/*
 * Created by SharpDevelop.
 * User: j.leverd
 * Date: 13/10/2023
 * Time: 11:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace morpion
{
	class Program
    {
        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int j, int k)
        {
        	for ( j = 0; j < grille.GetLength(0); j++) 
            {
                Console.Write("\n|===|===|===|\n"); // affiche a chaque ligne
                Console.Write("|"); // entre chaque case
                for ( k = 0; k < grille.GetLength(1); k++)
                {
                    if (grille[j,k] == 1) // si grille de j,k = 1 soit = joueur 1 alors :
                	{
                		Console.Write(" X "); // on affiche une croix dans la case
                	}
                	if (grille[j,k] == 2)
                	{
                		Console.Write(" O ");
                	}
                	if (grille[j,k] == 10)
                	{
                		Console.Write("   ");
                	}
                	Console.Write("|");
                }    
            }
        	Console.Write("\n|===|===|===|\n");
        }
		
        // Fonction permettant de changer
        // dans le tableau qu'elle est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int j, int k, int joueur)
        {
            if (j >= 0 && j < 3 && k >= 0 && k < 3) // vérifie si j et k sont compris entre 1 et 3 inclus
            {
            	if (grille[j,k] == 10) // vérifie si la case n'est prise par personne
            	{
            		grille[j,k]=joueur;
            		return true;
            	}
            }
            return false; 
        }

        // Fonction permettant de vérifier
        // si un joueur à gagner
        public static bool Gagner(int l, int c, int joueur) 
        {
            if (grille[l,0] == grille[l,1] && grille[l,1] == grille[l,2] && grille[l,0] != 10) // vérifie si toutes les case d'une ligne sont égale à soit X soit 0
            {
            	return true;
            }
            if (grille[0,c] == grille[1,c] && grille[1,c] == grille[2,c] && grille[0,c] != 10) // vérifie colonne
            {
            	return true;
            }
            if (((grille[0,0] == grille[1,1] && grille[1,1] == grille[2,2]) || (grille[2,0] == grille[1,1] && grille[1,1] == grille[0,2])) && grille[1,1] != 10) // vérifie diagonales
            {
            	return true;
            }
            return false; // si rien n'est vérifier on retourne false et le jeu continue
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations --
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre

            int essais = 0;    // compteur d'essais
	        int joueur = 1 ;   // 1 pour la premier joueur, 2 pour le second
	        int l, c = 0;      // numéro de ligne et de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 
            bool bonnePosition = false; // Permet de vérifier si la position souhaité est disponible

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10
	        for (j=0; j < grille.GetLength(0); j++)
		        for (k=0; k < grille.GetLength(1); k++)
			        grille[j,k] = 10;
					while(!gagner && essais != 9)
					{
						Console.Clear(); // on vide la console
						Console.WriteLine("c'est au tour du joueur "+joueur); // on écrit à qui c'est le tour de jouer
						AfficherMorpion(j,k);// affiche le morpion
						try
						{
							Console.WriteLine("Ligne   =    ");
							Console.WriteLine("Colonne =    ");
							// Peut changer en fonction de comment vous avez fait votre tableau.
							Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre 
							l = int.Parse(Console.ReadLine()) - 1; 
							// Peut changer en fonction de comment vous avez fait votre tableau.
							Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 10); // Permet de manipuler le curseur dans la fenêtre 
							c = int.Parse(Console.ReadLine()) - 1; 
							bonnePosition = AJouer(l,c,joueur); //vérifie si bonnePosition est à true ou false pour savoir si on peux jouer sur une case 
							gagner=Gagner(l,c,joueur); // gagner true ou false pour savoir si le jeux continue
							if (bonnePosition == true && gagner == false) // si la position est bonne et que personne n'as gagné
							{
								grille[l,c]=joueur;// on ajoute dans grille 1 ou 2 en fonction du joueur
							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e.ToString());
						}

						// Changement de joueur
						if (gagner==true) // si quelqu'un à gagner
						{
							break;// on sort de la boucle
						}
						if (bonnePosition == true && gagner == false)// si la position est bonne et que personne n'as gagner
						{
							essais = essais+1; // on rajoute un essaie
							// change de joueur
							if (joueur == 1 )
							{
								joueur=2;
							}
							else
							{
								joueur=1;
							}
						}
						else // si la case est déjà prise
						{
							Console.WriteLine("mauvais placement");
							Console.ReadKey(true); // permet que l'erreur s'affiche (sinon sa réaffiche juste le tableau sans marquer d'erreur)
						}
						
						

					}; // Fin TQ

            // Fin de la partie
            Console.Clear();
            AfficherMorpion(j,k);
            if ( gagner == false && essais == 9) // si personne n'as gagner mais qu'il n'y a plus d'essais alors égalité
            {
            	Console.WriteLine("égalité");
            }
            if (gagner == true) // si quelqu'un à gagner, on affiche le joueur gagnant
            {
            	Console.WriteLine("le gagnant est le joueur "+joueur);
            } 
            Console.ReadKey();
    }
  }
}