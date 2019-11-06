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

namespace SlackBot.Bots
{
    public class EchoBot : ActivityHandler
    {

        private string[] names = { "João", "Felipe", "Bernardo", "Caio", "Jucemar", "Eduardo", "Lucas", "Paulo" };
        private Random curr = new Random();

        public QnAMaker EchoBotQnA { get; private set; }
        public EchoBot(QnAMakerEndpoint endpoint)
        {
            // connects to QnA Maker endpoint for each turn
            EchoBotQnA = new QnAMaker(endpoint);
        }


        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await AccessQnAMaker(turnContext, cancellationToken);
            //await turnContext.SendActivityAsync(MessageFactory.Text(turnContext.Activity.Text), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            int max = names.Length - 1;
            await turnContext.SendActivityAsync(MessageFactory.Text($@"Seja bem vindo a SmartBot eu sou o {names[curr.Next(max)]}!"), cancellationToken);
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            string messaeText = string.IsNullOrEmpty(((TurnContext)turnContext).Activity.Text) ? "Default" : ((TurnContext)turnContext).Activity.Text;
        }

        private async Task AccessQnAMaker(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            turnContext.Activity.Text = turnContext.Activity.Text.ToLower();

            var results = await EchoBotQnA.GetAnswersAsync(turnContext);
            if (results.Any()) await turnContext.SendActivityAsync(MessageFactory.Text(results.First().Answer), cancellationToken);
            else await turnContext.SendActivityAsync(MessageFactory.Text("Desculpe, Não consegui entender sua pergunta :/"), cancellationToken);
        }
    }
}
