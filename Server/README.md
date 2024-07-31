# Documentation de l'API pour Rail's Mada

## Informations Générales

- **Version de .NET** : 8.0.303
- **Gestion de base de données** : SQLite
- **Auteur** : [Tahiry](https://github.com/RazTahiry)

## Introduction

Ce projet fournit une API REST pour la gestion des données de Rail's Mada. Il inclut des endpoints pour les entités suivantes :
- **Categorie**
- **Itineraire**
- **Train**
- **Voyageur**

## Prérequis

Avant de commencer, assurez-vous d'avoir les éléments suivants installés sur votre machine :
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)

## Lancer le Serveur Backend

Pour lancer le serveur backend, exécutez les commandes suivantes depuis le répertoire du serveur :

1. **Construire le projet** :
    ```bash
    dotnet build
    ```

2. **Mettre à jour la base de données** :
    ```bash
    dotnet ef database update
    ```

3. **Démarrer le serveur** :
    ```bash
    dotnet watch run
    ```

## Endpoints de l'API

### Categorie

- **GET** `/api/v1/categorie`
  - Récupère toutes les catégories.

- **POST** `/api/v1/categorie`
  - Crée une nouvelle catégorie.
  - **Corps de la requête** : `Categorie`

- **GET** `/api/v1/categorie/{CodeCategorie}`
  - Récupère une catégorie par son code.

- **DELETE** `/api/v1/categorie/{CodeCategorie}`
  - Supprime une catégorie par son code.

- **PUT** `/api/v1/categorie/{CodeCategorie}`
  - Met à jour une catégorie existante.
  - **Corps de la requête** : `Categorie`

### Itineraire

- **GET** `/api/v1/itineraire`
  - Récupère tous les itinéraires.

- **POST** `/api/v1/itineraire`
  - Crée un nouvel itinéraire.
  - **Corps de la requête** : `Itineraire`

- **GET** `/api/v1/itineraire/{CodeItineraire}`
  - Récupère un itinéraire par son code.

- **DELETE** `/api/v1/itineraire/{CodeItineraire}`
  - Supprime un itinéraire par son code.

- **PUT** `/api/v1/itineraire/{CodeItineraire}`
  - Met à jour un itinéraire existant.
  - **Corps de la requête** : `Itineraire`

### Train

- **GET** `/api/v1/train`
  - Récupère tous les trains.

- **POST** `/api/v1/train`
  - Crée un nouveau train.
  - **Corps de la requête** : `Train`

- **GET** `/api/v1/train/{Immatriculation}`
  - Récupère un train par son immatriculation.

- **DELETE** `/api/v1/train/{Immatriculation}`
  - Supprime un train par son immatriculation.

- **PUT** `/api/v1/train/{Immatriculation}`
  - Met à jour un train existant.
  - **Corps de la requête** : `Train`

### Voyageur

- **GET** `/api/v1/voyageur`
  - Récupère tous les voyageurs.

- **POST** `/api/v1/voyageur`
  - Crée un nouveau voyageur.
  - **Corps de la requête** : `Voyageur`

- **GET** `/api/v1/voyageur/{numTicket}`
  - Récupère un voyageur par son numéro de ticket.

- **DELETE** `/api/v1/voyageur/{numTicket}`
  - Supprime un voyageur par son numéro de ticket.

- **PUT** `/api/v1/voyageur/{numTicket}`
  - Met à jour un voyageur existant.
  - **Corps de la requête** : `Voyageur`

## Structure de la Base de Données

Les entités de la base de données sont définies comme suit :
- **Categorie** : Représente les catégories de train.
- **Itineraire** : Représente les itinéraires des trains.
- **Train** : Représente les trains, associés à des itinéraires.
- **Voyageur** : Représente les voyageurs, associés à des catégories.

