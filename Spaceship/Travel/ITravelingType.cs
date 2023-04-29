namespace ProjectSpaceship.Travel
{
    internal interface ITravelingType
    {
        public double CalcDistance(Traveling traveling);
        public Coordinate CalcNewPositionInSector(Traveling traveling);
        public Coordinate CalcNewPositionInWorld(Traveling traveling);
        public void TravelWithAnimation(Traveling traveling);
    }
}
