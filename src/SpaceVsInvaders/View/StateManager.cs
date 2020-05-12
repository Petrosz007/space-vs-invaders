using System;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View
{
    /// <summary>
    /// Manages the game view's state
    /// </summary>
    public class StateManager
    {
        private SVsIModel model;
        private ErrorDisplay errorDisplay;

        /// <summary>
        /// Whether a tower is currently being placed
        /// </summary>
        /// <value>Whether a tower is currently being placed</value>
        public bool PlacingTower { get; private set; }

        /// <summary>
        /// Type of the tower currently being placed
        /// </summary>
        /// <value>Type of the tower currently being placed</value>
        public TowerType TowerPlacingType { get; private set; }

        /// <summary>
        /// Currently selected tower
        /// </summary>
        /// <value>Currently selected tower</value>
        public SVsITower SelectedTower { get; private set; }

        /// <summary>
        /// Selected tower's position
        /// </summary>
        /// <value>Selected tower's position</value>
        public (int, int) SelectedPos { get; private set; }

        /// <summary>
        /// Whether the game is over
        /// </summary>
        /// <value>Whether the game is over</value>
        public bool GameOver { get; private set; }

        /// <summary>
        /// Whether the game resulted in victory
        /// </summary>
        /// <value>Whether the game resulted in victory</value>
        public bool Victory { get; private set; }

        /// <summary>
        /// Pause menu should be opened event
        /// </summary>
        public event EventHandler OpenPauseMenu;

        /// <summary>
        /// Constructor of <c>StateManager</c>
        /// </summary>
        /// <param name="model">Model to be used</param>
        /// <param name="errorDisplay">Error display to be used</param>
        public StateManager(SVsIModel model, ErrorDisplay errorDisplay)
        {
            this.model = model;
            this.errorDisplay = errorDisplay;

            PlacingTower = false;
            SelectedPos = (0,0);
            GameOver = false;
            Victory = false;
        }

        /// <summary>
        /// Handles tower buy button click event
        /// </summary>
        /// <param name="type">Tower type to be bought</param>
        public void HandleNewTowerType(TowerType type)
        {
            PlacingTower = true;
            TowerPlacingType = type;
        }

        /// <summary>
        /// Handles tile clicks, either places a tower or selects the tower
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="pos">Tile position (row, col)</param>
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

        /// <summary>
        /// Handles tower buy button click event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="towerType">Tower to be bought</param>
        public void HandleTowerBuyClicked(object sender, TowerType towerType)
        {
            PlacingTower = true;
            TowerPlacingType = towerType;
        }

        /// <summary>
        /// Handles tower upgrade buttons click event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">(not used)</param>
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

        /// <summary>
        /// Handles upgrade tower button click event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">(not used)</param>
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

        /// <summary>
        /// Handles Castle upgrade button click event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">(not used)</param>
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

        /// <summary>
        /// Handles Escape key press event,
        /// if PlacingTower then it cancles it,
        /// otherwise it opens the pause menu
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">(not used)</param>
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

        /// <summary>
        /// Handles move key press event, adjusts the current selected position accordingly
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="key">Key pressed (must be one of the arrow keys)</param>
        public void HandleMoveKeysPressed(object sender, Keys key)
        {
            (int row, int col) = key switch{
                Keys.Up    => (SelectedPos.Item1 - 1, SelectedPos.Item2),
                Keys.Down  => (SelectedPos.Item1 + 1, SelectedPos.Item2),
                Keys.Left  => (SelectedPos.Item1, SelectedPos.Item2 - 1),
                Keys.Right => (SelectedPos.Item1, SelectedPos.Item2 + 1),
            };

            if(row < 0 || col < 0 || row >= model.Rows || col >= model.Cols) return;

            SelectedPos = (row, col);
        }

        /// <summary>
        /// Handles Enter key press event, if currently placing tower it places it on the selected position
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">(not used)</param>
        public void HandleEnterPressed(object sender, EventArgs args)
        {
            if(PlacingTower)
            {
                HandleTileClicked(this, SelectedPos);
            }
        }

        /// <summary>
        /// Handles game over event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="victory">Whether the player won or not</param>
        public void HandleGameOver(object sender, bool victory)
        {
            GameOver = true;
            Victory = victory;
        }
    }
}