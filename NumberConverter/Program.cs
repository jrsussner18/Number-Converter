// Jake Sussner
// 9/25/24
// CSC - 210 Program 001 -> Number Converter

// inport namespaces
using System;
using System.IO;
using NumberConverter;

class Program
{
    static void Main()
    {
        // initialize variables for input and output text files
        string inputFile = "input.txt";
        string outputFile = "output.txt";

        // use the StreamReader and SteamWriter classes to read and write files
        StreamReader readFromFile = new StreamReader(inputFile);
        StreamWriter writer = new StreamWriter(outputFile);

        string line;

        // reading the input file line by line
        while ((line = readFromFile.ReadLine()) != null)
        {
            string[] data = line.Split(" ");
            // storing data
            long num = long.Parse(data[0]);
            long currBase = long.Parse(data[1]);
            long nextBase = long.Parse(data[2]);
            long numBits = long.Parse(data[3]);

            // putting stored data into new number object
            Number number = new Number(num, currBase, nextBase, numBits);
            
            // declare and convert to final number using Number class methods
            string finalNum;
            if (currBase == 10)
            {
                finalNum = number.fromBaseTen(num);
            }
            else if (nextBase == 10)
            {
                finalNum = (number.toBaseTen(true)).ToString();
            }
            else
            {
                finalNum = number.convertToOtherBase();
            }
            // write teh final number to output text file
            writer.WriteLine(finalNum);
        }
        // close the reader and writer
        readFromFile.Close();
        writer.Close();
    }
    
}