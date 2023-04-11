namespace Map3D
{
    class World
    {
        List<Sector> sectors = new List<Sector>();

        public bool CreateSector(Coordinate SectorCoordinate)
        {

            if (SectorListContains(SectorCoordinate))
            {
                return false;
            }
            sectors.Add(new Sector(SectorCoordinate));
            return true;
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
