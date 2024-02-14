using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test with an example puzzle
            int[,] exampleBoard =
            {
                { 3, 9, -1, -1, 5, -1, -1, -1, -1 },
                { -1, -1, -1, 2, -1, -1, -1, -1, 5 },
                { -1, -1, -1, 7, 1, 9, -1, 8, -1 },
                { -1, 5, -1, -1, 6, 8, -1, -1, -1 },
                { 2, -1, 6, -1, -1, 3, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1, -1, -1, 4 },
                { 5, -1, -1, -1, -1, -1, -1, -1, -1 },
                { 6, 7, -1, 1, -1, 5, -1, 4, -1 },
                { 1, -1, 9, -1, -1, -1, 2, -1, -1 }
            };
            SolveSudoku(ref exampleBoard);
            for (int i = 0; i < exampleBoard.GetLength(0); i++)
            {
                for (int j = 0; j < exampleBoard.GetLength(1); j++)
                {
                    Console.Write(exampleBoard[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }
        static bool SolveSudoku(ref int[,] puzzle)
        {
            Tuple<int, int> emptyPosition = FindNextEmpty(puzzle);
            int row = emptyPosition.Item1;
            int col = emptyPosition.Item2;

            if (row < 0)
            {
                return true;
            }

            for (int guess = 1; guess < 10; guess++)
            {
                if (IsValid(puzzle, guess, row, col))
                {
                    puzzle[row, col] = guess;

                    if (SolveSudoku(ref puzzle))
                    {
                        return true;
                    }
                }

                // If not valid OR if our guess does not solve the puzzle
                puzzle[row, col] = -1;
            }

            // If none of the number that we try work, then this puzzle is unsolvable
            return false;
        }
        static Tuple<int, int> FindNextEmpty(int[,] puzzle)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] == -1)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }
        static bool IsValid(int[,] puzzle, int guess, int row, int col)
        {
            // Check in row
            int[] rowValues = puzzle.GetRow(row);
            if (rowValues.Contains(guess))
            {
                return false;
            }

            // Check in column
            int[] colValues = puzzle.GetColumn(col);
            if (colValues.Contains(guess))
            {
                return false;
            }

            // Check 3x3 matrix
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;
            for (int i = rowStart; i < rowStart + 3; i++)
            {
                for (int j = colStart; j < colStart + 3; j++)
                {
                    if (puzzle[i, j] == guess)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
