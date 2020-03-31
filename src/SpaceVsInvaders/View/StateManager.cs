using System;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;

namespace SpaceVsInvaders.View
{
    public class StateManager
    {
        private SVsIModel model;
        public bool PlacingTower { get; private set; }
        public TowerType TowerPlacingType { get; private set; }

        public SVsITower SelectedTower { get; private set; }

        public StateManager(SVsIModel model)
        {
            this.model = model;

            PlacingTower = false;
        }

        public void HandleNewTowerType(TowerType type)
        {
            PlacingTower = true;
            if (type == TowerType.Damage) TowerPlacingType = TowerType.Damage;
            if (type == TowerType.Heal) TowerPlacingType = TowerType.Heal;
            if (type == TowerType.Gold) TowerPlacingType = TowerType.Gold;
        }

        public void HandleTileClicked(object sender, (int,int) pos)
        {
            (int row, int col) = pos;
            
            if(PlacingTower)
            {
                PlacingTower = false;
                model.PlaceTower(row, col, TowerPlacingType);
            }
            else
            {
                // TODO: implement tower info showing
                SelectedTower = model.Towers[row, col];
            }
        }

        public void HandleTowerBuyClicked(object sender, TowerType towerType)
        {
            PlacingTower = true;
            TowerPlacingType = towerType;
        }
    }
}