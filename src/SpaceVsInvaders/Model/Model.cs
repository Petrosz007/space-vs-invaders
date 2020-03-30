using System;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    public enum TowerType
    {
        Damage,
        Gold,
        Heal
    }
    
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

        public bool IsGameOver { get; private set; }

        public SVsIModel()
        {
        }

        public void HandleTick()
        {
            HandleTowers();
            HandleEnemies();
        }

        private void HandleTowers()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                {
                    if (Towers[i,j].CoolDown == 0)
                    {
                        switch (Towers[i,j].GetType().ToString())
                        {
                            case "SVsIDamageTower":
                                HandleDamageTower(i,j);
                                break;
                            case "SVsIGoldTower":
                                HandleGoldTower(i,j);
                                break;
                            case "SVsIHealTower":
                                HandleHealTower(i,j);
                                break;
                            default:
                                break;
                        }
                        Towers[i,j].CoolDown = Towers[i,j].TickTime;
                    }
                    else
                    {
                        Towers[i,j].CoolDown -= 1;
                    }
                }
        }

        /// <summary>
        ///  Megkeresi a hatótávolságában hozzá legközelebb eső ellenség(eket), és ennek (ezeknek) Health-jét csökkenti.
        /// </summary>
        private void HandleDamageTower(int row, int col)
        {
            for(int i = row-1; i >= 0; i--)
            {
                if (Enemies[i,col] != null && Towers[row,col].Range >= i)
                {
                    foreach(SVsIEnemy e in Enemies[i,col])
                    {
                        e.Health -= Towers[row, col].Damage();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// A játékos pénzének mennyiségét megemeli a fejlettségi szint függvényében.
        /// </summary>
        private void HandleGoldTower(int row, int col)
        {
            Money += Towers[row, col].Gold();
        }

        /// <summary>
        /// Önnönmaga 3x3-as környezetében emeli minden torony Health-jét.
        /// </summary>
        private void HandleHealTower(int row, int col)
        {
            if (row-1 >= 0 && col-1 >= 0)
                Towers[row-1, col-1].Health += Towers[row, col].Heal();

            if (row-1 >= 0)
                Towers[row-1, col].Health += Towers[row, col].Heal();

            if (row-1 >= 0 && col+1 < Cols)
                Towers[row-1, col+1].Health += Towers[row, col].Heal();

            if (col-1 >= 0)
                Towers[row, col-1].Health += Towers[row, col].Heal();

            if (col+1 < Cols)
                Towers[row, col+1].Health += Towers[row, col].Heal();

            if (row+1 < Rows && col-1 >= 0)
                Towers[row+1, col-1].Health += Towers[row, col].Heal();

            if (row+1 < Rows)
                Towers[row+1, col].Health += Towers[row, col].Heal();

            if (row+1 < Rows && col+1 < Cols)
                Towers[row+1, col+1].Health += Towers[row, col].Heal();
        }

        private void HandleEnemies()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    if (Enemies[i,j] != null)
                    {
                        foreach(SVsIEnemy e in Enemies[i,j])
                        {
                            if (e.CoolDown == 0)
                            {
                                for (int k = i; k < Rows; k++)
                                    if (Towers[k,j].GetType().ToString() != "SVsITower")
                                    {
                                        Towers[k,j].Health -= e.Damage;
                                        break;
                                    }
                                e.CoolDown = e.TickTime;
                            }
                            else
                            {
                                e.CoolDown -= 1;
                            }

                            if (SecondsElapsed % e.Movement == 0 && i+1 < Rows)
                            {
                                Enemies[i+1,j].Add(e);
                                Enemies[i,j].Remove(e);
                            }
                        }   
                    }
        }

        public void NewGame(int rows, int cols) 
        {
            Rows = rows;
            Cols = cols;

            Money = 0;
            SecondsElapsed = 0;
            Castle = new SVsICastle();
            Enemies = new List<SVsIEnemy>[Rows, Cols];

            for(int i = 0; i < Rows; ++i)
                for(int j = 0; j < Cols; ++j)
                    Enemies[i,j] = new List<SVsIEnemy>();

            Towers = new SVsITower[Rows, Cols];
            IsGameOver = false;
        }

        private void CheckGameOver()
        {
            /// <remarks>
            /// Amikor a var eletereje 0-ra csokkent mar
            /// </remarks>
            if (Castle.Health == 0)
                IsGameOver = true;

            /// <remarks>
            /// Amikor az ellenseg kozvetlenul a var elott van, es a kovetkezo lepesevel belepne a varba
            /// </remarks>
            for (int j = 0; j < Cols; j++)
            {
                if (Enemies[Rows-1,j] != null)
                {
                    foreach(SVsIEnemy e in Enemies[Rows-1,j])
                    {
                        if (SecondsElapsed % e.Movement == 0)
                            IsGameOver = true;
                    }
                }
            }
        }

        public void UpgradeTower(int row, int col)
        {
            Towers[row, col].Health += 10;
            Towers[row,col].Level += 1;
        }

        public void PlaceTower(int row, int col, TowerType type)
        {
            switch(type)
            {
                case TowerType.Damage:
                    Towers[row,col] = new SVsIDamageTower();
                    break;
                case TowerType.Gold:
                    Towers[row,col] = new SVsIGoldTower();
                    break;
                case TowerType.Heal:
                    Towers[row,col] = new SVsIHealTower();
                    break;
                default:
                    break;
            }
        }

        public void SellTower(int row, int col)
        {
            Money += Towers[row, col].Cost/2;
            Towers[row, col] = null;
        }

        private void DestroyTower(int row, int col)
        {
            Towers[row, col] = null;
        }

        /*    
        private SVsIEnemy TowerCanAttack(SVsITower Tower) ?en ez mar nem tudom, hogy mit csinal 
        {
            
        }
        */

        public void UpgradeCastle()
        {
            Castle.Level += 1;
            Castle.Health += Castle.Level * 10;
        }
    }
}