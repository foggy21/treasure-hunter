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
            bool isOpen = true;
            int posX = 1, posY = 1; // Position of player.
            char playerSymbol = '@';
            char[,] map =
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', '#', '#'},
                {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', '#', ' ', '#', ' ', ' ', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', ' ', ' ', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            };

            if (!SetPlayerOnMap(map, ref posX, ref posY)) isOpen = false;

            if (!isOpen)
            {
                Console.Write("Невозможно найти место для появление игрока. Переделайте карту\n");
            }

            while (isOpen)
            {
                DrawMap(map);
                if (MoveOfPlayer(map, ref posX, ref posY))
                {
                    map[posY, posX] = playerSymbol;
                }
                Console.Clear();
            }


        }

        static void DrawMap(char[,] map)
        {
            Console.CursorVisible = false;
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

        static bool SetPlayerOnMap(char[,] map, ref int posX, ref int posY, char playerSymbol = '@')
        {
            if ((posX > map.GetUpperBound(map.Rank - 1) || posY > map.GetUpperBound(0)) ||
                (posX <= 0 || posY <= 0)) return false;

            if (map[posY, posX] != '#')
            {
                map[posY, posX] = playerSymbol;
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
                map[posY, posX] = ' ';
                posX = copyPosX;
                posY = copyPosY;
                return true;
            }
            return false;
        }
    }
}
