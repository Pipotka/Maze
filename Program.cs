using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maze2
{
    internal class Program
    {
        static int offsetj = -1;

        static void Main(string[] args)
        {
            int Width, Height;

            Console.ResetColor();
            Console.Write("Ширина лабиринта = ");
            Width = int.Parse(Console.ReadLine()) + 4;
            Console.WriteLine();

            Console.Write("Высота лабиринта = ");
            Height = int.Parse(Console.ReadLine()) + 4;

            int[,] Maze = new int[Height, Width];

            FillMaze(Maze, Height, Width);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            bool IsEnd = false;

            int i = 2, j = 2;
            for (int L = 2; L < Height - 2; L++)
            {
                for (int C = 2; C < Width - 2; C++)
                {
                    if (Maze[L,C] == (int)Items.Player)
                    {
                        i = L;
                        j = C;
                    }
                }
            }
            Console.CursorVisible = false;
            Point Player = new Point();
            Player.SetIJ(i, j);
            Maze[i, j] = (int)Items.Player;
            PrintMaze(Maze, Height, Width);
            while (!(IsEnd))
            {

                ConsoleKey key = Console.ReadKey().Key;
                Move(Maze, Player.GetI(), Player.GetJ(), key, ref Player, Height,Width);
                if (Maze[Player.GetI(), Player.GetJ()] == (int)Items.Finish)
                {
                    IsEnd = true;
                }
            }
        }

        static void Navigator (int[,] Maze, int Height, int Width,int Player_I, int Player_J)
        {
            List <Point> WayToFinish = new List <Point>();
            ReNavigator(Maze, WayToFinish, Player_I, Player_J, Dir.None);
            PrintWayToFinsh(WayToFinish);
        }

        static void PrintWayToFinsh(List<Point> WayToFinish)
        {
            for(int index = 0;  index < WayToFinish.Count; index++)
            {
                if(index != WayToFinish.Count - 1)
                {
                    Console.SetCursorPosition(WayToFinish[index].GetJ() + offsetj, WayToFinish[index].GetI());
                    Console.Write('¤');

                }
            }
        }

        static bool ReNavigator(int[,] Maze, List<Point> WayToFoinish, int i, int j, Dir LastPosition)
        {
            bool IsEnd = false;
            Point cell = new Point();

            if (Maze[i, j] == (int)Items.Finish)
            {
                IsEnd = true;
                return IsEnd;
            }

            else
            {
                if (((Maze[i, j - 1] != (int)Items.Wall) && (Maze[i, j - 1] != (int)Items.StrongWall)) && (LastPosition != Dir.Left))
                {
                    cell.SetIJ(i, j - 1);
                    WayToFoinish.Add(cell);
                    IsEnd = ReNavigator(Maze, WayToFoinish, i, j - 1, Dir.Right);
                    if (IsEnd)
                    {
                        return IsEnd;
                    }
                    else
                    {
                        WayToFoinish.Remove(cell);
                    }
                }

                if (((Maze[i, j + 1] != (int)Items.Wall) && (Maze[i, j + 1] != (int)Items.StrongWall)) && (LastPosition != Dir.Right) && (!IsEnd))
                {
                    cell.SetIJ(i, j + 1);
                    WayToFoinish.Add(cell);
                    IsEnd = ReNavigator(Maze, WayToFoinish, i, j + 1, Dir.Left);
                    if (IsEnd)
                    {
                        return IsEnd;
                    }
                    else
                    {
                        WayToFoinish.Remove(cell);
                    }
                }

                if (((Maze[i - 1, j] != (int)Items.Wall) && (Maze[i - 1, j] != (int)Items.StrongWall)) && (LastPosition != Dir.Up) && (!IsEnd))
                {
                    cell.SetIJ(i - 1, j);
                    WayToFoinish.Add(cell);
                    IsEnd = ReNavigator(Maze, WayToFoinish, i - 1, j, Dir.Down);
                    if (IsEnd)
                    {
                        return IsEnd;
                    }
                    else
                    {
                        WayToFoinish.Remove(cell);
                    }
                }

                if (((Maze[i + 1, j] != (int)Items.Wall) && (Maze[i + 1, j] != (int)Items.StrongWall)) && (LastPosition != Dir.Down) && (!IsEnd))
                {
                    cell.SetIJ(i + 1, j);
                    WayToFoinish.Add(cell);
                    IsEnd = ReNavigator(Maze, WayToFoinish, i + 1, j, Dir.Up);
                    if (IsEnd)
                    {
                        return IsEnd;
                    }
                    else
                    {
                        WayToFoinish.Remove(cell);
                    }
                }


                return false;
            }
        }

        static void Move(int[,] Maze, int i, int j, ConsoleKey key, ref Point Player, int Height ,int Width)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (!((Maze[i - 1, j] == (int)Items.Wall) || (Maze[i - 1, j] == (int)Items.StrongWall)))
                    {
                        Console.SetCursorPosition(j + offsetj, i);
                        Console.Write(' ');
                        Player.SetIJ(i - 1, j);
                        Console.SetCursorPosition(j + offsetj, i - 1);
                        Console.Write('▼');
                        Console.SetCursorPosition(Width, Player.GetI());
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (!((Maze[i, j - 1] == (int)Items.Wall) || (Maze[i, j - 1] == (int)Items.StrongWall)))
                    {
                        Console.SetCursorPosition(j + offsetj, i);
                        Console.Write(' ');
                        Player.SetIJ(i, j - 1);
                        Console.SetCursorPosition(j - 1 + offsetj, i);
                        Console.Write('▼');
                        Console.SetCursorPosition(Width, Player.GetI());
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (!((Maze[i, j + 1] == (int)Items.Wall) || (Maze[i, j + 1] == (int)Items.StrongWall)))
                    {
                        Console.SetCursorPosition(j + offsetj, i);
                        Console.Write(' ');
                        Player.SetIJ(i, j + 1);
                        Console.SetCursorPosition(j + 1 + offsetj, i);
                        Console.Write('▼');
                        Console.SetCursorPosition(Width, Player.GetI());
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (!((Maze[i + 1, j] == (int)Items.Wall) || (Maze[i + 1, j] == (int)Items.StrongWall)))
                    {
                        Console.SetCursorPosition(j + offsetj, i);
                        Console.Write(' ');
                        Player.SetIJ(i + 1, j);
                        Console.SetCursorPosition(j + offsetj, i + 1);
                        Console.Write('▼');
                        Console.SetCursorPosition(Width, Player.GetI());
                    }
                    break;

                case ConsoleKey.N:
                {
                    Navigator(Maze,Height,Width,Player.GetI(),Player.GetJ());
                    Console.SetCursorPosition(Width, Player.GetI());
                    break;
                }

            default:
                    break;
            }
        }

        static void FillMaze(int[,] Maze, int Height, int Width)
        {
            FillWalls(Maze, Height, Width);
            Random rand = new Random();
            int i = rand.Next(2, Height - 2);
            int j = rand.Next(2, Width - 2);
            List<Point> list = new List<Point>();

            Maze[i, j] = 0;
            InList(list, Maze, i, j, Height, Width);

            while(list.Count != 0)
            {
                int RandWall = rand.Next(0, list.Count);
                i = list[RandWall].GetI();
                j = list[RandWall].GetJ();

                if(CountOfRoads(Maze,i,j,Height,Width) == 1)
                {
                    Maze[i, j] = 0;
                    InList(list, Maze, i, j, Height, Width);
                }

                list.RemoveAt(RandWall);
            }

            SetPlayer(Maze, Height, Width);
            SetFinish(Maze, Height, Width);
        }

        static void PrintMaze (int[,] Maze, int Height, int Width)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (NotOutMaze(i, j, Height, Width))
                    {
                        if (Maze[i, j] == 1)
                            Console.Write('█');

                        else if (Maze[i, j] == 0)
                            Console.Write(' ');

                        else if (Maze[i, j] == (int)Items.Player)
                            Console.Write('▼');
                    }
                    else if(!((i == 0 || i == Height - 1) || (j == 0 || j == Width - 1)))
                    {
                        if ((i == 1) && ((j > 0) && (j < Width - 1)))
                            Console.Write('▄');

                        else if ((i == Height - 2) && ((j > 0) && (j < Width - 1)))
                            Console.Write('▀');

                        else if (Maze[i, j] == (int)Items.Finish)
                            Console.Write('֍');

                        else
                            Console.Write('█');
                    }
               }
                Console.WriteLine();
            }
        }

        static bool NotOutMaze( int i, int j, int Height, int Width)
        {
            if(!((i < 2 || i >= Height - 2) || (j < 2 || j >= Width - 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool AroundCell (int[,] Maze, int i, int j, int Height, int Width,int item, int mod)
        {
            switch (mod)
            {
                case 1:
                    if(NotOutMaze(i - 1, j, Height, Width))
                    {
                        if (Maze[i - 1, j] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 2:
                    if (NotOutMaze(i, j - 1, Height, Width))
                    {
                        if (Maze[i, j - 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 3:
                    if (NotOutMaze(i, j + 1, Height, Width))
                    {
                        if (Maze[i, j + 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 4:
                    if (NotOutMaze(i + 1, j, Height, Width))
                    {
                        if (Maze[i + 1, j] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 5:
                    if (NotOutMaze(i - 1, j - 1, Height, Width))
                    {
                        if (Maze[i - 1, j - 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 6:
                    if (NotOutMaze(i - 1, j + 1, Height, Width))
                    {
                        if (Maze[i - 1, j + 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 7:
                    if (NotOutMaze(i + 1, j - 1, Height, Width))
                    {
                        if (Maze[i + 1, j - 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                case 8:
                    if (NotOutMaze(i + 1, j + 1, Height, Width))
                    {
                        if (Maze[i + 1, j + 1] == item)
                        {
                            return true;
                        }
                    }
                    return false;

                default:
                    return false;
            }
        }

        static void SetPlayer(int[,] Maze, int Height, int Width)
        {
            int i = 2;
            int j = 2;

            if (Maze[i,j] == 0)
            {
                Maze[i, j] = (int)Items.Player;
            }
            else
            {
                i++;
                j++;
                for(int mod = 1; mod != 9; mod++)
                {
                    if(AroundCell(Maze, i, j, Height, Width, (int)Items.Road, mod))
                    {
                        SetItem(Maze, i, j, (int)Items.Player, mod);
                        break;
                    }
                }
            }
        }

        static void SetFinish (int[,] Maze, int Height, int Width)
        {
            int i = Height - 2;
            int j = Width - 2;
            
            for(;i > 1; i--)
            {
                if (Maze[i, j - 1] == 0)
                {
                    Maze[i, j] = (int)Items.Finish;
                    break;
                }
            }
        }

        static void FillWalls (int[,] Maze, int Height, int Width)
        {
            for(int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if(NotOutMaze(i, j, Height, Width))
                    {
                        Maze[i, j] = 1;
                    }
                    else
                    {
                        Maze[i, j] = 2;
                    }
                }
            }
        }

        static void InList (List<Point> list, int[,] Maze, int i, int j, int Height, int Width)
        {
            Point cell = new Point();
            for (int mod = 1; mod != 5; mod++)
            {
                if(AroundCell(Maze,i,j,Height, Width, (int)Items.Wall, mod))
                {
                    SetPoint(ref cell, i, j, mod);
                    list.Add(cell);
                }
            }
        }

        static void SetPoint(ref Point cell, int i, int j, int mod)
        {
            switch (mod)
            {
                case 1:
                    cell.SetIJ(i - 1, j);
                    break;

                case 2:
                    cell.SetIJ(i, j - 1);
                    break;

                case 3:
                    cell.SetIJ(i, j + 1);
                    break;

                case 4:
                    cell.SetIJ(i + 1, j);
                    break;

                case 5:
                    cell.SetIJ(i - 1, j - 1);
                    break;

                case 6:
                    cell.SetIJ(i - 1, j + 1);
                    break;

                case 7:
                    cell.SetIJ(i + 1, j - 1);
                    break;

                case 8:
                    cell.SetIJ(i + 1, j + 1);
                    break;
            }
        }

        static void SetItem(int[,]Maze, int i, int j,int item, int mod)
        {
            switch (mod)
            {
                case 1:
                    Maze[i - 1, j] = item;
                    break;

                case 2:
                    Maze[i, j - 1] = item;
                    break;

                case 3:
                    Maze[i, j + 1] = item;
                    break;

                case 4:
                    Maze[i + 1, j] = item;
                    break;

                case 5:
                    Maze[i - 1, j - 1] = item;
                    break;

                case 6:
                    Maze[i - 1, j + 1] = item;
                    break;

                case 7:
                    Maze[i + 1, j - 1] = item;
                    break;

                case 8:
                    Maze[i + 1, j + 1] = item;
                    break;
            }
        }

        static int CountOfRoads (int[,]Maze, int i, int j, int Height, int Width)
        {
            int count = 0;

            for (int mod = 1; mod != 5; mod++)
            {
                switch (mod)
                {
                    case 1:
                        if (NotOutMaze(i - 1, j, Height, Width))
                        {
                            if (Maze[i - 1, j] == 0)
                            {
                                count++;
                            }
                        }
                        break;

                    case 2:
                        if (NotOutMaze(i, j - 1, Height, Width))
                        {
                            if (Maze[i, j - 1] == 0)
                            {
                                count++;
                            }
                        }
                        break;

                    case 3:
                        if (NotOutMaze(i, j + 1, Height, Width))
                        {
                            if (Maze[i, j + 1] == 0)
                            {
                                count++;
                            }
                        }
                        break;

                    case 4:
                        if (NotOutMaze(i + 1, j, Height, Width))
                        {
                            if (Maze[i + 1, j] == 0)
                            {
                                count++;
                            }
                        }
                        break;

                    case 5:
                        if (NotOutMaze(i - 1, j - 1, Height, Width))
                        {
                            if (Maze[i - 1, j - 1] == 1)
                            {
                                count++;
                            }
                        }
                        break;

                    case 6:
                        if (NotOutMaze(i - 1, j + 1, Height, Width))
                        {
                            if (Maze[i - 1, j + 1] == 1)
                            {
                                count++;
                            }
                        }
                        break;

                    case 7:
                        if (NotOutMaze(i + 1, j - 1, Height, Width))
                        {
                            if (Maze[i + 1, j - 1] == 1)
                            {
                                count++;
                            }
                        }
                        break;

                    case 8:
                        if (NotOutMaze(i + 1, j + 1, Height, Width))
                        {
                            if (Maze[i + 1, j + 1] == 1)
                            {
                                count++;
                            }
                        }
                        break;
                }
            }
            return count;
        }
    }
}
