namespace DefaultNamespace;
using System;

public class GameOfLife
{
    // Size of the grid
    private static int gridSize = 20;

    // 2D array to represent the grid
    private static bool[,] grid = new bool[gridSize, gridSize];

    // Function to initialize the grid with random values
    private static void InitializeGrid()
    {
        Random random = new Random();
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                grid[i, j] = random.Next(2) == 1;
            }
        }
    }

    // Function to count the number of live neighbors of a cell
    private static int CountLiveNeighbors(int row, int col)
    {
        int liveNeighbors = 0;

        // Loop through the 8 neighboring cells
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                // Check if the cell is within the grid boundaries
                if (i >= 0 && i < gridSize && j >= 0 && j < gridSize && (i != row || j != col))
                {
                    // Increment liveNeighbors if the neighbor is alive
                    if (grid[i, j])
                    {
                        liveNeighbors++;
                    }
                }
            }
        }

        return liveNeighbors;
    }

    // Function to update the grid based on Conway's rules
    private static void UpdateGrid()
    {
        // Create a temporary grid to store the next generation
        bool[,] nextGeneration = new bool[gridSize, gridSize];

        // Loop through each cell in the grid
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // Count the live neighbors of the current cell
                int liveNeighbors = CountLiveNeighbors(i, j);

                // Apply Conway's rules to determine the state of the cell in the next generation
                if (grid[i, j] && (liveNeighbors < 2 || liveNeighbors > 3))
                {
                    // Rule 1 & 3: A live cell with fewer than two live neighbours dies (underpopulation).
                    // Rule 3: A live cell with more than three live neighbours dies (overpopulation).
                    nextGeneration[i, j] = false;
                }
                else if (!grid[i, j] && liveNeighbors == 3)
                {
                    // Rule 2: A dead cell with exactly three live neighbours becomes a live cell (reproduction).
                    nextGeneration[i, j] = true;
                }
                else
                {
                    // Rule 4: A live cell with two or three live neighbours lives on to the next generation.
                    nextGeneration[i, j] = grid[i, j];
                }
            }
        }

        // Update the grid with the next generation
        grid = nextGeneration;
    }

    // Function to print the grid to the console
    private static void PrintGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Console.Write(grid[i, j] ? "O" : " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // Function to run the game of life for a specified number of generations
    public static void RunGame(int generations)
    {
        // Initialize the grid with random values
        InitializeGrid();

        // Print the initial grid
        PrintGrid();

        // Loop for the specified number of generations
        for (int i = 0; i < generations; i++)
        {
            // Update the grid based on Conway's rules
            UpdateGrid();

            // Print the grid after each update
            PrintGrid();
        }
    }

    public static void Main(string[] args)
    {
        // Run the game of life for 10 generations
        RunGame(10);
    }
}
}