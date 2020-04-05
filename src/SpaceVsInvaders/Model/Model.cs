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
    
    public enum EnemyType
    {
        Buff,
        Normal,
        Speedy
    }

    public class SVsIModel
    {
        public int Money { get; set; }
        public int SecondsElapsed { get; private set; }
        
        public List<SVsIEnemy>[,] Enemies;

        public int Rows { get; set; }

        public int Cols { get; set; }

        public SVsITower[,] Towers;

        public int Difficulty { get; private set; }

        public SVsICastle Castle;

        public bool IsGameOver { get; private set; }

        public WaveSpawner WS;
        public bool IsSpawningEnemies { get; private set; }

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
            WS = new WaveSpawner();
            IsSpawningEnemies = true;
        }

        public void HandleTick()
        {
            Money += 1;
            SecondsElapsed += 1;
            if(SecondsElapsed % 10 == 0 && SecondsElapsed != 0 && IsSpawningEnemies)
            {
                WS.SpawnEnemies(SecondsElapsed, Cols);
            }

            //? Lehet hogy vissza kell cserélni a sorrendet ha bugos
            HandleEnemies();
            HandleTowers();

            CheckGameOver();
        }

        /// <summary>
        /// Tornyok lekezelése. (Gyógyítás, ellenség lövése, pénzmennyiség növelése.)
        /// </summary>
        private void HandleTowers()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                if (null != Towers[i,j])
                {
                    if (Towers[i,j].CoolDown == 0)
                    {
                        
                        if (Towers[i,j] is SVsIDamageTower)
                        {
                            HandleDamageTower(i,j);
                        }
                        if (Towers[i,j] is SVsIGoldTower)
                        {
                            HandleGoldTower(i,j);
                        }
                        if (Towers[i,j] is SVsIHealTower)
                        {
                            HandleHealTower(i,j);
                        }
                        Towers[i,j].CoolDown = Towers[i,j].TickTime;
                    }
                    else
                    {
                        Towers[i,j].CoolDown -= 1;
                    }
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
                if (Enemies[i,col].Count != 0 && Towers[row,col].Range >= row-1-i)
                {
                    if(Towers[row, col] is SVsIDamageTower damageTower)
                        {
                            Enemies[i,col][0].Health -= damageTower.Damage();
                            onTowerHasAttacked(row, col, i, col);
                        
                            if(Enemies[i,col][0].Health <= 0)
                            {
                                Enemies[i,col].Remove(Enemies[i,col][0]);
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
            if(Towers[row, col] is SVsIGoldTower goldTower)
            {
                Money += goldTower.Gold();
            }
        }

        /// <summary>
        /// Önnönmaga 3x3-as környezetében emeli minden torony Health-jét.
        /// </summary>
        public void HandleHealTower(int row, int col) // teszt miatt public
        {
            if(Towers[row, col] is SVsIHealTower healTower)
            {
                if (row-1 >= 0 && col-1 >= 0 && null != Towers[row-1, col-1])
                    Towers[row-1, col-1].Health += healTower.Heal();

                if (row-1 >= 0 && null !=  Towers[row-1, col])
                    Towers[row-1, col].Health += healTower.Heal();

                if (row-1 >= 0 && col+1 < Cols && null != Towers[row-1, col+1])
                    Towers[row-1, col+1].Health += healTower.Heal();

                if (col-1 >= 0 && null !=  Towers[row, col-1])
                    Towers[row, col-1].Health += healTower.Heal();

                if (col+1 < Cols && null !=  Towers[row, col+1])
                    Towers[row, col+1].Health += healTower.Heal();

                if (row+1 < Rows && col-1 >= 0 && null != Towers[row+1, col-1])
                    Towers[row+1, col-1].Health += healTower.Heal();

                if (row+1 < Rows && null != Towers[row+1, col])
                    Towers[row+1, col].Health += healTower.Heal();

                if (row+1 < Rows && col+1 < Cols && null !=  Towers[row+1, col+1])
                    Towers[row+1, col+1].Health += healTower.Heal();
            }
        }

        
        /// <summary>
        /// Kiemeltem a placeEnemy hívást
        /// </summary>
        public void WhichEnemy(EnemyType type, int i)
        {
            if(type is EnemyType.Normal) PlaceEnemy(0,i,EnemyType.Normal);
            if(type is EnemyType.Buff) PlaceEnemy(0,i,EnemyType.Buff);
            if(type is EnemyType.Speedy) PlaceEnemy(0,i,EnemyType.Speedy);
        }

        /// <summary>
        /// Ellenségek lövésének, mozgatásának lekezelése.
        /// </summary>
        private void HandleEnemies()
        {
            //GenerateEnemy();
            for (int i = Rows-1; i >= 0; i--)
                for (int j = Cols-1; j >=  0; j--)
                    if (null != Enemies[i,j])
                    {
                        for( int l = 0; l < Enemies[i,j].Count; l++)
                        {
                            if (Enemies[i,j][l].CoolDown == 0)
                            {
                                
                                for (int k = i; k < Rows; k++)
                                    if (Towers[k,j] is SVsITower)
                                    {
                                        Towers[k,j].Health -= Enemies[i,j][l].Damage;
                                        if (Towers[k,j].Health <= 0)
                                            Towers[k,j] = null; // ne menjen minuszba a health
                                        break;
                                    }
                                Enemies[i,j][l].CoolDown = Enemies[i,j][l].TickTime;
                            }
                            else
                            {
                                Enemies[i,j][l].CoolDown -= 1;
                            }

                            if (SecondsElapsed % Enemies[i,j][l].Movement == 0 && i+1 < Rows && null == Towers[i+1,j]) // ha a kovetkezo sorban torony van, akkor ne masszon ra
                            {
                                if (null == Enemies[i+1,j])
                                {
                                    Enemies[i+1,j] = new List<SVsIEnemy>();
                                }

                                //! turn back enemy moving
                                 Enemies[i+1,j].Add(Enemies[i,j][l]);
                                 Enemies[i,j].Remove(Enemies[i,j][l]);
                                 onEnemyMoved(j,i, j,i+1);
                            }
                        }   
                    }
            if(WS.AreEnemiesLeft() && SecondsElapsed % 3 == 0) // itt kell megadni, hány másodpercenként jelenjenek meg, hogy az előző adag elmozduljon, mire ez bejátszódik
            {
                List<EnemyType> tmp = new List<EnemyType>(); // ez innen eltűnik? xd
                tmp = WS.GetSpawnedEnemies(Cols);

                if(tmp.Count > Cols-1)
                {
                    int i = 0;
                    while(tmp.Count > i && i < Cols)
                    {
                        WhichEnemy(tmp[i], i);
                        i++;
                    }
                }else{
                    HashSet<int> ind = new HashSet<int>();
                    ind.Clear();
                    for(int j = 0; j < Cols; j++) ind.Add(j);
                    Random rnd = new Random();
                    int i = 0;
                    while(tmp.Count > i && i < Cols)
                    {
                        int szam = rnd.Next(ind.Count);
                        WhichEnemy(tmp[i], szam);
                        i++;
                    }
                }
            }

        }

        public void NewGame(int rows, int cols) 
        {
            Money = 80;
            SecondsElapsed = 0;
            Castle = new SVsICastle();
            Rows = rows;
            Cols = cols;
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
        public bool UpgradeTower(int row, int col)
        {
            var tower = Towers[row, col];
            int upgradeCost = tower.Cost + tower.Level * 50;

            if(Money >= upgradeCost)
            {
                Money -= upgradeCost;
                tower.Health += 50;
                tower.MaxHealth += 50;
                tower.Level++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ha a játékosnak van elég pénze, akkor lerak egy előre kiválaszott típusú tornyot a kiválasztott mezőre.
        /// </summary>
        public bool PlaceTower(int row, int col, TowerType type)
        {
            int damageCost = Config.GetValue<TowerConfig>("DamageTower").Cost;
            int goldCost   = Config.GetValue<TowerConfig>("GoldTower").Cost;
            int healCost   = Config.GetValue<TowerConfig>("HealTower").Cost;

            switch(type)
            {
                // TODO: a tornyoknal van kulon cost propertyjuk
                case TowerType.Damage:
                    if(Money >= damageCost) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= damageCost;
                        Towers[row,col] = new SVsIDamageTower();
                        return true;
                    }
                    return false;

                case TowerType.Gold:
                    if(Money >= goldCost) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= goldCost;
                        Towers[row,col] = new SVsIGoldTower();
                        return true;
                    }
                    return false;

                case TowerType.Heal:
                     if(Money >= healCost) // ezt majd ki kell cserélni a config-ből kiolvasott értékekre!!!
                    {
                        Money -= healCost;
                        Towers[row,col] = new SVsIHealTower();
                        return true;
                    }
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        ///Eladja az adott tornyot, es a torony értékénel felével növel a pénzt.
        /// </summary>
        public void SellTower(int row, int col)
        {
            Money += Towers[row, col].Cost/2;
            Towers[row, col] = null;
        }

        /// <summary>
        /// Leromboljuk az adott tornyot.
        /// </summary>
        private void DestroyTower(int row, int col)
        {
            Towers[row, col] = null;
            onTowerDestroyed(row, col);
        }
    
        /// <summary>
        /// Ha van elég pénze a játékosnak, akkor fejleszti a kastélyt. (Ha sikeres a fejlesztés akkor igazat ad vissza.)
        /// </summary>
        public bool UpgradeCastle()
        {
            int upgradeCost = Castle.UpgradeCost * Castle.Level;

            if(Money >= upgradeCost)
            {
                Money -= upgradeCost;
                Castle.Level += 1;
                Castle.Health += Castle.Level * 10;
                return true;
            }
            
            return false;
        }

       /*
        public void GenerateEnemy()
        {
            if (SecondsElapsed % 3 == 0) // fontos, hogy ezt most csak a teszt vegett raktam ennyire, ez a valosagban inkabb 7-15 mp
            {
                Random rnd = new Random();
                int number = rnd.Next(0,3);
                int col = rnd.Next(0, Cols);
                if (Enemies[0,col] == null)
                {
                    Enemies[0,col] = new List<SVsIEnemy>();
                    if (number == 0)
                    {
                        Enemies[0,col].Add(new SVsIBuffEnemy());
                    }
                    if (number == 1)
                    {
                        Enemies[0,col].Add(new SVsINormalEnemy());
                    }
                    if (number == 2)
                    {
                        Enemies[0,col].Add(new SVsISpeedyEnemy());
                    }
                }
            }
        }
        */

        ///<summary>
        /// Csak a teszteléshez kell.
        ///</summary>
        public void PlaceEnemy(int row, int col, EnemyType enemyType)
        {
            Enemies[row,col] ??= new List<SVsIEnemy>();

            SVsIEnemy enemyToPlace = enemyType switch
            {
                EnemyType.Buff   => new SVsIBuffEnemy(),
                EnemyType.Normal => new SVsINormalEnemy(),
                EnemyType.Speedy => new SVsISpeedyEnemy()
            };

            Enemies[row,col].Add(enemyToPlace);
        }
    }
}