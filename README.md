# BadgeUp .NET Client
Official .NET client for working with [BadgeUp](https://www.badgeup.io/). Targets compatibility with .NET 4.6 and .NET Core v1.1 (LTS).

[![Build Status](https://travis-ci.org/BadgeUp/badgeup-dotnet-core-client.svg?branch=master)](https://travis-ci.org/BadgeUp/badgeup-dotnet-core-client)

## Quickstart

## Initialization
The BadgeUp .NET client is initialized with an API key which can be generated in the BadgeUp dashboard.
```cs
using BadgeUpClient.Types;
using BadgeUpClient.Responses;

// instantiate the client
var badgeup = new BadgeUpClient('<api key here>');

// send an event
EventResponse response = badgeup.Event.Send(new Event( "some_user", "jump", new Modifier { Inc = 1 } ));

// loop through all the progress results
foreach (var prog in response.Progress)
{
    // check if this is a newly-earned achievement
    if (prog.IsComplete && prog.IsNew)
    {
        string earnedAchievementId = prog.EarnedAchievementId;
        string achievementId = prog.AchievementId;
        System.Console.WriteLine($"Achievement with ID {prog.AchievementId} Earned!");

        // from here you can use AchievementId and EarnedAchievementId to get the original achievement and awards objects
        var earnedAchievement = await client.EarnedAchievement.GetById(earnedAchievementId);
        var achievement = await client.Achievement.GetById(achievementId);

        // get associated award information
        foreach (var awardId in achievement.Awards)
        {
            var award = await client.Award.GetById(awardId);
            // in the dashboard set the award to `{ "points": 5 }`
            int points = award.Data["points"].ToObject<int>();
            System.Console.WriteLine($"Points awarded: {points}");
        }
    }
}
```

## Development
```sh
dotnet restore
```

## Testing
```sh
dotnet test
```
