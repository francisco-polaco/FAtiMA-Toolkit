﻿using System;
using System.Linq;
using AssetManagerPackage;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using GAIPS.Rage;

namespace EmotionalAppraisalTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Emotional Appraisal Asset
        static void Main(string[] args)
        {
			AssetManager.Instance.Bridge = new BasicIOBridge();
			var kickEvent = "Event(Action-Start, Player, Kick, John)";

            // To create a new asset it is required to tell the name of the agent which will correpond to the perspective of the "SELF" 
            EmotionalAppraisalAsset ea = new EmotionalAppraisalAsset("John");

            //The following lines add an appraisal rule that will make the kickEvent be perceived as undesirable
            //Normally, these rules should be authored using the AuthoringTool provided with the asset but they can also be added dynamically
            var rule = new AppraisalRuleDTO { EventMatchingTemplate = "Event(Action-Start, *, Kick, SELF)", Desirability = -5f, Praiseworthiness = -3f };
            ea.AddOrUpdateAppraisalRule(rule);

            //Emotions are generated by the appraisal of the events that occur in the game world 
            ea.AppraiseEvents(new[] { kickEvent });           
            Console.WriteLine("\nAfter appraising  '" + kickEvent + "'\nWith the appraisal rule '" + rule.EventMatchingTemplate + " Desirability: "+rule.Desirability+ " Praiseworthiness: " + rule.Praiseworthiness +  "'");
            Console.WriteLine("\nMood on tick '" + ea.Tick + "': " + ea.Mood);
            Console.WriteLine("Active Emotions: " + string.Concat(ea.ActiveEmotions.Select(e => e.Type + "-" + e.Intensity + " ")));

            //Each event that is appraised will be stored an the agent's autobiographical memory
            Console.WriteLine("\nEvents occured so far: " + string.Concat(ea.EventRecords.Select(e => "\nId: " + e.Id + " Event: " + e.Event)));
            
            //The update function will increase the current tick by 1. Each active emotion will decay to 0 and the mood will slowly return to 0
            for (int i = 0; i < 3; i++)
            {
                ea.Update();
                Console.WriteLine("\nMood on tick '" + ea.Tick + "': " + ea.Mood);
                Console.WriteLine("Active Emotions: " + string.Concat(ea.GetAllActiveEmotions().Select(e => e.EmotionType + "-" + e.Intensity + " ")));
            }
            
            //The asset can also be loaded from an existing file using the following method:
            ea = EmotionalAppraisalAsset.LoadFromFile("../../../Examples/EATest.ea");
            
            Console.ReadKey();
        }
    }
}

