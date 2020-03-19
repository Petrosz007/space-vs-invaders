using System;
using System.Collections.Generic;
using SpaceVsInvaders.Castle;
using SpaceVsInvaders.Towers;
using SpaceVsInvaders.Enemies;

namespace SpaceVsInvaders.Model
{
    public class SVsIModel
    {
        public int Money { get; private set; }
        public int SecondsElapsed { get; private set; }
        
        public List<SVsIEnemy>[,] Enemies;

        public int Rows { get; private set; }

        public int Cols { get; private set; }

        public SVsITower[,] Towers;

        public int Difficulty { get; private set; }

        public SVsICastle Castle;

        public SVsIModel()
        {
            NewGame();
            
        }

        public void HandleTick()
        {

        }

        private void HandleTowers()
        {

        }

        private void HandleEnemies()
        {

        }


        public void NewGame() 
        {
            Money = 0;
            SecondsElapsed = 0;
            Castle = new SVsICastle();
            Enemies = new List<SVsIEnemy>[Rows, Cols];
            Towers = new SVsITower[Rows, Cols];
        }

        private void CheckGameOver()
        {

        }

        public void UpgradeTower(int row, int col)
        {

        }

        public void PlaceTower(int row, int col)
        {

        }

        public void SellTower(int row, int col)
        {

        }

        private void DestroyTower(int row, int col)
        {

        }

        /* egyelore nem jovok ra, hogy ez miert nem helyes
        private SVsIEnemy TowerCanAttack(SVsITower Tower)
        {

        }
        */

        public void UpgradeCastle()
        {

        }


    }
}