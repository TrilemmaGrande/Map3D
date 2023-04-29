namespace ProjectSpaceship.StellarObjects
{
    internal class BlackHole : StellarObject
    {
        public BlackHole(Coordinate coordinate, string owner) : base(coordinate)
        {
            stellarType = "Black Hole";
            this.owner = owner;
        }
    }
}
