# BadgeUp .NET Client
Official .NET client for working with [BadgeUp](https://www.badgeup.io/). Targets compatibility with .NET 4.6 and .NET Core v1.1 (LTS).

[![Build Status](https://travis-ci.org/BadgeUp/badgeup-dotnet-client.svg?branch=master)](https://travis-ci.org/BadgeUp/badgeup-dotnet-client)
[![Build status](https://ci.appveyor.com/api/projects/status/ayanietgkkcvjjk8?svg=true)](https://ci.appveyor.com/project/MarkHerhold/badgeup-dotnet-client)

## Quickstart

### Install
Install the package [from nuget here](https://www.nuget.org/packages/BadgeUpClient/).

### Example Use
The BadgeUp .NET client is initialized with an API key which can be generated in the BadgeUp dashboard.
```cs
using BadgeUpClient.Types;
using BadgeUpClient.Responses;

// instantiate the client
var badgeup = new BadgeUpClient("<api key here>");

// create an event
var badgeupEvent = new Event("some_user", "jump", new Modifier { Inc = 1 });

// optionally provide a DateTimeOffset for when the event occurred
badgeupEvent.Timestamp = DateTimeOffset.Parse("2017-01-01T18:00:00+05:30");

//send an event
EventResponse response = await badgeup.Event.Send(badgeupEvent);

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

        // you can get pre-existing specific custom metadata fields for this achievement
        string fieldValueAsString = achievement.Meta.GetCustomField<string>("my_custom_field_name");
        int fieldValueAsInteger = achievement.Meta.GetCustomField<int>("some_age_field");
        MyClass fieldValueAsSpecificType = achievement.Meta.GetCustomField<MyClass>("custom_field_name");
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

## Release Instructions
```sh
dotnet build --configuration Release
dotnet pack --output nupkgs --include-symbols --include-source --configuration Release
dotnet nuget push .\BadgeUpClient\nupkgs\BadgeUpClient.0.0.1.nupkg --api-key <key> --source https://api.nuget.org/v3/index.json
```
