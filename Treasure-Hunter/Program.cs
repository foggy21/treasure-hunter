using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treasure_Hunter
{
    internal class Program
    {
        static char[,] mapWithTraps = new char[24, 27]
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', 'X', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', 'T', ' ', 'T', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', 'T', 'T', ' ', ' ', '#', ' ', 'T', '#', ' ', ' ', 'T', ' ', ' ', ' ', ' ', 'T', '#', ' ', 'T', ' ', ' ', 'X', 'T', '#'},
                {'#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', 'T', '#', '#', '#'},
                {'#', ' ', 'T', '#', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', '#', 'X', ' ', 'T', ' ', ' ', '#', 'T', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', 'X', 'T', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', 'T', 'T', ' ', '#'},
                {'#', 'T', ' ', '#', '#', ' ', '#', ' ', 'T', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', 'T', ' ', 'T', '#', 'T', ' ', 'T', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', 'T', '#', 'X', ' ', '#'},
                {'#', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', 'T', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', '#'},
                {'#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', 'T', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', 'T', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', 'T', ' ', '#'},
                {'#', ' ', '#', ' ', 'T', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', 'X', '#', ' ', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', 'T', ' ', 'T', ' ', 'T', '#', '#', ' ', ' ', ' ', '#'},
                {'#', '#', '#', 'T', ' ', '#', ' ', ' ', 'T', '#', '#', ' ', ' ', '#', '#', '#', 'T', ' ', ' ', ' ', ' ', ' ', '#', ' ', 'T', ' ', '#'},
                {'#', ' ', 'T', ' ', ' ', '#', 'T', ' ', ' ', 'X', '#', ' ', ' ', '#', 'X', ' ', ' ', ' ', 'T', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', 'T', ' ', 'T', '#', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#', 'X', 'T', ' ', '#'},
                {'#', 'X', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', 'T', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', 'X', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            };

        static char[,] map = new char[24, 27]
        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'X', '#'},
                {'#', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', 'X', ' ', '#'},
                {'#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', '#', '#'},
                {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', 'X', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', 'X', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', '#', ' ', '#', ' ', ' ', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', '#', 'X', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', 'X', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', ' ', ' ', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', 'X', '#', ' ', ' ', '#', 'X', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#', 'X', ' ', ' ', '#'},
                {'#', 'X', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', 'X', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
        };

        const int maxHealthOfPlayer = 5;
        const char player = '@';
        const char treasure = 'X';
        const char trap = 'T';
        const char wall = '#';
        const char ground = ' ';
        const char explodedTrap = '*';
        static int startPosX, startPosY; // Start position of player.
        static void Main(string[] args)
        {
            bool isOpen = true;
            int posX = 1, posY = 1; // Position of player.
            int healthOfPlayer = maxHealthOfPlayer;
            int lengthOfBag = 1;
            char[] bag = new char[lengthOfBag];
            

            Console.CursorVisible = false;

            if (!SetPlayerOnMap(ref posX, ref posY, out startPosX, out startPosY)) isOpen = false;

            if (!isOpen)
            {
                Console.Write("Невозможно найти место для появление игрока. Переделайте карту\n");
            }

            while (isOpen)
            {
                DrawMap();
                DrawBar(map.GetUpperBound(map.Rank - 1) + 1, map.GetLowerBound(0), maxHealthOfPlayer, healthOfPlayer, nameOfBar: "Health");
                DrawBag(bag, map.GetUpperBound(map.Rank - 1) + 1, map.GetLowerBound(0) + 1);
                Console.SetCursorPosition(posX, posY);
                Console.Write(player);
                if (MoveOfPlayer(ref posX, ref posY))
                {
                    Console.SetCursorPosition(posY, posX);
                    Console.Write(player);
                    if (mapWithTraps[posY, posX] == trap)
                    {
                        ExploudTrap(map, mapWithTraps, posX, posY, ref healthOfPlayer);
                        if (healthOfPlayer <= 0)
                        {
                            ReloadGameLoop(ref posX, ref posY);
                            healthOfPlayer = maxHealthOfPlayer;
                        }
                    }
                    if (map[posY, posX] == treasure)
                    {
                        bag = GetTreasure(bag, ref lengthOfBag, posX, posY);
                    }
                }
                Console.Clear();
            }
        }


        static void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            int lengthOfColumns = map.GetUpperBound(map.Rank - 1) + 1;
            int iterator = 0;
            foreach (char symbol in map)
            {
                Console.Write($"{symbol}");
                if (++iterator == lengthOfColumns)
                {
                    Console.Write('\n');
                    iterator = 0;
                }
            }
        }

        static bool SetPlayerOnMap(ref int posX, ref int posY, out int startPosX, out int startPosY)
        {
            if ((posX > map.GetUpperBound(map.Rank - 1) || posY > map.GetUpperBound(0)) ||
                (posX <= 0 || posY <= 0))
            {
                startPosX = 0;
                startPosY = 0;
                return false;
            }


            if (map[posY, posX] != wall)
            {
                startPosX = posX;
                startPosY = posY;
                return true;
            }

            if (posX < map.GetUpperBound(map.Rank - 1))
            {
                posX++;
                return SetPlayerOnMap(ref posX, ref posY, out startPosX, out startPosY);

            }
            else if (posY < map.GetUpperBound(0))
            {
                posY++;
                return SetPlayerOnMap(ref posX, ref posY, out startPosX, out startPosY);
            } else
            {
                startPosX = 0;
                startPosY = 0;
                return false;
            }

        }

        static bool MoveOfPlayer(ref int posX, ref int posY)
        {
            ConsoleKey userInput = Console.ReadKey().Key;
            int copyPosX = posX, copyPosY = posY;
            switch (userInput)
            {
                case ConsoleKey.A:
                    copyPosX--;
                    break;
                case ConsoleKey.D:
                    copyPosX++;
                    break;
                case ConsoleKey.W:
                    copyPosY--;
                    break;
                case ConsoleKey.S:
                    copyPosY++;
                    break;
            }

            if (map[copyPosY, copyPosX] != wall)
            {
                posX = copyPosX;
                posY = copyPosY;
                return true;
            }
            return false;
        }
        static void ExploudTrap(char[,] map, char[,] mapWithTraps, int posX, int posY, ref int health)
        {
            Console.Beep();
            map[posY, posX] = explodedTrap;
            mapWithTraps[posY, posX] = ground;
            health--;
        }

        static void DrawBar(int posX, int posY, int maxStatictic, int statictic, string nameOfBar = "", ConsoleColor color = ConsoleColor.Red)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.SetCursorPosition(posX, posY);
            if (nameOfBar != "") Console.Write($" {nameOfBar}: ");
            Console.Write('[');
            for (int i = 1; i <= maxStatictic; i++)
            {
                if (i > statictic)
                {
                    Console.BackgroundColor = defaultColor;
                }
                else
                {
                    Console.BackgroundColor = color;
                }
                Console.Write('|');
            }
            Console.BackgroundColor = defaultColor;
            Console.Write(']');
        }

        static void DrawBag(char[] bag, int posX, int posY, string nameOfBag = "Your bag", ConsoleColor color = ConsoleColor.Magenta)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.SetCursorPosition(posX, posY);
            Console.Write($" {nameOfBag}: [");
            Console.ForegroundColor = color;
            foreach (char item in bag)
            {
                Console.Write(item);
            }
            Console.ForegroundColor = defaultColor;
            Console.Write(']');
        }

        static void ReloadGameLoop(ref int posX, ref int posY)
        {
            posX = startPosX;
            posY = startPosY;
            Console.Beep(800, 800);
        }

        static char[] GetTreasure(char[] bag, ref int lengthOfBag, int posX, int posY)
        {
            bag[lengthOfBag - 1] = map[posY, posX];
            map[posY, posX] = ground;

            char[] expendedBag = new char[lengthOfBag+1];

            for (int i = 0; i < lengthOfBag; i++)
            {
                expendedBag[i] = bag[i];
            }

            ++lengthOfBag;

            return expendedBag;
        }
    }
}
