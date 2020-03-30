using System;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View
{
    public class StateManager
    {
        private SVsIModel model;
        public bool PlacingTower { get; set; }
        public TowerType TowerPlacingType { get; set; }

        public StateManager(SVsIModel model)
        {
            this.model = model;

            PlacingTower = false;
        }

        public void HandleTileClicked(object sender, Tuple<int,int> position)
        {
            if(PlacingTower)
            {
                PlacingTower = false;
                model.PlaceTower(position.Item1, position.Item2, TowerPlacingType);
            }
            else
            {
                // TODO: implement tower info showing
            }
        }

        public void HandleTowerBuyClicked(object sender, TowerType towerType)
        {
            PlacingTower = true;
            TowerPlacingType = towerType;
        }
    }
}