namespace Spaceship
{
    class World
    {
        List<Sector> sectors = new List<Sector>();

        public void CreateSector(Coordinate SectorCoordinate)
        {
            sectors.Add(new Sector(SectorCoordinate));
        }
        private bool SectorListContains(Coordinate SectorCoordinate)
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
    }
}
