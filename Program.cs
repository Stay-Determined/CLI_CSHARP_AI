﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using OpenAI_API;

string resultat = "";


string message = "";
string langue = "";
string fonctionApi = "";
 void menu()
{
    Console.Clear();
    Console.WriteLine("Bienvenue, que souhaitez-vous faire :\n");
    Console.WriteLine("1 - Traduction de texte");
    Console.WriteLine("2 - Détection et correction d'erreur de texte");
    Console.WriteLine("3 - Quitter");
    sousMenuPrincipal();
}

//Affiche d'un décompte et arrêt du programme
void stopProgram()
{
    Console.Clear();
    for (int i = 3; i > 0; i--)
    {
        Console.WriteLine("Le programme va s'arrêter dans " + i + " secondes");
        Thread.Sleep(1000);
        Console.Clear();
    }
    Console.WriteLine("Bonne journée et à bientôt");
    Environment.Exit(0);
}


// Menu principal du choix de l'outil
void sousMenuPrincipal()
{

    string var1 = Console.ReadLine();
    switch (var1)
    {
        case "1":
            sousMenuPartTraduction();
            break;
        case "2":
            sousMenuPartCorrection();   
            break;
        case "3":
            stopProgram();
            break;
        default:
            Console.WriteLine("Veuillez choisir une des options existante");
            tempo();
            Console.Clear();
            menu();
            break;

    }
}

// Menu principal --> choix d'un outil --> utilisation de l'outil --> (Ici) Interface de choix après utilisation 
void sousMenuFin()
{
    Console.Clear();
    Console.WriteLine("\n\nVoulez-vous réaliser une autre action ?\n");
    Console.WriteLine("1 - Oui --> Menu principal");
    Console.WriteLine("2 - Oui --> Traduire un texte");
    Console.WriteLine("3 - Oui --> Corriger un texte");
    Console.WriteLine("4 - Non --> Arrêt de l'interface");
    string var1 = Console.ReadLine();
    switch (var1)
    {
        case "1":
            menu();
            break;
        case "2":
            sousMenuPartTraduction();
            break;
        case "3":
            sousMenuPartCorrection();
            break;
        case "4":
            stopProgram();
            break;
        default:
            Console.WriteLine("Veuillez choisir une des options existante");
            tempo();
            Thread.Sleep(3000);
            menu();
            break;
    }
}

// Menu principal --> choix d'un outil -->(Ici) utilisation de l'outil pour la correction orthographique de texte
void sousMenuPartCorrection()
{
    Console.Clear();
    fonctionApi = "correction";
    Console.WriteLine("Ecrivez un texte à corriger :");
    message = Console.ReadLine();
    apiOpenAI(message, fonctionApi);
    tempo();
    Console.WriteLine("Continuer ?\n");
    Console.WriteLine("1 - OUI");
    Console.WriteLine("2 - NON");
    string choix = Console.ReadLine();
    switch (choix)
    {
        case "1":
            sousMenuFin();
            break;
        case "2":
            stopProgram();
            break;
    }
}


// Menu principal --> choix d'un outil -->(Ici) utilisation de l'outil pour la traduction de texte
void sousMenuPartTraduction()
{
    Console.Clear();
    fonctionApi = "traduction";
    Console.WriteLine("Ecrivez une phrase à traduire :");
    message = Console.ReadLine();
    sousMenuPartTraduction2();
}


