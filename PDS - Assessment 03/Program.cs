using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PDS___Assessment_03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calling the game class.
            Game link = new Game();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("CMP1127M Programming and Data Structures: Assessment 3 - James Astell - AST17668733");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("\n\t\t\t**********************************************************");
            Console.WriteLine("\t\t\t*                  Welcome to my game!                   *");
            Console.WriteLine("\t\t\t*                                                        *");
            Console.WriteLine("\t\t\t*    You will be presented with three Prime Ministers    *");
            Console.WriteLine("\t\t\t*          from the past 300 years and must              *");
            Console.WriteLine("\t\t\t*         guess which of those served first.             *");
            Console.WriteLine("\t\t\t*                                                        *");
            Console.WriteLine("\t\t\t*       When you are ready press 'Enter' to begin!       *");
            Console.WriteLine("\t\t\t**********************************************************\n");
            Console.ReadLine();
            Console.Clear();
            link.score();
        }
    }



    class Game
    {
        public void score()
        {
            while (true)
            {
                Console.Clear();
                Player score = new Player();

                // Reads in the excel file using StreamReader.
                var read = new StreamReader("list_of_prime_ministers_of_uk-1-UPDATE (1).csv");

                // Turning the excel collumns into lists.
                List<string> CollumnNo = new List<string>();
                List<string> CollumnPM = new List<string>();
                List<string> CollumnDOB = new List<string>();
                List<string> CollumnStart = new List<string>();
                List<string> CollumnEnd = new List<string>();

                while (!read.EndOfStream)
                {
                    // Splitting the excel data into it's seperate rows.
                    var line = read.ReadLine();
                    var values = line.Split(',');


                    //Stops any of the same lists being used.
                    if (!values[0].Equals("No"))
                    {
                        // Adding the values into their individual arrays.
                        CollumnNo.Add(values[0]);
                        CollumnPM.Add(values[1]);
                        CollumnDOB.Add(values[2]);
                        CollumnStart.Add(values[3]);
                        CollumnEnd.Add(values[4]);
                    }
                }

                // Loops the program 5 times and generates a new random list each time.
                int l = 0;
                while (l < 5)
                {


                    // Joins the No and Prime Minister lists together and randomizers them together.
                    var joined = CollumnNo.Zip(CollumnPM, (x, y) => new { x, y });
                    var joined2 = CollumnPM.Zip(CollumnDOB, (y, z) => new { y, z });
                    var shuffled = joined.OrderBy(x => Guid.NewGuid()).ToList();
                    CollumnNo = shuffled.Select(pair => pair.x).ToList();
                    CollumnPM = shuffled.Select(pair => pair.y).ToList();

                    string firstNo = ""; ;
                    string secondNo = "";
                    string thirdNo = "";
                    string firstPM = "";
                    string secondPM = "";
                    string thirdPM = "";

                    // Selects the first three values from the randomized list.
                    firstPM = CollumnPM[0];
                    secondPM = CollumnPM[1];
                    thirdPM = CollumnPM[2];
                    firstNo = CollumnNo[0];
                    secondNo = CollumnNo[1];
                    thirdNo = CollumnNo[2];

                    // Turns the No's into int so we can determine which is the lowest.
                    int firstNoInt = Int32.Parse(firstNo);
                    int secondNoInt = Int32.Parse(secondNo);
                    int thirdNoInt = Int32.Parse(thirdNo);

                    Console.WriteLine("\n -----------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("\n {0} === 1 \t\t {1} === 2 \t\t {2} === 3 \t\t ", firstPM, secondPM,  thirdPM);
                    Console.WriteLine("\n -----------------------------------------------------------------------------------------------------------");

                    try
                    {
                        int userGuess;

                        Console.WriteLine("\n Please type 1 / 2 / 3 for the Prime Minister you believe served first: ");

                        userGuess = Convert.ToInt32(Console.ReadLine());

                        // Calculating which is the lowest value out of the 3.
                        List<int> intList = new List<int> { firstNoInt, secondNoInt, thirdNoInt };
                        int minValue = intList.Min();

                        // Whichever the lowest value is, the code changes that specific value to 1,2 or 3 to match the users input.
                        if (firstNoInt == minValue)
                        {
                            firstNoInt = 1;
                        }
                        if (secondNoInt == minValue)
                        {
                            secondNoInt = 2;
                        }
                        if (thirdNoInt == minValue)
                        {
                            thirdNoInt = 3;
                        }

                        String firstNoString = firstNoInt.ToString();
                        String secondNoString = secondNoInt.ToString();
                        String thirdNoString = thirdNoInt.ToString();
                        String userGuessString = userGuess.ToString();

                        // Displays whether the user is correct or not.
                        switch (userGuessString)
                        {
                            case "1":
                                if (firstNoString == userGuessString)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Correct!");
                                    score.plusScore(); 
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Incorrect!");
                                }
                                break;
                            case "2":
                                if (secondNoString == userGuessString)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Correct!");
                                    score.plusScore();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Incorrect!");
                                }
                                break;
                            case "3":
                                if (thirdNoString == userGuessString)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Correct!");
                                    score.plusScore();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n That was Incorrect!");
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("\n Please enter a valid input (1, 2 or 3)");
                                break;
                        }
                        l++;
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("\n You did somthing wrong! Please only type 1, 2 or 3");
                    }
                }
                Console.Clear();
                Console.WriteLine("\n Thank you for playing!");
                score.showScore();

                // Allowing the user to restart the game if they wish.
                while (true)
                {   
                Console.WriteLine("\n Do you want to play again [Y / N]? ");
                string restart = Console.ReadLine().ToUpper();
                    if (restart == "Y")                    
                        break;
                    if (restart == "N")
                        return;
                    else
                    {
                        Console.WriteLine(" Please enter Y or N");
                    }
                }

            }
        }
    }


    // Creating a new class to hold the players score.
    class Player
    {
        public int userScore;

        public void showScore()
        {
            Console.WriteLine(" You scored {0} / 5!", userScore);
        }
        // Adding a point to the users score.
        public void plusScore()
        {
            userScore = userScore + 1;
        }
    }
}
