// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Builder.AI.QnA.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
namespace Microsoft.BotBuilderSamples
{
    public class BotServices : IBotServices
    {
        public BotServices(IConfiguration configuration)
        {
            Debug.WriteLine("Initializing Bot Services " ) ;

            InitializeService(configuration);
            Debug.WriteLine("Initializing Bot Services Done" ) ;
        }

        public IQnAMakerClient QnAMakerService { get; private set; }

        private void InitializeService(IConfiguration configuration)
        {
            var QnAEndpointHostName = configuration["QnAEndpointHostName"];
            var QnAEndpointKey = configuration["QnAEndpointKey"];
            var QnAKnowledgebaseId = configuration["QnAKnowledgebaseId"];

            var ProjectName = configuration["ProjectName"];
            var LanguageEndpointKey = configuration["LanguageEndpointKey"];
            var LanguageEndpointHostName = configuration["LanguageEndpointHostName"];
            if (!String.IsNullOrEmpty(LanguageEndpointHostName) && !String.IsNullOrEmpty(LanguageEndpointKey) && !String.IsNullOrEmpty(ProjectName))
            {
               Debug.WriteLine("Initializing CustomQuestionAnswering maker  Services " ) ;
                QnAMakerService = new CustomQuestionAnswering(new QnAMakerEndpoint
                {
                    KnowledgeBaseId = ProjectName,
                    Host = LanguageEndpointHostName,
                    EndpointKey = LanguageEndpointKey,
                    QnAServiceType = ServiceType.Language
                });
             Debug.WriteLine("Initializing CustomQuestionAnswering maker  Services Done" ) ;

            }
            else if (!String.IsNullOrEmpty(QnAEndpointHostName) && !String.IsNullOrEmpty(QnAEndpointKey) && !String.IsNullOrEmpty(QnAKnowledgebaseId))
            {
                               Debug.WriteLine("Initializing Simple QA service  maker  Services " ) ;

                QnAMakerService = new QnAMaker(new QnAMakerEndpoint
                {
                    KnowledgeBaseId = QnAKnowledgebaseId,
                    Host = QnAEndpointHostName,
                    EndpointKey = QnAEndpointKey,
                    QnAServiceType = ServiceType.QnAMaker
                });
                             Debug.WriteLine("Initializing Simple QA service  maker  Services Done" ) ;

            }
            else
            {
                 Debug.WriteLine("Error in Configuration " ) ;

                throw new ArgumentException("Please fill in the configuration parameters.");
            }
        }
    }
}