// Menu principal --> choix d'un outil --> utilisation de l'outil pour la traduction de texte --> Choix de la langue 
void sousMenuPartTraduction2()
{
    Console.WriteLine("\nEn quelle langue voulez-vous traduire ce texte ?");
    Console.WriteLine("1 - Français\n2 - Anglais\n3 - Allemand\n4 - Plus de langues...\n5 - Retour");
    langue = Console.ReadLine();

    switch (langue)
    {
        case "1":
            langue = "Français";
            Console.WriteLine("Vous me dites que vous voulez traduire un texte français en français ?\nVous n'êtes pas le couteau le plus aïguisé du tiroir...");
            tempo();
            sousMenuPartTraduction2();
            sousMenuFin();
            break;
        case "2":
            langue = "Anglais";
            apiOpenAI(message, fonctionApi, langue);
            tempo();
            sousMenuFin();
            break;
        case "3":
            langue = "Allemand";
            apiOpenAI(message, fonctionApi, langue);
            tempo();
            sousMenuFin();
            break;
        case "4":
            extensionInterfaceTraduction();
            break;
        case "5":
            menu();
            break;
        default:
            Console.WriteLine("Choisissez une des valeurs proposées");
            tempo();
            sousMenuPartTraduction2();
            break;

    }
}


// Menu principal --> choix d'un outil --> utilisation de l'outil pour la traduction de texte --> Choix de la langue / (Ici) autres choix de langues parmis plusieurs
void extensionInterfaceTraduction()
{
    Console.WriteLine("\n1 - Russe\n2 - Espagnol\n3 - Portugais\n4 - Italien\n5 - Plus ...\n0 - Retour\n");
    langue = Console.ReadLine();

    switch (langue)
    {
        case "1":
            langue = "Russe";
            apiOpenAI(message, fonctionApi, langue);
            sousMenuFin();
            break;
        case "2":
            langue = "Espagnol";
            apiOpenAI(message, fonctionApi, langue);
            sousMenuFin();
            break;
        case "3":
            langue = "Portugais";
            apiOpenAI(message, fonctionApi, langue);
            sousMenuFin();
            break;
        case "4":
            langue = "Italien";
            apiOpenAI(message, fonctionApi, langue);
            sousMenuFin();
            break;
        case "5":
            Console.WriteLine("Je ne peux pas faire mieux :\nhttps://translate.google.fr\n\n");
            Console.WriteLine("0 - Retour\n");
            langue = "";
            Console.ReadLine();
            string retour = "";
            switch (retour)
            {
                case "0":
                    extensionInterfaceTraduction();
                    break;
            }
            break;
        case "0":
            sousMenuPartTraduction2();
            break;
        default:
            Console.WriteLine("Choisissez une des valeurs proposées");
            tempo();
            extensionInterfaceTraduction();
            break;
    }
}


// Menu principal --> choix d'un outil --> utilisation de l'outil --> (Ici)  
async void apiOpenAI(string texte, string fonction, string langue = "F  rançais")
{
    Console.Clear();
    string apiKey = "API_KEY";
    string apiModel = "text-davinci-003";
        List<string> reponses = new List<string>();

    switch (fonction)
    {
        case "traduction":
            fonction = "Traduis ce message en " + langue + " : " + texte;
            break;
        case "correction":
            fonction = "Retourne moi le texte suivant corrigé et surtout fais bien attention à ne pas remplacer un mot, fais uniquement la correction grammaticale et orthagraphique: " + texte + ".";
            break;
        default:
            Console.WriteLine("Erreur lors du choix du type de travail demandé");
            menu();
            break;
    }

    OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));

    var completionRequest = new OpenAI_API.Completions.CompletionRequest()
    {
        Prompt = fonction,
        Model = apiModel,
        Temperature = 0,
        MaxTokens = 100,
        TopP = 0.1,
        FrequencyPenalty = 0.0,
        PresencePenalty = 0.0,

    };


    //Fonction de création de requête de complétion (qui va permettre de réaliser nos deux types d'actions)
    var result = await api.Completions.CreateCompletionsAsync(completionRequest);
    foreach (var choice in result.Completions)
    {
        reponses.Add(choice.Text);
    }

    Console.OutputEncoding = Encoding.UTF8;
    Console.WriteLine(reponses.FirstOrDefault());
}


void tempo()
{
    List<string> monTableau = new List<string>();
    string actual = "=" ;

    for (int i = 0; i<30; i++)
    { 
            monTableau.Add("=");
    }

    
    foreach(var a in monTableau)
    {
        Console.Write(a);
        Thread.Sleep(100);
    }
    Console.Write(">");

}
menu();


