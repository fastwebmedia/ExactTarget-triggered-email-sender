﻿using System;
using ExactTarget.TriggeredEmail.Creation;
using ExactTarget.TriggeredEmail.Trigger;
using NUnit.Framework;

namespace ExactTarget.TriggeredEmail.Test.Integration
{
    public class DynamicTemplatedTriggeredEmailTests : TestBase
    {
        [Test]
        public void CreateAndSend()
        {
            var externalKey = Guid.NewGuid().ToString();
            Create(externalKey);
            Send(externalKey);
        }

        private void Create(string externalKey)
        {
            var triggeredEmailCreator = new TriggeredEmailCreator(Config);

            Assert.DoesNotThrow(() => triggeredEmailCreator.Create(
                                        externalKey,
                                        "<html>" +
                                        "<head>" +
                                        "<style>.green{color:green}</style>" +
                                        "</head>" +
                                        "<body>Hello %%FirstName%%,   " +
                                        "<p class='green'>Green Content: %%MyOwnValue%% ...</p>" +
                                        "<body>" +
                                        "<html>",
                                        Priority.High),
                                    "Failed to create Triggered Email");

            Assert.DoesNotThrow(() => triggeredEmailCreator.StartTriggeredSend(externalKey), "Failed to start Triggered Send");
        }

        private void Send(string externalKey)
        {
            var triggeredEmail = new ExactTargetTriggeredEmail(externalKey, TestRecipientEmail);
            triggeredEmail.AddReplacementValue("Subject", "Test email");
            triggeredEmail.AddReplacementValue("FirstName", "John");
            triggeredEmail.AddReplacementValue("MyOwnValue", "Some test copy here...");

            var emailTrigger = new EmailTrigger(Config);
            
            Assert.DoesNotThrow( () =>  emailTrigger.Trigger(triggeredEmail), "Failed to send email");
        }

    }
}
