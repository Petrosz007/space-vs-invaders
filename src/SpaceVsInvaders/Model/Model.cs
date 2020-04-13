using System;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    public class SVsIModelException : Exception 
    {
        public SVsIModelException(string message)
            : base(message) 
        {}
    }
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
    public enum CatastropheType
    {
        Healing,
        Asteroid
    }

    public class SVsIModel
    {
        public int Money { get; set; }
        public int SecondsElapsed { get; private set; }
        public int TowerCounter { get; private set; }
        public int TowerUpdates { get; private set; }
        
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
        public event EventHandler<SVsIEventArgs> EnemyMovedToCastle;
        public event EventHandler<SVsIEventArgs> HealingCatastrophe;
        public event EventHandler<SVsIEventArgs> AsteroidCatastrophe;

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
        public void onEnemyMovedToCastle(int whereX, int whereY, EnemyType type)
        {
            if(EnemyMovedToCastle != null)
            {
                EnemyMovedToCastle(this, new SVsIEventArgs(whereX, whereY, type));
            }
        }
        public void onHealingCatastrophe(int whereX, int whereY)
        {
            if(HealingCatastrophe != null)
            {
                HealingCatastrophe(this, new SVsIEventArgs(whereX, whereY));
            }
        }
        
        public void onAsteroidCatastrophe(int whereX, int whereY)
        {
            if(AsteroidCatastrophe != null)
            {
                AsteroidCatastrophe(this, new SVsIEventArgs(whereX, whereY));
            }
        }
#endregion

        public SVsIModel()
        {
            WS = new WaveSpawner();
            IsSpawningEnemies = true;
            TowerCounter = 0;
        }

        public void HandleTick()
        {
            Money += 1;
            SecondsElapsed += 1;
            if(SecondsElapsed % 20 == 0 && SecondsElapsed != 0 && IsSpawningEnemies)
            {
                WS.SpawnEnemies(SecondsElapsed, Cols, TowerCounter,TowerUpdates);
            }

            //? Lehet hogy vissza kell cserélni a sorrendet ha bugos
            HandleEnemies();
            HandleTowers();
            Catastrophe();
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
        public void HandleHealTower(int row, int col)
        {
            if(Towers[row, col] is SVsIHealTower healTower)
            {
                int range = healTower.Range;
                for(int i = row - range; i <= row + range; ++i)
                {
                    for(int j = col - range; j <= col + range; ++j)
                    {
                        if(i < 0 || j < 0 || i >= Rows || j >= Cols) continue;

                        var tower = Towers[i, j];
                        if(tower != null)
                        {
                            int healedHealth = tower.Health + healTower.Heal();
                            tower.Health = (healedHealth < tower.MaxHealth) ? healedHealth : tower.MaxHealth;
                        }
                    }

                }
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
                        for( int k = 0; k < Enemies[i,j].Count; k++)
                        {
                            if (Enemies[i,j][k].CoolDown == 0 && i+1 < Rows)
                            {

                                  if (Towers[i+1,j] is SVsITower)
                                {
                                    Towers[i+1,j].Health -= Enemies[i,j][k].Damage;
                                    if (Towers[i+1,j].Health <= 0)
                                        Towers[i+1,j] = null; // ne menjen minuszba a health
                                    break;   
                                }
                            
                                Enemies[i,j][k].CoolDown = Enemies[i,j][k].TickTime;
                            }
                            else
                            {
                                Enemies[i,j][k].CoolDown -= 1;
                            }

                            if (SecondsElapsed % Enemies[i,j][k].Movement == 0) // ha a kovetkezo sorban torony van, akkor ne masszon ra
                            {
                                if( i+1 < Rows && null == Towers[i+1,j])
                                {
                                    if (null == Enemies[i+1,j])
                                    {
                                        Enemies[i+1,j] = new List<SVsIEnemy>();
                                    }
                                    //! turn back enemy moving
                                    Enemies[i+1,j].Add(Enemies[i,j][k]);
                                    Enemies[i,j].Remove(Enemies[i,j][k]);
                                    onEnemyMoved(j,i, j,i+1);
                                }
                                else if (i+1 == Rows)
                                {
                                    Castle.Health -= 1;
                                    if(Enemies[i,j][k] is SVsIBuffEnemy)
                                        onEnemyMovedToCastle(i,j, EnemyType.Buff);
                                    if (Enemies[i,j][k] is SVsINormalEnemy)
                                        onEnemyMovedToCastle(i,j, EnemyType.Normal);
                                    if (Enemies[i,j][k] is SVsISpeedyEnemy)
                                        onEnemyMovedToCastle(i,j, EnemyType.Speedy);
                                    Enemies[i,j].Remove(Enemies[i,j][k]);
                                }
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
            Money = 0;
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
        public void UpgradeTower(int row, int col)
        {
            var tower = Towers[row, col] ?? throw new SVsIModelException("No tower selected.");

            if(Money >= tower.UpgradeCost)
            {
                TowerUpdates++;
                Money -= tower.UpgradeCost;
                tower.Upgrade();
            }
            else
            {
                throw new SVsIModelException("No money for the upgrade.");
            }
        }

        /// <summary>
        /// Ha a játékosnak van elég pénze, akkor lerak egy előre kiválaszott típusú tornyot a kiválasztott mezőre.
        /// </summary>
        public void PlaceTower(int row, int col, TowerType type)
        {
            if(Towers[row, col] != null) 
                throw new SVsIModelException("Tower already there.");

            SVsITower tower = type switch
            {
                TowerType.Damage => new SVsIDamageTower(),
                TowerType.Gold   => new SVsIGoldTower(),
                TowerType.Heal   => new SVsIHealTower(),
            };

            if(Money >= tower.Cost)
            {
                TowerCounter++;
                Money -= tower.Cost;
                Towers[row,col] = tower;
            }
            else
            {
                throw new SVsIModelException("Not enough money for new tower.");
            }
        }

        /// <summary>
        ///Eladja az adott tornyot, es a torony értékénel felével növel a pénzt.
        /// </summary>
        public void SellTower(int row, int col)
        {
            var tower = Towers[row, col] ?? throw new SVsIModelException("No tower selected.");

            Money += tower.SellCost;
            Towers[row, col] = null;
            TowerCounter--;
        }

        /// <summary>
        /// Leromboljuk az adott tornyot.
        /// </summary>
        private void DestroyTower(int row, int col)
        {
            Towers[row, col] = null;
            onTowerDestroyed(row, col);
            TowerCounter--;
        }
    
        /// <summary>
        /// Ha van elég pénze a játékosnak, akkor fejleszti a kastélyt. (Ha sikeres a fejlesztés akkor igazat ad vissza.)
        /// </summary>
        public void UpgradeCastle()
        {
            int upgradeCost = Castle.UpgradeCost * Castle.Level;

            if(Money >= Castle.CurrentUpgradeCost)
            {
                Money -= Castle.CurrentUpgradeCost;
                Castle.Upgrade();
            }
            else
            {
                throw new SVsIModelException("No money for castle upgrade.");
            }
        }

         /// <summary>
        ///  Katasztrófákat generál.
        /// </summary>
        public void Catastrophe()
        {
            Random rnd = new Random();
            int szam = rnd.Next(200);
            if (szam < 10)
            {
                for (int j = 0; j < 3; j++)
                {
                    int dmg = rnd.Next(20,50);
                    Coordinate tmp = generateCoordinates();
                    HandleAsteroidCatastrophe(tmp.X, tmp.Y, dmg);
                    onAsteroidCatastrophe(tmp.X, tmp.Y);
                }
            }
            else if (szam > 190)
            {
                    for (int j = 0; j < 3; j++)
                {
                    int heal = rnd.Next(20,50);
                    Coordinate tmp = generateCoordinates();
                    HandleHealingCatastrophe(tmp.X, tmp.Y, heal);
                    onHealingCatastrophe(tmp.X, tmp.Y);
                }
            }
        }
        public void HandleAsteroidCatastrophe(int i, int j, int dmg)
        {
            if (null != Enemies[i,j])
            {
                if (null != Towers[i,j])
                {
                    Towers[i,j].Health -= dmg;
                    if(Towers[i,j].Health <= 0)
                    {
                        DestroyTower(i,j);
                    }
                }
                for( int k = 0; k < Enemies[i,j].Count; k++)
                {
                    Enemies[i,j][k].Health -= 20;
                    if(Enemies[i,j][k].Health <= 0)
                    {
                        Enemies[i,j].Remove(Enemies[i,j][k]);
                        onEnemyDead(i,j);
                    }
                }
            }
        }
        public void HandleHealingCatastrophe(int i, int j, int heal)
        {
            if (null != Towers[i,j])
            {
                int healedHealth = Towers[i,j].Health + heal;
                Towers[i,j].Health = (healedHealth < Towers[i,j].MaxHealth) ? healedHealth : Towers[i,j].MaxHealth;                
            }
            if (null != Enemies[i,j])
            {
                for( int k = 0; k < Enemies[i,j].Count; k++)
                {
                    int healedHealth = Enemies[i,j][k].Health + heal;
                    Enemies[i,j][k].Health = (healedHealth < Enemies[i,j][k].MaxHealth) ? healedHealth : Enemies[i,j][k].MaxHealth;    
                }
            }
        }
        public Coordinate generateCoordinates()
        {
            Random rnd = new Random();
            int y = rnd.Next(0,Cols); 
            int x = rnd.Next(0,Rows); 
            return new Coordinate(x,y);
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