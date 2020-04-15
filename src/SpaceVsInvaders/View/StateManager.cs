using System;
using Microsoft.Xna.Framework.Input;
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
        public bool GameOver { get; private set; }
        public bool Victory { get; private set; }

        public event EventHandler OpenPauseMenu;

        public StateManager(SVsIModel model, ErrorDisplay errorDisplay)
        {
            this.model = model;
            this.errorDisplay = errorDisplay;

            PlacingTower = false;
            SelectedPos = (0,0);
            GameOver = false;
            Victory = false;
        }

        public void HandleNewTowerType(TowerType type)
        {
            PlacingTower = true;
            TowerPlacingType = type;
        }

        public void HandleTileClicked(object sender, (int, int) pos)
        {
            SelectedPos = pos;
            (int row, int col) = pos;

            if (PlacingTower)
            {
                PlacingTower = false;
                try {
                    model.PlaceTower(row, col, TowerPlacingType);
                }
                catch(SVsIModelException error)
                {
                    errorDisplay.AddError(error.Message);
                }
            }
            else
            {
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
            try {
                model.UpgradeTower(SelectedPos.Item1, SelectedPos.Item2);
            }
            catch(SVsIModelException error)
            {
                errorDisplay.AddError(error.Message);
            }
        }

        public void HandleTowerSellClicked(object sender, EventArgs args)
        {
            try {
                model.SellTower(SelectedPos.Item1, SelectedPos.Item2);
            }
            catch(SVsIModelException error)
            {
                errorDisplay.AddError(error.Message);
            }
        }

        public void HandleCastleUpgradeClicked(object sender, EventArgs args)
        {
            try {
                model.UpgradeCastle();
            }
            catch(SVsIModelException error)
            {
                errorDisplay.AddError(error.Message);
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

        public void HandleMoveKeysPressed(object sender, Keys key)
        {
            (int row, int col) = key switch{
                Keys.Up    => (SelectedPos.Item1 - 1, SelectedPos.Item2),
                Keys.Down  => (SelectedPos.Item1 + 1, SelectedPos.Item2),
                Keys.Left  => (SelectedPos.Item1, SelectedPos.Item2 - 1),
                Keys.Right => (SelectedPos.Item1, SelectedPos.Item2 + 1),
            };

            if(row < 0 || col < 0 || row >= model.Rows || col >= model.Rows) return;

            SelectedPos = (row, col);
        }

        public void HandleEnterPressed(object sender, EventArgs args)
        {
            if(PlacingTower)
            {
                HandleTileClicked(this, SelectedPos);
            }
        }

        public void HandleGameOver(object sender, bool victory)
        {
            GameOver = true;
            Victory = victory;
        }
    }
}