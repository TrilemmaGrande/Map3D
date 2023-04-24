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
        public bool SectorListContains(Sector searchedSector)
        {
            foreach (var sector in sectors)
            {
                if (sector.GetSectorCoordinate().GetCoordinateX() == searchedSector.GetSectorCoordinate().GetCoordinateX() &&
                    sector.GetSectorCoordinate().GetCoordinateY() == searchedSector.GetSectorCoordinate().GetCoordinateY() &&
                    sector.GetSectorCoordinate().GetCoordinateZ() == searchedSector.GetSectorCoordinate().GetCoordinateZ())
                {
                    return true;
                }
            }
            return false;
        }
        public Sector GetSectorFromSectorList(Coordinate inputCoordinate)
        {
            foreach (var sector in sectors)
            {
                if (sector.GetSectorCoordinate().GetCoordinateX() == inputCoordinate.GetCoordinateX() &&
                    sector.GetSectorCoordinate().GetCoordinateY() == inputCoordinate.GetCoordinateY() &&
                    sector.GetSectorCoordinate().GetCoordinateZ() == inputCoordinate.GetCoordinateZ())
                {
                    return sector;
                }
            }
            return null;
        }
        public List<Sector> GetSectors()
        {
            return sectors;
        }
        public void CreatePlayerSpaceship(string name, double speedMax, double weight, double fuelMax, double enginePower, Coordinate positionInSector, Sector sector)
        {
            if (SectorListContains(sector))
            {
                sector = GetSectorFromSectorList(sector.GetSectorCoordinate());
            }
            else
            {
                sectors.Add(sector);
            }
            playerSpaceShip = new Spaceship(name, speedMax, weight, fuelMax, enginePower, positionInSector, sector);
            spaceships.Add(playerSpaceShip);            
        }
        public Spaceship GetPlayerSpaceship()
        {
            return playerSpaceShip;
        }
    }
}
