# SlackBot

Bot Framework v4 used to for FAQ integrated with Telegram

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 2.1

  ```bash
  # determine dotnet version
  dotnet --version
  ```

## To try this sample

- In a terminal, navigate to `SlackBot`

    ```bash
    # change into project folder
    cd # SlackBot
    ```

- Run the bot from Visual Studio.

  
  - Launch Visual Studio
  - File -> Open -> Project/Solution
  - Navigate to `SlackBot` folder
  - Select `SlackBot.csproj` file
  - Press `F5` to run the project

## Testing the bot using Bot Framework Emulator

[Bot Framework Emulator](https://github.com/microsoft/botframework-emulator).

- Install the Bot Framework Emulator version 4.5.0 or greater from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to the bot using Bot Framework Emulator

- Launch Bot Framework Emulator
- File -> Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages`