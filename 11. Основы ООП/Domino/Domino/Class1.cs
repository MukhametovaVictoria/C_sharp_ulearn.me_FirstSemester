using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino
{
    public static class Domino
    {
        public static List<int[]> domino;
        public static Dictionary<int[], int> scores;
        public static List<int[]> dominoOnTheTable = new List<int[]>();
        public static int start; //do auto 4 and after comparing minimal results
        public static int end;
        public static bool endTheGame = false;
        public static void CreateListWithDomino() //do automaticly 1
        {
            domino = new List<int[]>();
            scores = new Dictionary<int[], int>();
            for (int i = 0; i < 7; i++)
                for (int j = i; j < 7; j++)
                {
                    int[] oneDomino = new int[] { i, j };
                    domino.Add(oneDomino);
                    if (i + j != 0) scores.Add(oneDomino, i + j);
                    else scores[oneDomino] = 10;
                }
        }
    }

    public class User
    {
        public List<int[]> userDomino;
        Random rnd = new Random();
        public int userScores;
        
        public void Take7Domino() //do automaticly after the start of the game 2
        {
            for (int i = 0; i < 7; i++)
                Take1Domino();
        }

        public void Take1Domino() //the button for user
        {
            int index = rnd.Next(0, Domino.domino.Count - 1);
            userDomino.Add(Domino.domino[index]);
            Domino.domino.RemoveAt(index);

            if (userDomino.Count >= 10)
                CountScores();
        }

        public int FindMinDoubleDominoInUserList() //do auto 3
        {
            for (int i = 0; i < 7; i++)
                if (userDomino.Contains(new int[] {i, i})) return i;
            return -1; //no one doubles
        }

        //if user makes a desition 
        public bool Compare(int[] selectedDomino, int previous)
        {
            for (int i = 0; i < 2; i++)
                if (selectedDomino[i] == previous) return true;
            return false;
        }

        public void MakeAMove(Dictionary<int[], int> previous, int[] selectedDomino) //button
        {
            var value = previous.Values.First();
            var key = previous.Keys.First();
            var resolution = Compare(selectedDomino, value);
            if (resolution)
            {
                userDomino.Remove(selectedDomino);
                if (selectedDomino[0] == value) ChangeValues(key, selectedDomino[1], selectedDomino);
                else ChangeValues(key, selectedDomino[0], selectedDomino);

                if (userDomino.Count == 0) Console.WriteLine("You are win");
            }
            else Console.WriteLine("You chose wrong domino");
        }

        public static void ChangeValues(int[] key, int value, int[] selectedDomino)
        {
            if (key == Domino.dominoOnTheTable[0])
            {
                Domino.dominoOnTheTable.Insert(0, selectedDomino);
                Domino.start = value;
            }
            else
            {
                Domino.dominoOnTheTable.Add(selectedDomino);
                Domino.end = value;
            }
        }

        public Dictionary<int[], int> ChooseThePreviousDomino(int[] selected) //button
        {
            var dic = new Dictionary<int[], int>();
            if (selected == Domino.dominoOnTheTable[0] && Domino.dominoOnTheTable.Count > 1)
                dic.Add(selected, Domino.start);
            else if (selected == Domino.dominoOnTheTable[Domino.dominoOnTheTable.Count - 1])
                dic.Add(selected, Domino.end);
            else dic.Add(selected, -1);
            return  dic;
        }

        public bool UserCanMove()
        {
            for (int i = 0; i < userDomino.Count; i++)
                for (int j = 0; j < 2; j++)
                    if (userDomino[i][j] == Domino.start || userDomino[i][j] == Domino.end) return true;
            return false;
        }

        public void CountScores()
        {
            int scores = 0;
            for (int i = 0; i < userDomino.Count; i++)
                scores += Domino.scores[userDomino[i]];

            if (scores >= 13) userScores += scores;

            if (userScores > 100) EndTheGame();
        }

        public static void EndTheGame()
        {
            Domino.endTheGame = true;
        }

    }
}
