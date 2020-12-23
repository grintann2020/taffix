namespace THEX
{
    public class HexP
    {
        private Grid _grid;
        private Coord _coord;
        private HexP[] _adjacencies = new HexP[6];
        public Grid Grid { get { return this._grid; } }
        public Coord Coord { get { return this._coord; } }
        public int Row { get { return _grid.row; } }
        public int Col { get { return _grid.col; } }
        public float X { get { return _coord.x; } }
        public float Z { get { return _coord.z; } }
        public float Y { get { return _coord.y; } }
        public HexP[] Adjacencies { get { return _adjacencies; } set { this._adjacencies = value; } }
        public HexP East { get { return this._adjacencies[0]; } set { this._adjacencies[0] = value; } }
        public HexP NorthEast { get { return this._adjacencies[1]; } set { this._adjacencies[1] = value; } }
        public HexP NorthWest { get { return this._adjacencies[2]; } set { this._adjacencies[2] = value; } }
        public HexP West { get { return this._adjacencies[3]; } set { this._adjacencies[3] = value; } }
        public HexP SouthWest { get { return this._adjacencies[4]; } set { this._adjacencies[4] = value; } }
        public HexP SouthEast { get { return this._adjacencies[5]; } set { this._adjacencies[5] = value; } }

        public HexP(Grid grid, Coord coord)
        {
            this._grid = grid;
            this._coord = coord;
            for (int i = 0; i < 6; i++)
            {
                this._adjacencies[i] = null;
            }
        }
    }
}