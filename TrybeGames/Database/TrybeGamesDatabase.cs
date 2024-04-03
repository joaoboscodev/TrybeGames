namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        var gamesByStudio = 
            from game in Games
            where game.DeveloperStudio == gameStudio.Id
            select game;
        return gamesByStudio.ToList();  
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        var gamesPlayedByPlayer = 
            from game in Games
            where game.Players.Contains(player.Id)
            select game;
        return gamesPlayedByPlayer.ToList();  
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        var gamesPurchasedByPlayer = 
            from game in Games
            where playerEntry.GamesOwned.Contains(game.Id)
            select game;
        return gamesPurchasedByPlayer.ToList();  
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        var gamesAndStudio =
            from game in Games
            join studio in GameStudios on game.DeveloperStudio equals studio.Id
            select new GameWithStudio {
                GameName = game.Name,
                StudioName = studio.Name,
                NumberOfPlayers = game.Players.Count
            };
        return gamesAndStudio.ToList();
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        var registeredGameTypes =
            from game in Games
            select game.GameType;
        return registeredGameTypes.Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        var studioGamesAndPlayers =
            from studio in GameStudios
            select new StudioGamesPlayers {
                GameStudioName = studio.Name,
                Games = (
                    from game in Games
                    where game.DeveloperStudio == studio.Id
                    select new GamePlayer {
                        GameName = game.Name,
                        Players = (
                            from player in Players
                            from players in game.Players
                            where players == player.Id
                            select player).ToList()
                    }
                ).ToList()
            };
            return studioGamesAndPlayers.ToList();
    }

}
