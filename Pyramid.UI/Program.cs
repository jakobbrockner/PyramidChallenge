using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Pyramid.Business.Classes;
using Pyramid.Core.Interfaces;
using Pyramid.Data.HelperClasses;


namespace Pyramid.UI
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            //
            //Get appSetting for filePath and number of children, and should Odd Even rules be enforced. If not found, set defaults
            //
            var inputFilePath = config["inputFilePath"];
            var foundChildNodeSetting = int.TryParse(config["numberOfChildNodes"], out var numberOfChildNodes);
            if (!foundChildNodeSetting)
            {
                numberOfChildNodes = 2;
            }
            var foundForceOddEvenSetting = bool.TryParse(config["ForceOddEvenRule"], out var forceOddEvenRule);
            if (!foundForceOddEvenSetting)
            {
                forceOddEvenRule = true;
            }
            //
            // Use value generator.
            //
            var inputReader = new ValueGenerator();
            CreatePyramidAndDisplayPath(inputReader, numberOfChildNodes, forceOddEvenRule);

        }

        private static void CreatePyramidAndDisplayPath(IValuesReader valueReader, int numberOfChildNodes, bool forceOddEvenRule)
        {
            var pyramid = new PyramidGraph(valueReader, numberOfChildNodes, forceOddEvenRule);
            pyramid.Build();
            pyramid.CreatePath();
            DisplayPath(pyramid.PyramidPathItems, pyramid.Max);
        }

        private static void DisplayPath(List<int> pyramidPathItems, int maxValue)
        {
            var displayPath = "";
            var arrowDisplay = "";
            foreach (var value in pyramidPathItems)
            {
                displayPath = string.Join(arrowDisplay, displayPath, value);
                arrowDisplay = " -> ";
            }
            Console.Write(displayPath);
            Console.WriteLine();
            Console.WriteLine($"Max: {maxValue}");
            Console.WriteLine("Press a key to end");
            Console.ReadKey();
        }


       
    }
}
