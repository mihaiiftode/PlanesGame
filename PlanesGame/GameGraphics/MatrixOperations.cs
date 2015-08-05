namespace PlanesGame.GameGraphics
{
    public class MatrixOperations
    {
        public int[,] Rotate(int[,] matrix, int rows, int collumns)
        {
            var destination = new int[collumns, rows];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < collumns; c++)
                {
                    destination[c, rows - r - 1] = matrix[r, c];
                }
            }
            return destination;
        }

        public int[,] Invert(int[,] matrix, int rows, int collumns)
        {
            if (rows > collumns)
            {
                var firstLine = new int[collumns];
                for (var c = 0; c < collumns; c++)
                {
                    firstLine[c] = matrix[0, c];
                }

                var destination = new int[rows, collumns];
                for (var r = 0; r < rows - 1; r++)
                {
                    for (var c = 0; c < collumns; c++)
                    {
                        destination[r, c] = matrix[r + 1, c];
                    }
                }

                for (var c = 0; c < collumns; c++)
                {
                    destination[rows - 1, c] = firstLine[c];
                }

                return destination;
            }
            else
            {
                var firstCollumn = new int[rows];
                for (var r = 0; r < rows; r++)
                {
                    firstCollumn[r] = matrix[r, 0];
                }

                var destination = new int[rows, collumns];
                for (var r = 0; r < rows; r++)
                {
                    for (var c = 0; c < collumns - 1; c++)
                    {
                        destination[c, r] = matrix[r, c + 1];
                    }
                }

                for (var r = 0; r < rows; r++)
                {
                    destination[r, collumns - 1] = firstCollumn[r];
                }

                return destination;
            }
        }
    }
}