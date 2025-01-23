# Projet N-Tiers : Blaze 4

## Membres du groupe
- **Baptiste WETTERWALD**
- **Thomas KEPPLER**
- **(Gauthier CETINGOZ)**

## Architecture

Blaze4 est une application N-tiers structurée en quatre projets principaux :
1. **Puissance4.Application** : Gestion de la logique métier et API REST
2. **Puissance4.DataAccess** : Accès à la base de données via Entity Framework Core
3. **Puissance4.DTOs** : Modèles de données partagés entre l'API et le front-end
4. **Puissance4.Presentation** : Front-end en Blazor WASM

### Vue d'ensemble des projets

#### 1. Puissance4.Application

Ce projet gère la logique métier et expose des endpoints API REST pour la communication avec le front-end.

- **Controllers** :
    - `AuthController.cs` : Gestion de l'authentification (login, signup, token JWT).
    - `GamesController.cs` : Gestion des parties (création, récupération, jeu).

- **Domain** :
    - `Cell.cs`, `Game.cs`, `Grid.cs`, `Player.cs`, `Token.cs` : Modèles métier pour représenter les concepts du jeu Puissance4.

- **Mappers** :
    - `CellMapper.cs`, `GameMapper.cs`, `GridMapper.cs`, `PlayerMapper.cs` : Conversions entre modèles métier, entités EF Core et DTOs.

- **Services** :
    - `AuthService.cs` : Gère l'authentification et la gestion des utilisateurs.
    - `GameService.cs` : Contient la logique des parties (création, jeu, validation des tours).

- **Program.cs** :
    - Configure les services (ex. EF Core, répertoires, JWT).
    - Définit les routes et options de l’application.

**URLs de développement** :
- HTTPS : `https://localhost:7164`
- HTTP : `http://localhost:5272`

#### 2. Puissance4.DataAccess

Ce projet gère l’accès à la base de données SQLite via Entity Framework Core.

- **Entities** :
    - `EFCell.cs`, `EFGame.cs`, `EFGrid.cs`, `EFPlayer.cs` : Entités représentant les tables de la base de données.

- **Repositories** :
    - **Implementations** :
        - `CellRepository.cs`, `GameRepository.cs`, `GridRepository.cs`, `PlayerRepository.cs`, `Repository.cs` (générique).
    - **Interfaces** :
        - `ICellRepository.cs`, `IGameRepository.cs`, `IGridRepository.cs`, `IPlayerRepository.cs`, `IRepository.cs` (générique).

- **Migrations** :
    - Contient les migrations EF Core pour gérer la structure de la base de données.

- **Autres composants** :
    - `DataAccessConfiguration.cs` : Configure les repositories et la connexion à la DB.
    - `DbInitializer.cs` : Initialise les utilisateurs dans la DB avec des comptes prédéfinis :
        - `Baptouste` / `#qlflop`
        - `Mehmett` / `ChefMehmett`
        - `Kepplouf` / `Thomsoja`

- **Base de données** :
    - SQLite : `puissance4.db`.

**Composants EF Core** :
- `Puissance4DbContext.cs` : Context de la base de données.
- `Puissance4DbContextFactory.cs` : Factory pour générer le DbContext.

#### 3. Puissance4.DTOs

Ce projet contient les DTOs (Data Transfer Objects), qui filtrent et standardisent les données transmises entre l’API et le front.

- **DTOs disponibles** :
    - `BearerTokenDto.cs` : DTO pour le token JWT.
    - `CellDto.cs`, `GameDto.cs`, `GridDto.cs`, `LoginDto.cs`, `PlayerDto.cs`, `PlayTurnDto.cs` : Modèles utilisés pour les différentes entités et actions.

#### 4. Puissance4.Presentation

Ce projet gère l’interface utilisateur via Blazor WASM. Il communique avec l’API pour fournir une expérience interactive.

- **Layouts** :
    - `MainLayout.razor`, `NavMenu.razor` : Layout principal et menu de navigation.

- **Pages** :
    - `Game.razor` : Vue d’une partie.
    - `Games.razor` : Liste des parties disponibles.
    - `Login.razor`, `Logout.razor`, `Signup.razor` : Pages d’authentification.

- **Composants** :
    - `App.razor` : Point d’entrée de l’application.
    - `GameBoard.razor` : Représentation graphique du plateau.
    - `Modal.razor`, `RedirectToLogin.cs` : Gestion des modales et des redirections.

- **Services** :
    - `AuthenticatedHttpClientHandler.cs` : Gestion des requêtes avec token JWT.
    - `AuthService.cs`, `GameService.cs`, `PlayerService.cs` : Services pour l’interaction avec l’API.
    - `JwtAuthenticationStateProvider.cs` : Gestion de l’état d’authentification.

**URLs de développement** :
- HTTPS : `https://localhost:6969`
- HTTP : `http://localhost:5067`

### Tests

Tous les tests ont été réalisés en HTTPS, avec les configurations suivantes :
- API : `https://localhost:7164`
- Front : `https://localhost:6969`

---

## Instructions pour exécuter le projet


1. **Base de données** :
    - Assurez-vous que `puissance4.db` existe dans le projet DataAccess.
    - Pour générer la DB :
      ```bash
      dotnet ef database update --project Puissance4.DataAccess
      ```

2. **Lancez l’API** :
   ```bash
   cd Puissance4.Application
   dotnet run
   ```

3. **Lancez le front-end** :
   ```bash
   cd Puissance4.Presentation
   dotnet run
   ```

4. **Ouvrez votre navigateur** :
    - Front : `https://localhost:6969`
    - API : `https://localhost:7164`

**Si besoin, supprimez la BDD puis recréez-la** :
```bash
rm Puissance4.DataAccess/puissance4.db
dotnet ef database update --project Puissance4.DataAccess
```

## Fonctionnalités principales

1. **Authentification** :
    - Créez un compte ou connectez-vous avec un des comptes prédéfinis.
2. **Liste des parties** :
    - Consultez les parties disponibles ou créez-en une nouvelle.
3. **Jeu en ligne** :
    - Jouez en temps réel avec validation des tours et gestion des victoires.

## Remarque

Le front n'étant pas la priorité du projet, certaines fonctionnalités ne sont pas très ergonomiques, ex : la grille ne s'actualise pas automatiquement après un coup adverse. Il est nécessaire de rafraîchir la page pour voir les changements.
L'accent a été mis sur la logique métier et la communication entre les différents projets.