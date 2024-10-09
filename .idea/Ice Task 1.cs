namespace DefaultNamespace;

using System;

public class PerlinNoise
{
    private static Random random = new Random();

    // Generate a 2D array of random values for the gradient vectors
    private static double[,] GenerateGradientVectors(int gridSize)
    {
        double[,] gradientVectors = new double[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Generate a random angle between 0 and 2*PI
                double angle = random.NextDouble() * 2 * Math.PI;
                // Calculate the gradient vector components
                gradientVectors[x, y] = Math.Cos(angle);
                gradientVectors[x, y + 1] = Math.Sin(angle);
            }
        }

        return gradientVectors;
    }

    // Calculate the dot product between a point and a gradient vector
    private static double DotProduct(double x, double y, double gx, double gy)
    {
        return x * gx + y * gy;
    }

    // Calculate the smooth interpolation function (smoothstep)
    private static double SmoothStep(double t)
    {
        return t * t * (3 - 2 * t);
    }

    // Calculate the Perlin noise value at a given point
    public static double PerlinNoise2D(double x, double y, double[,] gradientVectors, int gridSize)
    {
        // Find the grid cell containing the point
        int gridX = (int)Math.Floor(x);
        int gridY = (int)Math.Floor(y);

        // Calculate the fractional coordinates within the cell
        double fx = x - gridX;
        double fy = y - gridY;

        // Calculate the dot products with the gradient vectors at the corners of the cell
        double topLeft = DotProduct(fx, fy, gradientVectors[gridX, gridY], gradientVectors[gridX, gridY + 1]);
        double topRight = DotProduct(fx - 1, fy, gradientVectors[gridX + 1, gridY], gradientVectors[gridX + 1, gridY + 1]);
        double bottomLeft = DotProduct(fx, fy - 1, gradientVectors[gridX, gridY - 1], gradientVectors[gridX, gridY]);
        double bottomRight = DotProduct(fx - 1, fy - 1, gradientVectors[gridX + 1, gridY - 1], gradientVectors[gridX + 1, gridY]);

        // Interpolate the dot products using smoothstep
        double top = SmoothStep(fx) * topLeft + (1 - SmoothStep(fx)) * topRight;
        double bottom = SmoothStep(fx) * bottomLeft + (1 - SmoothStep(fx)) * bottomRight;
        double noiseValue = SmoothStep(fy) * top + (1 - SmoothStep(fy)) * bottom;

        return noiseValue;
    }

    public static void Main(string[] args)
    {
        // Set the grid size
        int gridSize = 5;

        // Generate the gradient vectors
        double[,] gradientVectors = GenerateGradientVectors(gridSize);

        // Calculate and print the Perlin noise values for a 5x5 grid
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                double noiseValue = PerlinNoise2D(x, y, gradientVectors, gridSize);
                Console.Write(noiseValue + " ");
            }
            Console.WriteLine();
        }
    }
}