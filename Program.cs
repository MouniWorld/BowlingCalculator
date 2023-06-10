
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingCalculator
{
    class Program
    {

        static int intLength(int integer)
        {
            int length = 1;
            if (integer / 10 > 0)
                length = 2;
            if (integer / 100 > 0)
                length = 3;
            return length;
        }

        static String scoreTwo(int totalScore, String score2)
        {
            if (intLength(totalScore) == 1)
                score2 += totalScore + "     ";
            else if (intLength(totalScore) == 2)
                score2 += totalScore + "    ";
            if (intLength(totalScore) == 3)
                score2 += totalScore + "   ";
            return score2;
        }
        static int ValidateInput(string prompt)
        {
            int value;
            bool validInput = false;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && value >= 0 && value <= 10)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 10.");
                }
            } while (!validInput);

            return value;
        }

        static void Main(string[] args)
        {
            int frameScore = 0, prevFrame = 0, prevFrameTwo = 0, bowlOne, bowlTwo = 0, frame = 1, totalScore = 0, extraFrame;
            bool strike = false, strikeTwo = false, spare = false;
            String score1 = "", score2 = "", LastFrameTwo = "", LastFrameThree = "", frameNum = "", line = "";

            for (; frame <= 10; frame++)
            {
                Console.WriteLine("Please Enter your Scores for Frame {0}:", frame);
                do //bowlOne loop
                {
                    bowlOne = ValidateInput("Bowl 1:");
                } while (bowlOne > 10 || bowlOne < 0);
                if (spare == true)
                {
                    prevFrame = 10 + bowlOne;
                    spare = false;
                    totalScore = prevFrame + totalScore;
                    score2 = scoreTwo(totalScore, score2);

                }
                if (strikeTwo == true && bowlOne == 10)
                {
                    prevFrameTwo = 30;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                }
                if (strikeTwo == true && bowlOne != 10)
                {
                    strikeTwo = false;
                    prevFrameTwo = 10 + 10 + bowlOne;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                }
                if (strike == true && bowlOne == 10)
                {
                    strikeTwo = true;
                    prevFrameTwo = 20;
                }

                if (bowlOne < 10)
                {
                    do //bowlTwo loop
                    {
                        bowlTwo = ValidateInput("Bowl 2:");
                    } while (bowlTwo > (10 - bowlOne) || bowlTwo < 0);
                    if (bowlOne + bowlTwo == 10)
                    {
                        spare = true;
                        score1 += bowlOne + "-/ | ";
                    }

                    if (strikeTwo == true && frame == 10)
                    {
                        prevFrameTwo = 10 + 10 + bowlTwo;
                        totalScore = prevFrameTwo + totalScore;
                        score2 = scoreTwo(totalScore, score2);
                        strikeTwo = false;
                    }

                    if (strike == true && bowlOne != 10)
                    {
                        strike = false;
                        prevFrame = 10 + bowlOne + bowlTwo;
                        totalScore = totalScore + prevFrame;
                        score2 = scoreTwo(totalScore, score2);
                    }
                    if (spare != true && strike != true && strikeTwo != true)
                    {
                        frameScore = bowlOne + bowlTwo;
                        totalScore = totalScore + frameScore;
                        score2 = scoreTwo(totalScore, score2);
                        if (frame != 10)
                            score1 += " " + bowlOne + "-" + bowlTwo + " |";
                        else
                            score1 += " " + bowlOne + "-" + bowlTwo;
                    }
                }
                else
                {
                    strike = true;
                    prevFrame = 10;
                    if (frame != 10)
                        score1 += " X-  |";
                }
                if (frame == 10 && strike == true)
                {
                    do
                        bowlTwo = ValidateInput("");
                    while (bowlTwo < 0 || bowlTwo > 10);

                    if (strikeTwo == true)
                    {
                        prevFrameTwo = 10 + 10 + bowlTwo;
                        totalScore = prevFrameTwo + totalScore;
                        score2 = scoreTwo(totalScore, score2);
                        strikeTwo = false;
                    }
                }

                if (frame == 10 && (spare == true || strike == true))
                {
                    do
                       extraFrame = ValidateInput("");
                    while (extraFrame < 0 || extraFrame > 10);
                    if (strike == true)
                    {
                        prevFrame = 10 + bowlTwo + extraFrame;
                        totalScore = totalScore + prevFrame;
                        score2 = scoreTwo(totalScore, score2);
                        if (bowlTwo == 10)
                            LastFrameTwo = "-X";
                        else
                            LastFrameTwo += bowlTwo;
                        if (extraFrame == 10)
                            LastFrameThree = "-X";
                        else
                            LastFrameThree += extraFrame;
                        score1 += " X" + LastFrameTwo + LastFrameThree;
                    }
                    else
                    {
                        if (extraFrame == 10)
                            LastFrameThree = "-X";
                        else
                            LastFrameThree += extraFrame;
                        if (bowlTwo + extraFrame == 10 && extraFrame != 10)
                            LastFrameThree = "-/";
                        else
                            LastFrameThree += extraFrame;
                        totalScore = totalScore + 10 + extraFrame;
                        score2 = scoreTwo(totalScore, score2);
                        score1 += bowlOne + "-/" + LastFrameThree;
                    }
                }
                frameNum += frame + "    ";
                line += "------";
            }
            Console.WriteLine(frameNum);
            Console.WriteLine(line);
            Console.WriteLine(score1);
            Console.WriteLine(score2);
        }
    }
}


