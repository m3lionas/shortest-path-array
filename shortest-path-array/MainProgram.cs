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
        public static ArrayList inputArrays = new ArrayList();
        public static Dictionary<string, ValueTuple<string, int>> calculatedArrays = new Dictionary<string, ValueTuple<string, int>>();

        public static void printAllCalculatedArrays()
        {
            Console.WriteLine();
            Console.WriteLine("Want to see all solutions that were already calculated? [y/n]");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
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

        public static void ReadSolutionsFromFile()
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
                Console.WriteLine("Exception while reading from file: " + e.Message);
            }
        }

        public static void readInputFromFile() //catch Format exception
        {
           string[] lines = System.IO.File.ReadAllLines("input.txt");
           foreach (var line in lines)
            {
                Console.WriteLine(line);
                ValueTuple<string,int> value;
                if (calculatedArrays.TryGetValue(line, out value))
                {
                    Console.WriteLine("got it already: " + value);
                    continue;
                }
                int[] ia = line.Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
                //Console.WriteLine(String.Join(",", ia));

                var result = Jumpy2(ia);
                Console.WriteLine(result);

                calculatedArrays.Add(line, result);
                WriteSolutionToFile(line, result);

                inputArrays.Add(ia);
            }
        }
        public static int jumpy(int[] arr)
        {
            if (arr.Length <= 1)
                return 0;

            if (arr[0] < 1)
                return -1;

            int position = arr.Length - 1;
            int prevPosition;
            int steps = 0;

            while (position != 0)
            {
                prevPosition = position;
                for (int i = 0; i < position; i++)
                {
                    if (arr[i] >= position - i)
                    {
                        position = i;
                        Console.WriteLine(arr[i]);
                        steps++;
                        //break;
                    }
                }
                if (prevPosition == position) return -1;
            }
            return steps;
        }

        public static (string solution, int steps) Jumpy2(int[] arr)
        {
            if (arr.Length <= 1)
                return ("-", 0);

            if (arr[0] < 1)
                return ("-", -1);

            int position = arr.Length - 1;
            int prevPosition;
            int steps = 0;
            string result = arr[position].ToString();

            while (position != 0)
            {
                prevPosition = position;
                for (int i = 0; i < position; i++)
                {
                    if (arr[i] >= position - i)
                    {
                        position = i;
                        steps++;

                        result = result.Insert(0, arr[i].ToString() + " ");
                        //result = result.Insert(0, arr[i].ToString());
                        //result += " ";
                        //result += arr[i].ToString();
                    }
                }
                if (prevPosition == position) return ("-", -1);
            }
            //Console.WriteLine("End result: " + result);
            return (result, steps);
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] arr = new int[] { 1, 3, -4, 2, -9, 2, 0, 5, 6, 8, 9 };
            int[] arr1 = new int[] { 1, 2, 0, 3, 0, 2, 0};

            //Console.WriteLine(String.Join(" ", arr1));
            ReadSolutionsFromFile();
            readInputFromFile();
            printAllCalculatedArrays();

            // calling minJumps method 
            //Console.WriteLine(minJumps(arr1));
            //Console.WriteLine();
            //Console.WriteLine(jumpy(arr1));

        }
    }
}
