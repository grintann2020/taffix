namespace T {
    public class HexCalc {
        private const float ASPECT_RATIO = 1.732050807568877f;

        private int[,][] dirArr = {
            {new int[]{0, +1}, new int[]{-1,  0}, new int[]{-1, -1}, new int[]{0, -1}, new int[]{+1, -1}, new int[]{+1,  0}},
            {new int[]{0, +1}, new int[]{-1, +1}, new int[]{-1,  0}, new int[]{0, -1}, new int[]{+1,  0}, new int[]{+1, +1}}
        };

        public int[] Adjacency(int row, int dir) {
            int parity = row & 1;
            return this.dirArr[parity, dir];
        }

        // public Hex Adjacency(Hex[,] hexs, Hex hex, int hexDirection)
        // {

        // if (hex.Row <= 0 || hex.Col <= 0 || hex.Row >= hexs.GetLength(0) - 1 || hex.Col >= hexs.GetLength(1) - 1)
        // {
        //     return null;
        // }
        //     int parity = hex.Row & 1;
        //     int[] direct = this.dirArr[parity, hexDirection];
        //     Debug.Log(
        //         " direct = "+ hexDirection + ", " +
        //         " row = " + hex.Row + ", row + direct[0] = " + (hex.Row + direct[0]) + " || " +
        //         " col = " + hex.Col + ", col + direct[1] = " + (hex.Col + direct[1])
        //     );
        //     return hexs[hex.Row + direct[0], hex.Col + direct[1]];
        // }

        public (float hDist, float vDist) DistributeDist(float size) {
            return (HDist(size), VDist(size));
        }

        public float HDist(float size) {
            return UnitW(HexW(size)) * 2;
        }

        public float VDist(float size) {
            return UnitH(HexH(size)) * 3;
        }

        public float UnitW(float hexW) {
            return hexW / 2;
        }

        public float UnitH(float hexH) {
            return hexH / 4;
        }

        public float HexW(float size) {
            return size * ASPECT_RATIO;
        }

        public float HexH(float size) {
            return size * 2;
        }

        public Coord CenterPos(int hUnits, int vUnits, float hUnitSpacing, float vUnitSpacing) {
            return new Coord(
                -((hUnitSpacing * (float)hUnits) / 2) + (hUnitSpacing / 2),
                0.0f,
                ((vUnitSpacing * (float)vUnits) / 2) - (vUnitSpacing / 2)
            );
        }
    }
}