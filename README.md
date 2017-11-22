# BadgeUp .NET Core Client
Official .NET Core client for working with [BadgeUp](https://www.badgeup.io/). Targets compatibility with .NET Core v1 (LTS).

[![Build Status](https://travis-ci.org/BadgeUp/badgeup-dotnet-core-client.svg?branch=master)](https://travis-ci.org/BadgeUp/badgeup-dotnet-core-client)

## Quickstart

## Initialization
The BadgeUp .NET Core client is initialized with an API key.
```js
var badgeup = new BadgeUp('<api key here>');
badgeup.SendEvent(new BadgeUp.Types.Event( "some_user", "jump", new BadgeUp.Types.Modifier { Inc = 1 } ));
```

## Development
```sh
dotnet restore
```

## Testing
```sh
dotnet test
```
