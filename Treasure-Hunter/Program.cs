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
        static void Main(string[] args)
        {
            const int maxHealthOfPlayer = 5;
            const char playerSymbol = '@';
            bool isOpen = true;
            int posX = 1, posY = 1; // Position of player.
            int healthOfPlayer = maxHealthOfPlayer;
            char[,] mapWithTraps = new char[24, 27]
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', 'X', '#'},
                {'#', '#', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', 'T', ' ', 'T', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', 'T', 'T', ' ', ' ', '#', ' ', 'T', '#', ' ', ' ', 'T', ' ', ' ', ' ', ' ', 'T', '#', ' ', 'T', ' ', ' ', 'X', 'T', '#'},
                {'#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', 'T', '#', '#', '#'},
                {'#', ' ', 'T', '#', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', '#', 'X', ' ', 'T', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', 'X', 'T', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', 'T', ' ', '#'},
                {'#', 'T', ' ', '#', '#', ' ', '#', ' ', 'T', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', 'T', ' ', 'T', '#', ' ', ' ', 'T', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', 'T', '#', 'X', ' ', '#'},
                {'#', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', '#'},
                {'#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', 'T', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', 'T', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', 'T', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', 'X', '#', ' ', ' ', 'T', 'T', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', 'T', ' ', 'T', ' ', 'T', '#', '#', ' ', ' ', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', 'T', '#', '#', ' ', ' ', '#', '#', '#', 'T', ' ', ' ', ' ', ' ', ' ', '#', ' ', 'T', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', 'T', ' ', ' ', 'X', '#', ' ', ' ', '#', 'X', ' ', ' ', ' ', 'T', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', 'T', ' ', 'T', '#', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#', 'X', 'T', ' ', '#'},
                {'#', 'X', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', 'T', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', ' ', ' ', ' ', 'T', ' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', '#', ' ', ' ', 'X', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            };
            char[,] map = new char[24, 27]
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

            Console.CursorVisible = false;

            if (!SetPlayerOnMap(map, ref posX, ref posY)) isOpen = false;

            if (!isOpen)
            {
                Console.Write("Невозможно найти место для появление игрока. Переделайте карту\n");
            }

            while (isOpen)
            {
                DrawMap(map);
                DrawBar(map.GetUpperBound(map.Rank - 1) + 1, map.GetLowerBound(0), maxHealthOfPlayer, healthOfPlayer, nameOfBar: "Health");
                Console.SetCursorPosition(posX, posY);
                Console.Write(playerSymbol);
                Console.SetCursorPosition(0, 26);
                Console.Write(healthOfPlayer);
                if (MoveOfPlayer(map, ref posX, ref posY))
                {
                    Console.SetCursorPosition(posY, posX);
                    Console.Write(playerSymbol);
                    if (mapWithTraps[posY, posX] == 'T')
                    {
                        ExploudTrap(map, mapWithTraps, posX, posY, ref healthOfPlayer);
                    }
                }
                Console.Clear();
            }
        }


        static void DrawMap(char[,] map)
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

        static bool SetPlayerOnMap(char[,] map, ref int posX, ref int posY)
        {
            if ((posX > map.GetUpperBound(map.Rank - 1) || posY > map.GetUpperBound(0)) ||
                (posX <= 0 || posY <= 0)) return false;

            if (map[posY, posX] != '#')
            {
                return true;
            }

            if (posX < map.GetUpperBound(map.Rank - 1))
            {
                posX++;
                return SetPlayerOnMap(map, ref posX, ref posY);

            }
            else if (posY < map.GetUpperBound(0))
            {
                posY++;
                return SetPlayerOnMap(map, ref posX, ref posY);
            } else
            {
                return false;
            }

        }

        static bool MoveOfPlayer(char[,] map, ref int posX, ref int posY)
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

            if (map[copyPosY, copyPosX] != '#')
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
            map[posY, posX] = '*';
            mapWithTraps[posY, posX] = ' ';
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
    }
}
