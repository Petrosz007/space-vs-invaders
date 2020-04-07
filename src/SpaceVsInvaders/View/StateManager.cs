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
        public (int, int) SelectedPos { get; private set; }

        public event EventHandler OpenPauseMenu;

        public StateManager(SVsIModel model, ErrorDisplay errorDisplay)
        {
            this.model = model;
            this.errorDisplay = errorDisplay;

            PlacingTower = false;
        }

        public void HandleNewTowerType(TowerType type)
        {
            PlacingTower = true;
            TowerPlacingType = type;
        }

        public void HandleTileClicked(object _, (int, int) pos)
        {
            SelectedPos = pos;
            (int row, int col) = pos;

            if (PlacingTower)
            {
                PlacingTower = false;
                if (!model.PlaceTower(row, col, TowerPlacingType))
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

        public void HandleTowerUpgradeClicked(object sender, EventArgs args)
        {
            if (!model.UpgradeTower(SelectedPos.Item1, SelectedPos.Item2))
            {
                errorDisplay.AddError("Not enough money for the upgrade.");
            }
        }

        public void HandleTowerSellClicked(object sender, EventArgs args)
        {
            model.SellTower(SelectedPos.Item1, SelectedPos.Item2);
        }

        public void HandleCastleUpgradeClicked(object sender, EventArgs args)
        {
            if(!model.UpgradeCastle())
            {
                errorDisplay.AddError("No money for castle upgrade.");
            }
        }

        public void HandleEscapePressed(object sender, EventArgs args)
        {
            if(PlacingTower) 
            {
                PlacingTower = false;
            }
            else 
            {
                OpenPauseMenu?.Invoke(this, new EventArgs());
            }
        }
    }
}