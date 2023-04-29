using ProjectSpaceship.Spaceships;
using ProjectSpaceship.Spaceships.Modules;
using ProjectSpaceship.Travel;

namespace ProjectSpaceship
{
    class World
    {
        private List<Sector> sectors = new List<Sector>();
        private Spaceship playerSpaceship;
        private List<Spaceship> spaceships = new List<Spaceship>();

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
        public void CreatePlayerSpaceship(string name, Tank tank, Engine engine, Cargo cargo, Position position)
        {
            MergePositionWithSectorList(position);
            playerSpaceship = new Spaceship(name, tank, engine, cargo, position);
            spaceships.Add(playerSpaceship);
            this.playerSpaceship.OnTraveling += OnTraveling_MergeCoordinate;
        }
        private void MergePositionWithSectorList(Position position)
        {
            if (SectorListContains(position.GetSector()))
            {
                position.SetSector(GetSectorFromSectorList(position.GetSector().GetSectorCoordinate()));
            }
            else
            {
                sectors.Add(position.GetSector());
            }
        }
        public void OnTraveling_MergeCoordinate(object sender, TravelingEventArgs e)
        {
            MergeCoordinateWithPositionSector(e.GetCoordinate());
        }
        private void MergeCoordinateWithPositionSector(Coordinate coordinate)
        {
            Sector newSector = GetSectorFromSectorList(coordinate);
            if (newSector != null)
            {
                playerSpaceship.GetPosition().SetSector(newSector);
            }
            else
            {
                CreateSector(coordinate);
                newSector = GetSectorFromSectorList(coordinate);
                playerSpaceship.GetPosition().SetSector(newSector);
            }
        }
        public Spaceship GetPlayerSpaceship()
        {
            return playerSpaceship;
        }
    }
}
