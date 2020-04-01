using System;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View
{
    public class StateManager
    {
        private SVsIModel model;
        private ErrorDisplay errorDisplay;
        public bool PlacingTower { get; private set; }
        public TowerType TowerPlacingType { get; private set; }

        public SVsITower SelectedTower { get; private set; }

        public StateManager(SVsIModel model, ErrorDisplay errorDisplay)
        {
            this.model = model;
            this.errorDisplay = errorDisplay;

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
                if(!model.PlaceTower(row, col, TowerPlacingType))
                {
                    errorDisplay.AddError("No money REEEEEEEE");
                }
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