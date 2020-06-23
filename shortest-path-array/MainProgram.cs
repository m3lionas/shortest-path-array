using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace shortest_path_array
{
    public class MainProgram
    {
        public static Dictionary<string, ValueTuple<string, int>> calculatedArrays = new Dictionary<string, ValueTuple<string, int>>();

        /*
         * Main algorithm to solve the problem.
         * The idea is to take the last element of the array and
         * search for an element that is the furthest on the left
         * and can jump to it. Then we take that element and repeat
         * what we did for the last element of the array until we
         * reach the start of the array.
        */
        public static (string solution, int steps) FindBestPath(int[] arr)
        {
            if (arr.Length < 1) //array consists of one element
                return ("-", 0);

            if (arr[0] < 1)     //first element of array is 0 or less
                return ("-", -1);

            int position = arr.Length - 1; //position of the targeted element
            int prevPosition;
            int steps = 0;
            string result = arr[position].ToString();

            while (position != 0)
            {
                prevPosition = position;
                for (int i = 0; i < position; i++)
                {
                    if (arr[i] >= position - i) //if element arr[i] can jump to target element position
                    {
                        position = i; //now arr[i] will become the target element position
                        steps++; //we took a step

                        result = result.Insert(0, arr[i].ToString() + " ");
                    }
                }
                if (prevPosition == position) return ("-", -1); //we couldn't find an element that could jump to target
            }
            return (result, steps);
        }

        public static void ProcessInput()
        {
            //reading a batch of arrays from input file
            Console.WriteLine("Getting inputs from file...");
            Console.WriteLine();

            string[] lines = null;
            try
            {
                lines = System.IO.File.ReadAllLines("input.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while reading from input file: " + e.Message);
            }

            //processing each array
            foreach (var line in lines)
            {
                Console.WriteLine("Problem: " + line);
                if (calculatedArrays.TryGetValue(line, out (string, int) value)) //if we already know a solution
                {
                    Console.WriteLine("Precalculated path: " + value.Item1);
                    Console.Write("Precalculated number of steps: " + value.Item2);
                    if (value.Item2 < 0)
                        Console.WriteLine(" (no solution for this problem)");
                    else
                        Console.WriteLine();
                    Console.WriteLine();
                    continue;
                }

                int[] ia = null;
                try
                {
                    ia = line.Split(' ').Select(s => Convert.ToInt32(s)).ToArray(); //string into int array
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{line}' from input file");
                    continue;
                }

                var result = FindBestPath(ia); //calculate best path
                Console.WriteLine("Path: " + result.Item1);
                Console.Write("Number of steps: " + result.Item2);
                if (result.Item2 < 0)
                    Console.WriteLine(" (no solution for this problem)");
                else
                    Console.WriteLine();
                Console.WriteLine();

                calculatedArrays.Add(line, result);
                WriteSolutionToFile(line, result);
            }
        }

        public static void ReadSolutionsFromFile() //read already precalculated solutions from file
        {
            String line, solution, stepsString;
            try
            {
                StreamReader sr = new StreamReader("solutions.txt");
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    solution = sr.ReadLine();
                    stepsString = sr.ReadLine();
                    try
                    {
                        calculatedArrays.Add(line, (solution, Int32.Parse(stepsString)));
                    }
                    catch (ArgumentException)
                    {
                        //there's already a solution for this line in calculatedArrays, so no action should be taken
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{stepsString}'");
                    }
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while reading solutions from file: " + e.Message);
            }
        }

        public static void WriteSolutionToFile(string problem, ValueTuple<string, int> solution)
        {
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("solutions.txt", true))
                {
                    file.WriteLine(problem);
                    file.WriteLine(solution.Item1);
                    file.WriteLine(solution.Item2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while writing to file: " + e.Message);
            }
        }

        public static void PrintCalculatedArrays() //CLI to display known solutions
        {
            Console.WriteLine();
            Console.WriteLine("To see all solutions that were already calculated press 'A'");
            Console.WriteLine("To search for a single already known solution press 'S'");
            Console.WriteLine("Press any other key to quit");


            if (Console.ReadKey(true).Key == ConsoleKey.A)
            {
                foreach (var item in calculatedArrays)
                {
                    Console.WriteLine("Problem: " + item.Key);
                    Console.WriteLine("Path: " + item.Value.Item1);
                    Console.Write("Number of steps: " + item.Value.Item2);
                    if (item.Value.Item2 < 0)
                        Console.WriteLine(" (no solution for this problem)");
                    else
                        Console.WriteLine();
                    Console.WriteLine();
                }
            }

            if (Console.ReadKey(true).Key == ConsoleKey.S)
            {
                Console.WriteLine("Write the number array seperating each number with a single space and press Enter");
                string line = Console.ReadLine();
                if (calculatedArrays.TryGetValue(line, out (string, int) value)) //if we already know a solution
                {
                    Console.WriteLine("Path: " + value.Item1);
                    Console.Write("Number of steps: " + value.Item2);
                    if (value.Item2 < 0)
                        Console.WriteLine(" (no solution for this problem)");
                    else
                        Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Couldn't find a precalculated solution for this problem.");
                }
            }
        }

        static void Main(string[] args)
        {
            ReadSolutionsFromFile();
            ProcessInput();
            PrintCalculatedArrays();
        }
    }
}
