namespace Spaceship
{
    class World
    {
        private static List<Sector> sectors = new List<Sector>();
        private static Spaceship playerSpaceShip;
        private static List<Spaceship> spaceships = new List<Spaceship>();

        public void CreateSector(Coordinate SectorCoordinate)
        {
            sectors.Add(new Sector(SectorCoordinate));
        }
        public bool SectorListContains(Coordinate SectorCoordinate)
        {
            foreach (var sector in sectors)
            {
                if (sector.GetSectorPosition() == SectorCoordinate)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Sector> GetSectors()
        {
            return sectors;
        }
        public Sector GetSectorFromSectorList(Coordinate coordinate)
        {
            foreach (var sector in sectors)
            {
                if (sector.GetSectorPosition() == coordinate)
                {
                    return sector;
                }
            }
            return null;
        }
        public void CreatePlayerSpaceship(string name, double speedMax, double weight, double fuelMax, double enginePower, Coordinate positionInSector, Coordinate positionInWorld)
        {
            playerSpaceShip = new Spaceship(name, speedMax, weight, fuelMax, enginePower, positionInSector, positionInWorld, this);
            spaceships.Add(playerSpaceShip);
        }
        public Spaceship GetPlayerSpaceship()
        {
            return playerSpaceShip;
        }
    }
}
