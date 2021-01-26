namespace T {
    public class Hex {
        private Grid _grid;
        private Coord _coord;
        private Hex[] _adjArr = new Hex[6];
        public Grid Grid { get { return this._grid; } }
        public Coord Coord { get { return this._coord; } }
        public int Row { get { return _grid.Row; } }
        public int Col { get { return _grid.Col; } }
        public float X { get { return _coord.X; } }
        public float Z { get { return _coord.Z; } }
        public float Y { get { return _coord.Y; } }
        public Hex[] Adjacencies { get { return _adjArr; } set { this._adjArr = value; } }
        public Hex E { get { return this._adjArr[0]; } set { this._adjArr[0] = value; } }
        public Hex NE { get { return this._adjArr[1]; } set { this._adjArr[1] = value; } }
        public Hex NW { get { return this._adjArr[2]; } set { this._adjArr[2] = value; } }
        public Hex W { get { return this._adjArr[3]; } set { this._adjArr[3] = value; } }
        public Hex SW { get { return this._adjArr[4]; } set { this._adjArr[4] = value; } }
        public Hex SE { get { return this._adjArr[5]; } set { this._adjArr[5] = value; } }

        public Hex(Grid grid, Coord coord) {
            _grid = grid;
            _coord = coord;
            for (int i = 0; i < 6; i++) {
                _adjArr[i] = null;
            }
        }
    }
}