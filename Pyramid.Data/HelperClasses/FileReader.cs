using System;
using System.Collections.Generic;
using System.IO;
using Pyramid.Core.Interfaces;

namespace Pyramid.Data.HelperClasses
{
    public class InputFileReader : IValuesReader
    {
        private readonly string inputFilePath;
        private readonly List<int[]> pyramidValues = new List<int[]>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputFilePath">Full path to input file</param>
        public InputFileReader(string inputFilePath)
        {
            this.inputFilePath = inputFilePath;
        }

        public List<int[]> ReturnValues()
        {
            if (string.IsNullOrEmpty(this.inputFilePath))
            {
                Console.WriteLine("Please supply filepath to input file in appsettings.json");
            }

            if (!File.Exists(this.inputFilePath))
            {
                throw new FileNotFoundException($"Please supply a valid path in appSettings.json. {this.inputFilePath} does not exist");
            }

            FileStream fileStream = new FileStream(this.inputFilePath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    var values = reader.ReadLine().TrimEnd().TrimStart().Split(" ");
                    int[] intValues = Array.ConvertAll(values, int.Parse );
                    this.pyramidValues.Add(intValues);
                }
            }

            return this.pyramidValues;
        }
    }
}
