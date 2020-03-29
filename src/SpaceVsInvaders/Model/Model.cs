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

        public WaveSpawner WS;

#region Events
        public event EventHandler<SVsIEventArgs> EnemyMoved;
        public event EventHandler<SVsIEventArgs> TowerHasAttacked;
        public event EventHandler<SVsIEventArgs> EnemyDead;
        public event EventHandler<SVsIEventArgs> TowerDestroyed;
        public event EventHandler<SVsIEventArgs> GameOver;

         public void onEnemyMoved(int fromX, int fromY, int toX, int toY)
        {
            if(EnemyMoved != null)
            {
                EnemyMoved(this, new SVsIEventArgs(fromX, fromY, toX, toY));
            }
        }
        public void onTowerHasAttacked(int fromX, int fromY, int toX, int toY)
        {
            if(TowerHasAttacked != null)
            {
                TowerHasAttacked(this, new SVsIEventArgs(fromX, fromY, toX, toY));
            }
        }
        public void onTowerDestroyed(int whereX, int whereY)
        {
            if(TowerDestroyed != null)
            {
                TowerDestroyed(this, new SVsIEventArgs(whereX, whereY));
            }
        }
        public void onEnemyDead(int whereX, int whereY)
        {
            if(EnemyDead != null)
            {
                EnemyDead(this, new SVsIEventArgs(whereX, whereY));
            }
        }
        public void onGameOver()
        {
            if(GameOver != null)
            {
                GameOver(this, new SVsIEventArgs(true));
            }
        }
#endregion

        public SVsIModel()
        {
            NewGame();
        }

        public void HandleTick()
        {
            HandleTowers();
            HandleEnemies();
            CheckGameOver();
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
                        onTowerHasAttacked(row, col, i, col);
                        if(e.Health <= 0)
                        {
                            Enemies[i,col].Remove(e);
                            onEnemyDead(i,col);
                        }
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
                                /* //a tower előtti mezőről tud csak sebezni, amíg odaér addig "készenállhat harcra"
                                if(Towers[i+1,j].GetType().ToString() != "SVsITower")
                                {
                                     Towers[k,j].Health -= e.Damage;
                                }else e.CoolDown = e.TickTime;
                                */
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
                                onEnemyMoved(j,i, j,i+1);
                            }
                        }   
                    }
        }

        public void NewGame() 
        {
            Money = 0;
            SecondsElapsed = 0;
            Castle = new SVsICastle();
            Enemies = new List<SVsIEnemy>[Rows, Cols];
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
                        {
                            IsGameOver = true;
                            onGameOver();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ha a játékosnak van elég pénze, akkor a kiválasztot tornyot fejleszti.
        /// </summary>
        public void UpgradeTower(int row, int col)
        {
            if(Money >= 150 + Towers[row, col].Level * 50) // ezt is majd config fájlból át kell írni
            Towers[row, col].Health += 10;
            Towers[row,col].Level += 1;
        }

        /// <summary>
        /// Ha a játékosnak van elég pénze, akkor lerak egy előre kiválaszott típusú tornyot a kiválasztott mezőre.
        /// </summary>
        public bool PlaceTower(int row, int col, TowerType type)
        {
            switch(type)
            {
                case TowerType.Damage:
                    if(Money >= 150) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= 150;
                        Towers[row,col] = new SVsIDamageTower();
                        return true;
                    }else return false;
                case TowerType.Gold:
                    if(Money >= 150) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= 150;
                        Towers[row,col] = new SVsIGoldTower();
                        return true;
                    }else return false;
                case TowerType.Heal:
                     if(Money >= 150) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= 150;
                        Towers[row,col] = new SVsIHealTower();
                        return true;
                    }else return false;
                default:
                    return false;
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
            onTowerDestroyed(row, col);
        }

        /*    
        private SVsIEnemy TowerCanAttack(SVsITower Tower) ?en ez mar nem tudom, hogy mit csinal 
        {
            
        }
        */
    
        /// <summary>
        /// Ha van elég pénze a játékosnak, akkor fejleszti a kastélyt. (Ha sikeres a fejlesztés akkor igazat ad vissza.)
        /// </summary>
        public bool UpgradeCastle()
        {
            if(Money >= Castle.UpgradeCost)
            {
                Money -= Castle.UpgradeCost;
                Castle.UpgradeCost += Castle.Level * 100;
                Castle.Level += 1;
                Castle.Health += Castle.Level * 10;
                return true;
            }else return false;
        }
    }
}