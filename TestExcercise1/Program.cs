using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExcercise1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = false;
            int rollScore = 0;
            int[] arrScore ;
            int totalScore = 0;
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            Frame[] frames;
            do
            {
                frames = new Frame[10];
                for (int i=1;i<=10;i++)
                {
                    if(i==10)
                    {
                        arrScore = new int[3];
                        for (int j = 1; j <= 3; j++)
                        {
                            Console.WriteLine("Enter Score for the Roll");
                            rollScore = int.Parse(Console.ReadLine());
                            arrScore[j - 1] = rollScore;
                            Console.WriteLine("Frame " + i + " Score " + rollScore);
                            if (j == 2)
                            {
                                if(arrScore[0]+arrScore[1] < 10)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        arrScore = new int[2];
                        for (int j=1;j<=2;j++)
                        {
                            Console.WriteLine("Enter Score for the Roll");
                            rollScore = int.Parse(Console.ReadLine());
                            arrScore[j - 1] = rollScore;
                            Console.WriteLine("Frame "+i +" Score " + rollScore);
                            if(rollScore==10)
                            {
                                break;
                            }
                        }
                    }
                    frames[i - 1] = new Frame(i,arrScore);
                }
                totalScore = scoreCalculator.getScore(frames);
                Console.WriteLine("total Score is " + totalScore);
                Console.WriteLine("Enter true to Calculate Score for another Set Or false to quit");
                isContinue = bool.Parse(Console.ReadLine());
            } while (isContinue);
            Console.WriteLine("Program Completed");
        }


    }

    class Frame
    {
        public int frameNumber { get; set; }
        public int[] frameScore { get; set; }

        public Frame(int fNo, int[] fScore)
        {
            frameNumber = fNo;
            int frameSize = frameNumber < 10 ? 2 : 3;
            frameScore = new int[frameSize];
            //if (frameNumber < 10)
            //{
            //    frameScore = new int[2];
            //}
            //else
            //{
            //    frameScore = new int[3];
            //}
            for(int i=0;i<frameScore.Length;i++)
            {
                frameScore[i] = fScore[i];
                if(fNo != 10 && frameScore[i] == 10)
                {
                    break;
                }
                else if(fNo != 10 && i==1)
                {
                    if(frameScore[i]+ frameScore[i-1] < 10)
                    {
                        break;
                    }
                }
            }
        }
    }

    class ScoreCalculator
    {
        public int getScore(Frame[] frames)
        {
            int totalScore = 0, curScore = 0;
            foreach(var frame in frames)
            {
                bool isSpare = false, isStrike = false;
                int scoreCounter = 0;
                if (frame.frameNumber <=9)
                {
                    foreach (var score in frame.frameScore)
                    {
                        totalScore += score;
                        scoreCounter = scoreCounter + 1;
                        if (score == 10)
                        {
                            isStrike = true;
                            break;
                        }
                        else if (scoreCounter == 2 && (frame.frameScore[0] + frame.frameScore[1]) == 10)
                        {
                            isSpare = true;
                            break;
                        }
                    }
                    if(isStrike)
                    {
                        //add next two balls score
                        //get 1st ball score
                        Frame firstFrame=null, scndFrame=null;
                        firstFrame = frames[frame.frameNumber];
                        if (frame.frameNumber!=9)
                        {
                            scndFrame = frames[frame.frameNumber+1];
                        }
                        if(firstFrame.frameScore[0]!=10 || firstFrame.frameNumber == 10)
                        {
                            totalScore += firstFrame.frameScore[0] + firstFrame.frameScore[1];
                        }
                        else
                        {
                            totalScore += firstFrame.frameScore[0] + scndFrame.frameScore[0];
                        }

                    }
                    else if(isSpare)
                    {
                        Frame firstFrame = frames[frame.frameNumber];
                        totalScore += firstFrame.frameScore[0];
                    }
                }
                else
                {
                    foreach(var score in frame.frameScore)
                    {
                        totalScore += score;
                        scoreCounter = scoreCounter + 1;
                        if(scoreCounter == 2 && (frame.frameScore[0] + frame.frameScore[1]) < 10)
                        {
                            break;
                        }
                    }
                }
            }
            return totalScore;
        }
    }
}
