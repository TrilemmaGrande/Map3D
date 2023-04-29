namespace ProjectSpaceship.Travel
{
    internal class TravelingEventArgs : EventArgs
    {
        Coordinate coordinate;

        public TravelingEventArgs(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }
        public Coordinate GetCoordinate()
        {
            return coordinate;
        }
    }
}
