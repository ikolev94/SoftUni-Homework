﻿namespace ClockwiseRotationMatrix
{
    using System;

    public class WalkInMatrica
    {
        public static void ChangeDirection(ref int dx, ref int dy)
        {
            int[] directionX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] directionY = { 1, 0, -1, -1, -1, 0, 1, 1 };
            for (int i = 0; i < directionX.Length; i++)
            {
                if (directionX[i] == dx && directionY[i] == dy)
                {

                    dx = directionX[(i + 1) % directionX.Length];
                    dy = directionY[(i + 1) % directionY.Length];
                    return;
                }
            } 
        }

        public static bool CheckCell(int[,] arr, int x, int y)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };
            for (int i = 0; i < 8; i++)
            {
                if (x + dirX[i] >= arr.GetLength(0) || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }

                if (y + dirY[i] >= arr.GetLength(0) || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (arr[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static void FindEmptyCell(int[,] arr, out int x, out int y)
        {
            x = 0;
            y = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[i, j] == 0)
                    {
                        x = i;
                        y = j;
                        return;
                    }
                }
            }
        }

        public static void Main()
        {
            var n = InputReader();

            var matrix = CreateRotationMartix(n);

            PrintMatrix(matrix);
        }

        public static int[,] CreateRotationMartix(int n)
        {
            int[,] matrix = new int[n, n];
            int currentValue = 1;
            int row = 0;
            int col = 0;

            currentValue = FillMatrix(matrix, row, col, currentValue);
            currentValue++;
            FindEmptyCell(matrix, out row, out col);

            if (row != 0 && col != 0)
            {
               FillMatrix(matrix, row, col, currentValue);
            }

            return matrix;
        }

        public static int InputReader()
        {
            Console.WriteLine("Enter a positive number ");
            while (true)
            {
                try
                {
                    int n = int.Parse(Console.ReadLine());
                    if (n < 0)
                    {
                        throw new ArgumentException("Number must be positive !");
                    }

                    if (n > 100)
                    {
                        throw new ArgumentException("Number must be smaller than 100 !");
                    }

                    return n;
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write("{0,4}", matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private static int FillMatrix(int[,] matrix,  int row, int col,  int currentValue)
        {
            int n = matrix.GetLength(0);
            int rowDirection = 1;
            int colDirection = 1;

            while (true)
            {
                matrix[row, col] = currentValue;
                if (!CheckCell(matrix, row, col))
                {
                    break;
                }

                while (row + rowDirection >= n || row + rowDirection < 0 || col + colDirection >= n || col + colDirection < 0 || matrix[row + rowDirection, col + colDirection] != 0)
                {
                    ChangeDirection(ref rowDirection, ref colDirection);
                }

                row += rowDirection;
                col += colDirection;
                currentValue++;
            }
            return currentValue;
        }
    }
}