// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Linq;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SlackBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        public QnAMaker EchoBotQnA { get; private set; }

        public EchoBot(QnAMakerEndpoint endpoint)
        {
            // connects to QnA Maker endpoint for each turn
            EchoBotQnA = new QnAMaker(endpoint);
        }

        //Possible welcome names
        private string[] names = { "Euler", "Caio", "Bernardo", "Jefersson", "Daniel", "Edu", "Bot" };

        //Endpoint that listens the incoming messages
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await AccessQnAMaker(turnContext, cancellationToken);
            //await turnContext.SendActivityAsync(MessageFactory.Text(turnContext.Activity.Text), cancellationToken);
        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Olá! :D"), cancellationToken);
                }
            }
        }
        private async Task AccessQnAMaker(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var results = await EchoBotQnA.GetAnswersAsync(turnContext);
            if (results.Any())
            {
                if (turnContext.Activity.Text.Equals("/start") || turnContext.Activity.Text.ToLower().Equals("ola") || turnContext.Activity.Text.ToLower().Equals("olá")) await turnContext.SendActivityAsync(MessageFactory.Text($"Olá! Eu sou o {names[new Random().Next(names.Length - 1)]}:D"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text(results.First().Answer), cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Desculpe, Não consegui entender sua pergunta :/"), cancellationToken);
                turnContext.Activity.Text = "Default";
                await AccessQnAMaker(turnContext, cancellationToken);
            }
        }
    }
}
