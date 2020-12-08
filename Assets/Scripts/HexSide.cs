namespace CCCC
{
    public struct HexSide
    {
        public const float DEGREE = 30.0f;
        private Coord _coord;
        public Coord Coord { get { return this._coord; } }
        public int _indexOfDirection;
        public float Direction
        {
            get { return (float)_indexOfDirection * DEGREE; }
        }
        public float X { get { return _coord.x; } }
        public float Y { get { return _coord.y; } }
        public float Z { get { return _coord.z; } }
        
        public HexSide(int indexOfDirection, Coord coord)
        {
            this._indexOfDirection = indexOfDirection;
            this._coord = coord;
        }
    }
}