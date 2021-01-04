namespace THEX
{
    public class HexCalculator
    {
        private const float ASPECT_RATIO = 1.732050807568877f;

        private int[,][] directions = {
            {new int[]{0, +1}, new int[]{-1,  0}, new int[]{-1, -1}, new int[]{0, -1}, new int[]{+1, -1}, new int[]{+1,  0}},
            {new int[]{0, +1}, new int[]{-1, +1}, new int[]{-1,  0}, new int[]{0, -1}, new int[]{+1,  0}, new int[]{+1, +1}}
        };

        public int[] Adjacency(int row, int direction)
        {
            int parity = row & 1;
            return this.directions[parity, direction];
        }

        // public Hex Adjacency(Hex[,] hexs, Hex hex, int hexDirection)
        // {

        // if (hex.Row <= 0 || hex.Col <= 0 || hex.Row >= hexs.GetLength(0) - 1 || hex.Col >= hexs.GetLength(1) - 1)
        // {
        //     return null;
        // }
        //     int parity = hex.Row & 1;
        //     int[] direct = this.directions[parity, hexDirection];
        //     Debug.Log(
        //         " direct = "+ hexDirection + ", " +
        //         " row = " + hex.Row + ", row + direct[0] = " + (hex.Row + direct[0]) + " || " +
        //         " col = " + hex.Col + ", col + direct[1] = " + (hex.Col + direct[1])
        //     );
        //     return hexs[hex.Row + direct[0], hex.Col + direct[1]];
        // }

        public (float horizontalDistance, float verticalDistance) DistributionDistance(float size)
        {
            return (HorizontalDistance(size), VerticalDistance(size));
        }

        public float HorizontalDistance(float size)
        {
            return UnitWidth(size) * 2;
        }

        public float VerticalDistance(float size)
        {
            return UnitHeight(size) * 3;
        }

        public float UnitWidth(float size)
        {
            return (size * ASPECT_RATIO) / 2;
        }

        public float UnitHeight(float size)
        {
            return (size * 2) / 4;
        }

        public Coord CenterPosition(int horizontalUnits, int verticalUnits, float horizontalUnitSpacing, float verticalUnitSpacing)
        {
            return new Coord(
                -((horizontalUnitSpacing * (float)horizontalUnits) / 2) + (horizontalUnitSpacing / 2),
                0.0f,
                ((verticalUnitSpacing * (float)verticalUnits) / 2) - (verticalUnitSpacing / 2)
            );
        }
    }
}