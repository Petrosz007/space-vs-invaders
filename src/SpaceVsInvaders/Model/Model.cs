using System;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    /// <summary>
    /// The model's own kind of exception.
    /// </summary>
    public class SVsIModelException : Exception 
    {
        /// <summary>
        /// The exception's constuctor.
        /// </summary>
        /// <param name="message">Custom message reasoning the exception's thrown.</param>
        public SVsIModelException(string message)
            : base(message) 
        {}
    }
    /// <summary>
    /// Enumeration of possible tower types.
    /// </summary>
    public enum TowerType
    {
        /// <summary>
        /// Tower that can damage the enemy.
        /// </summary>
        Damage,
        /// <summary>
        /// Tower that produces money for the player.
        /// </summary>
        Gold,
        /// <summary>
        /// Tower that can heal other towers.
        /// </summary>
        Heal
    }
    /// <summary>
    /// Enumeration of possible enemy types.
    /// </summary>
    public enum EnemyType
    {
        /// <summary>
        /// Slow but harmful enemy.
        /// </summary>
        Buff,
        /// <summary>
        /// Avarage enemy.
        /// </summary>
        Normal,
        /// <summary>
        /// Fast but less harmful enemy.
        /// </summary>
        Speedy
    }
    /// <summary>
    /// Enumeration of possible catasrophes.
    /// </summary>
    public enum CatastropheType
    {
        /// <summary>
        /// Incase this type of disaster occurs, a randomly picked tower or a group of enemies standing on the same field will be healed.
        /// </summary>
        Healing,
        /// <summary>
        /// Incase this type of disaster occurs, a randomly picked tower or a group of enemies standing on the same field will be hurt or exterminated. 
        /// </summary>
        Asteroid
    }

    /// <summary>
    /// The model's class.
    /// </summary>
    public class SVsIModel
    {
        /// <summary>
        /// Players current amount of money.
        /// </summary>
        /// <value> Money </value>
        public int Money { get; set; }
        /// <summary>
        /// Seconds elapsed since the game has started.
        /// </summary>
        /// <value> SecondsElapsed </value>
        public int SecondsElapsed { get; private set; }
        /// <summary>
        ///  Interval of the enemies' reinforcement.
        /// </summary>
        /// <value> ReinforceTimes </value>
        public int ReinforceTimes { get; private set; }
        /// <summary>
        /// Tower counter.
        /// </summary>
        /// <value> TowerCounter </value>
        public int TowerCounter { get; private set; }
        /// <summary>
        /// Amount of tower updates.
        /// </summary>
        /// <value>TowerUpdates</value>
        public int TowerUpdates { get; private set; }
        /// <summary>
        /// Board of enemy lists.
        /// </summary>
        public List<SVsIEnemy>[,] Enemies;
        /// <summary>
        /// Heigth of the gametable.
        /// </summary>
        /// <value>Rows</value>
        public int Rows { get; set; }

        /// <summary>
        /// Width of the gametable.
        /// </summary>
        /// <value>Cols</value>
        public int Cols { get; set; }
        /// <summary>
        /// Board of towers.
        /// </summary>
        public SVsITower[,] Towers;

        /// <summary>
        /// Difficulty of the game.
        /// </summary>
        /// <value>Difficulty</value>
        public int Difficulty { get; private set; }

        /// <summary>
        /// Player's castle.
        /// </summary>
        public SVsICastle Castle;

        /// <summary>
        /// Is game over.
        /// </summary>
        /// <value> IsGameOver </value>
        public bool IsGameOver { get; private set; }

        /// <summary>
        /// WaveSpawner.
        /// </summary>
        public WaveSpawner WS;

        /// <summary>
        /// Wavespawner is spawning enemies.
        /// </summary>
        /// <value> IsSpawningEnemies </value>
        public bool IsSpawningEnemies { get; set; }

        /// <summary>
        /// Catasrophes can occur.
        /// </summary>
        /// <value></value>
        public bool IsCatastrophe { get; set; }

#region Events
        /// <summary>
        /// Enemy movement event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> EnemyMoved;
        /// <summary>
        /// Tower attack event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> TowerHasAttacked;
        /// <summary>
        /// Enemy exterminated event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> EnemyDead;
        /// <summary>
        /// Tower destroyed event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> TowerDestroyed;
        /// <summary>
        /// Game over event.
        /// </summary>
        public event EventHandler<bool> GameOver;
        /// <summary>
        /// Enemy entered the castle event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> EnemyMovedToCastle;
        /// <summary>
        /// Healing catastrophe occured event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> HealingCatastrophe;
        /// <summary>
        /// Asteroid catastrophe occured event.
        /// </summary>
        public event EventHandler<SVsIEventArgs> AsteroidCatastrophe;

        /// <summary>
        /// Enemy movement event sender.
        /// </summary>
        /// <param name="fromX"> The row it moved from.</param>
        /// <param name="fromY"> The coloumn it moved from. </param>
        /// <param name="toX">  The row it moved to.</param>
        /// <param name="toY">  The coloumn it moved to. </param>
        public void onEnemyMoved(int fromX, int fromY, int toX, int toY)
        {
            if(EnemyMoved != null)
            {
                EnemyMoved(this, new SVsIEventArgs(fromX, fromY, toX, toY));
            }
        }
        /// <summary>
        /// Tower attacked event sender.
        /// </summary>
        /// <param name="fromX"> The row it has attacked from.</param>
        /// <param name="fromY"> The coloumn it has attacked from. </param>
        /// <param name="toX">  The row of the enemy it has attacked.</param>
        /// <param name="toY">  The coloumn of the enemy it has attacked. </param>
        public void onTowerHasAttacked(int fromX, int fromY, int toX, int toY)
        {
            if(TowerHasAttacked != null)
            {
                TowerHasAttacked(this, new SVsIEventArgs(fromX, fromY, toX, toY));
            }
        }
        /// <summary>
        /// Tower destroyed event sender.
        /// </summary>
        /// <param name="whereX"> The row where it has been destroyed. </param>
        /// <param name="whereY"> The coloumn where it has been destroyed. </param>
        public void onTowerDestroyed(int whereX, int whereY)
        {
            if(TowerDestroyed != null)
            {
                TowerDestroyed(this, new SVsIEventArgs(whereX, whereY));
            }
        }
        /// <summary>
        /// Enemy exterminated event sender.
        /// </summary>
        /// <param name="whereX"> The row where it has been exterminated. </param>
        /// <param name="whereY"> The coloumn where it has been exterminated. </param>
        public void onEnemyDead(int whereX, int whereY)
        {
            if(EnemyDead != null)
            {
                EnemyDead(this, new SVsIEventArgs(whereX, whereY));
            }
        }
        /// <summary>
        /// Game over event sender.
        /// </summary>
        /// <param name="victory"> Whether the game has been won or lost. </param>
        public void onGameOver(bool victory)
        {
            IsGameOver = true;
            if(GameOver != null)
            {
                GameOver(this, victory);
            }
        }
        /// <summary>
        /// Enemy moved to castle event sender.
        /// </summary>
        /// <param name="whereX"> The row it moved to the castle from.</param>
        /// <param name="whereY"> The coloumn it moved to the castle from.</param>
        /// <param name="type"> The type of the enemy that moved to the castle. </param>
        public void onEnemyMovedToCastle(int whereX, int whereY, EnemyType type)
        {
            if(EnemyMovedToCastle != null)
            {
                EnemyMovedToCastle(this, new SVsIEventArgs(whereX, whereY, type));
            }
        }
        /// <summary>
        /// Healing catasrophe event sender.
        /// </summary>
        /// <param name="whereX"> The row where it occurs. </param>
        /// <param name="whereY"> The coloumn where it occurs. </param>
        public void onHealingCatastrophe(int whereX, int whereY)
        {
            if(HealingCatastrophe != null)
            {
                HealingCatastrophe(this, new SVsIEventArgs(whereX, whereY));
            }
        }

        /// <summary>
        /// Asteroid catasrophe event sender.
        /// </summary>
        /// <param name="whereX"> The row where it occurs. </param>
        /// <param name="whereY"> The coloumn where it occurs. </param>
        public void onAsteroidCatastrophe(int whereX, int whereY)
        {
            if(AsteroidCatastrophe != null)
            {
                AsteroidCatastrophe(this, new SVsIEventArgs(whereX, whereY));
            }
        }
#endregion
        /// <summary>
        /// Model's constructor.
        /// </summary>
        public SVsIModel()
        {
            WS = new WaveSpawner();
            IsSpawningEnemies = true;
            IsCatastrophe = true;
            TowerCounter = 0;
            ReinforceTimes = 0;
        }

        /// <summary>
        /// Handler of time passing.
        /// </summary>
        public void HandleTick()
        {
            Money += 1;
            SecondsElapsed += 1;
            if(SecondsElapsed % Config.GetValue<int>("WaveInterval") == 0 && SecondsElapsed != 0 && IsSpawningEnemies)
            {
                WS.SpawnEnemies(SecondsElapsed, Cols, TowerCounter,TowerUpdates,ReinforceTimes);
            }
            if (SecondsElapsed % Config.GetValue<int>("ReinforceInterval") == 0)
            {
                ReinforceTimes++;
            }

            //? Lehet hogy vissza kell cserélni a sorrendet ha bugos
            HandleEnemies();
            HandleTowers();
            if(IsCatastrophe)
            {
                Catastrophe();
            }
            CheckGameOver();
        }

        /// <summary>
        /// Handler of towers.
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
        /// Finds the closest enemy and reduces its healtpoints.  
        /// </summary>
        private void HandleDamageTower(int row, int col)
        {
            for(int i = row-1; i >= 0; i--)
            {
                if (Enemies[i,col].Count != 0 && Towers[row,col].Range > row-1-i)
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
        /// Periodically generates extra money for the player.
        /// </summary>
        private void HandleGoldTower(int row, int col)
        {
            if(Towers[row, col] is SVsIGoldTower goldTower)
            {
                Money += goldTower.Gold();
            }
        }

        /// <summary>
        /// Increases other towers' healthpoints near itself.
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
                        if(i < 0 || j < 0 || i >= Rows || j >= Cols || (i == row && j == col)) continue;

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
        /// Places given type of enemy on the gameboard.
        /// </summary>
        public void WhichEnemy(EnemyType type, int i)
        {
            if(type is EnemyType.Normal) PlaceEnemy(0,i,EnemyType.Normal);
            if(type is EnemyType.Buff) PlaceEnemy(0,i,EnemyType.Buff);
            if(type is EnemyType.Speedy) PlaceEnemy(0,i,EnemyType.Speedy);
        }

        /// <summary>
        /// Handles enemies' movement and shots.
        /// </summary>
        private void HandleEnemies()
        {
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
                                        Towers[i+1,j] = null; 
                                    break;   
                                }
                            
                                Enemies[i,j][k].CoolDown = Enemies[i,j][k].TickTime;
                            }
                            else
                            {
                                Enemies[i,j][k].CoolDown -= 1;
                            }

                            if (SecondsElapsed % Enemies[i,j][k].Movement == 0) 
                            {
                                if( i+1 < Rows && null == Towers[i+1,j])
                                {
                                    if (null == Enemies[i+1,j])
                                    {
                                        Enemies[i+1,j] = new List<SVsIEnemy>();
                                    }
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
            for(int k = 0; k < 1 + ReinforceTimes; k++)
            {
                if(WS.AreEnemiesLeft() && SecondsElapsed % 3 == 0) 
                {
                    List<EnemyType> tmp = new List<EnemyType>();
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

        }
        /// <summary>
        /// Stars a new game.
        /// </summary>
        /// <param name="rows"> Number of rows in the new game. </param>
        /// <param name="cols"> Number of coloumns in the new game. </param>
        public void NewGame(int rows, int cols) 
        {
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

        /// <summary>
        /// Checks if the game's over.
        /// </summary>
        private void CheckGameOver()
        {
            if (Castle.Health <= 0)
            {
                // IsGameOver = true;
                onGameOver(false);
            }
            if (SecondsElapsed * Config.GetValue<double>("TickTime") >=  Config.GetValue<double>("RoundTime"))
            {
                onGameOver(true);
            }
        }

        /// <summary>
        /// Upgrades the chosen tower if the player has enough money for it.
        /// </summary>
        /// <param name="row"> The row of the tower that will be upgraded. </param>
        /// <param name="col"> The coloumn of the tower that will be upgraded. </param>
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
        /// Places a tower of the selected type if the player has enough money for it.
        /// </summary>
        /// <param name="row"> The row where the tower will be placed. </param>
        /// <param name="col"> The coloumn where the tower will be placed. </param>
        /// <param name="type"> The type of the tower that will be placed. </param>
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
        /// Sells the selected tower and increases the player's money.
        /// </summary>
        /// <param name="row"> The row of the tower that will be sold. </param>
        /// <param name="col"> The coloumn of the tower that will be sold. </param>
        public void SellTower(int row, int col)
        {
            var tower = Towers[row, col] ?? throw new SVsIModelException("No tower selected.");

            Money += tower.SellCost;
            DestroyTower(row,col);
        }

        /// <summary>
        /// Destroys the tower whose coordinates were given.
        /// </summary>
        /// <param name="row"> The row where the tower will be destroyed. </param>
        /// <param name="col"> The coloumn where the tower will be destroyed. </param>
        private void DestroyTower(int row, int col)
        {
            Towers[row, col] = null;
            onTowerDestroyed(row, col);
            TowerCounter--;
        }
    
        /// <summary>
        /// Upgrades the castle if the player has enough money for it. Returns 'true' if the upgrade was successful.
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
        ///  Generates catastrophes.
        /// </summary>
        public void Catastrophe()
        {
            Random rnd = new Random();
            int szam = rnd.Next(200);
            if (szam < 4)
            {
                for (int j = 0; j < 3; j++)
                {
                    int dmg = rnd.Next(20,50);
                    Coordinate tmp = generateCoordinates();
                    HandleAsteroidCatastrophe(tmp.X, tmp.Y, dmg);
                    onAsteroidCatastrophe(tmp.X, tmp.Y);
                }
            }
            else if (szam > 196)
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

        /// <summary>
        /// Handles Damage Catastrophe
        /// </summary>
        /// <param name="i">Affected row</param>
        /// <param name="j">Affected col</param>
        /// <param name="dmg">Damage to be dealt</param>
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
                    Enemies[i,j][k].Health -= dmg;
                    if(Enemies[i,j][k].Health <= 0)
                    {
                        Enemies[i,j].Remove(Enemies[i,j][k]);
                        onEnemyDead(i,j);
                    }
                }
            }
        }

        /// <summary>
        /// Handles Heal Catastrophe
        /// </summary>
        /// <param name="i">Affected row</param>
        /// <param name="j">Affected col</param>
        /// <param name="heal">Healing to be dealt</param>
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

        /// <summary>
        /// Generates a random coordinate on the board
        /// </summary>
        /// <returns>A random coordinate on the board</returns>
        public Coordinate generateCoordinates()
        {
            Random rnd = new Random();
            int y = rnd.Next(0,Cols); 
            int x = rnd.Next(0,Rows); 
            return new Coordinate(x,y);
        }

        /// <summary>
        /// Places given type of enemy on the board.
        /// </summary>
        /// <param name="row"> The row where the enemy will be placed. </param>
        /// <param name="col"> The coloumn where the enemy will be placed. </param>
        /// <param name="enemyType"> The type of the enemy that will be placed. </param>
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