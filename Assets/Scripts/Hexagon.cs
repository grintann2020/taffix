namespace T
{
    public class Hexagon
    {
        private Grid _grid;
        private Coord _coord;
        private Hexagon[] _adjacencies = new Hexagon[6];
        public Grid Grid { get { return this._grid; } }
        public Coord Coord { get { return this._coord; } }
        public int Row { get { return _grid.row; } }
        public int Col { get { return _grid.col; } }
        public float X { get { return _coord.x; } }
        public float Z { get { return _coord.z; } }
        public float Y { get { return _coord.y; } }
        public Hexagon[] Adjacencies { get { return _adjacencies; } set { this._adjacencies = value; } }
        public Hexagon East { get { return this._adjacencies[0]; } set { this._adjacencies[0] = value; } }
        public Hexagon NorthEast { get { return this._adjacencies[1]; } set { this._adjacencies[1] = value; } }
        public Hexagon NorthWest { get { return this._adjacencies[2]; } set { this._adjacencies[2] = value; } }
        public Hexagon West { get { return this._adjacencies[3]; } set { this._adjacencies[3] = value; } }
        public Hexagon SouthWest { get { return this._adjacencies[4]; } set { this._adjacencies[4] = value; } }
        public Hexagon SouthEast { get { return this._adjacencies[5]; } set { this._adjacencies[5] = value; } }

        public Hexagon(Grid grid, Coord coord)
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