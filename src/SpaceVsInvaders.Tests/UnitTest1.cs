using System.Runtime.InteropServices;
using System;
using Xunit;
using SpaceVsInvaders;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Tests
{
    public class UnitTest1
    {
        private SVsIModel _model;

        public UnitTest1()
        {
            Config.Initiate();
        }

        [Fact]
        /// <summary>
        ///  Különböző tornyok elhelyezése a toronytáblán.
        /// </summary>
        public void PlacingTowers()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);

            int damageTowerCost = Config.GetValue<TowerConfig>("DamageTower").Cost;
            int goldTowerCost = Config.GetValue<TowerConfig>("GoldTower").Cost;
            int healTowerCost = Config.GetValue<TowerConfig>("HealTower").Cost;
           
            //  Nem sikerült a torony beszúrása, mert a játékosnak nincs elég pénze.
            Assert.Throws<SVsIModelException>(() => _model.PlaceTower(1,1, TowerType.Damage));
            Assert.Throws<SVsIModelException>(() => _model.PlaceTower(1,1, TowerType.Gold));
            Assert.Throws<SVsIModelException>(() => _model.PlaceTower(1,1, TowerType.Heal));

            // Támadó torony beszúrása sikeres.
            _model.Money = damageTowerCost;
            _model.PlaceTower(1,1, TowerType.Damage);
            Assert.True(_model.Towers[1,1] is SVsIDamageTower);

            // Termelő torony beszúrása sikeres.
            _model.Money = goldTowerCost;
            _model.PlaceTower(2,2, TowerType.Gold);
             Assert.True(_model.Towers[2,2] is SVsIGoldTower);
            
            // Gyógyító torony beszúrása sikeres.
            _model.Money = healTowerCost;
            _model.PlaceTower(3,3, TowerType.Heal);
             Assert.True(_model.Towers[3,3] is SVsIHealTower);

            // Tornyok beszúrása "kritikus" indexekre.
            _model.Money = healTowerCost;
            _model.PlaceTower(0,0, TowerType.Heal);
            Assert.True(_model.Towers[0,0] is SVsIHealTower);

            _model.Money = goldTowerCost;
            _model.PlaceTower(0,7, TowerType.Gold);
            Assert.True(_model.Towers[0,7] is SVsIGoldTower);

             _model.Money = damageTowerCost;
            _model.PlaceTower(7,0, TowerType.Damage);
            Assert.True(_model.Towers[7,0] is SVsIDamageTower);

            _model.Money = healTowerCost;
            _model.PlaceTower(7,7, TowerType.Heal);
            Assert.True(_model.Towers[7,7] is SVsIHealTower);
        }


        [Fact]
        /// <summary>
        /// Támadó torony támadó funkciójának tesztelése: ez pillanatnyilag az, hogy a hozzá legközelebb eső ellenségek közül a legelsőt sebzi.
        /// </summary>
        public void DamageTower()
        {
            _model = new SVsIModel();
            _model.NewGame(20,20);
            _model.IsSpawningEnemies = false;

            int damageTowerCost = Config.GetValue<TowerConfig>("DamageTower").Cost;
            int buffEnemyHealth = Config.GetValue<TowerConfig>("BuffEnemy").Health;   
            int normalEnemyHealth = Config.GetValue<TowerConfig>("NormalEnemy").Health;   
            int speedyEnemyHealth = Config.GetValue<TowerConfig>("SpeedyEnemy").Health;                        

            _model.Money = damageTowerCost * 10;

            _model.PlaceEnemy(0,0, EnemyType.Normal);
            _model.PlaceTower(2,0, TowerType.Damage);


            // oszinten nem tudom, hogy ezt hogy lehetne szebben megcsinalni
            var damageTower = (SVsIDamageTower) _model.Towers[2, 0];
            
            // Lerakásakor a torony egyből lő.
            _model.HandleTick();
            Assert.True(_model.Enemies[0,0][0].Health == normalEnemyHealth - damageTower.Damage());

            // Minden típusú ellenséget sebez.
            _model.PlaceTower(2,1, TowerType.Damage);
            var damageTower1 = (SVsIDamageTower) _model.Towers[2, 1];
            _model.PlaceEnemy(0,1, EnemyType.Buff);
            _model.PlaceTower(2,2, TowerType.Damage);
            var damageTower2 = (SVsIDamageTower) _model.Towers[2, 2];
            _model.PlaceEnemy(0,2, EnemyType.Normal);
            _model.PlaceTower(2,3, TowerType.Damage);
            var damageTower3 = (SVsIDamageTower) _model.Towers[2, 3];
            _model.PlaceEnemy(0,3, EnemyType.Speedy);
            
            _model.HandleTick();
            Assert.True(_model.Enemies[0,1][0].Health == buffEnemyHealth - damageTower1.Damage());
            Assert.True(_model.Enemies[0,2][0].Health == normalEnemyHealth - damageTower2.Damage());
            // Azért (1,3), mert a SpeedyEnemy azalatt az 1 tick alatt előrelépett egy mezőt.
            Assert.True(_model.Enemies[1,3][0].Health == speedyEnemyHealth - damageTower3.Damage());
        }

        [Fact]
        public void DamageTower2()
        {
            _model = new SVsIModel();
            _model.NewGame(20,20);
            _model.IsSpawningEnemies = false;

            int damageTowerCost = Config.GetValue<TowerConfig>("DamageTower").Cost;
            int buffEnemyHealth = Config.GetValue<TowerConfig>("BuffEnemy").Health;   
            int normalEnemyHealth = Config.GetValue<TowerConfig>("NormalEnemy").Health;   
            int speedyEnemyHealth = Config.GetValue<TowerConfig>("SpeedyEnemy").Health;                        

            _model.Money = damageTowerCost * 10;
            // Ha több ellenség tartózkodik ugyanazon a mezőn, akkor csak azt sebzi, aki leghamarabb volt ott.
           _model.PlaceTower(2,8, TowerType.Damage);
            var damageTower1 = (SVsIDamageTower) _model.Towers[2, 8];
            _model.PlaceEnemy(0,8, EnemyType.Buff);
            _model.PlaceEnemy(0,8, EnemyType.Normal);
            _model.PlaceEnemy(0,8, EnemyType.Normal);
            _model.HandleTick();

            Assert.True(_model.Enemies[0,8][0].Health == buffEnemyHealth - damageTower1.Damage());
            Assert.True(_model.Enemies[0,8][1].Health == normalEnemyHealth);
            Assert.True(_model.Enemies[0,8][2].Health == normalEnemyHealth);

            // Hatótávolságon kívül nem sebez.
            int range = Config.GetValue<TowerConfig>("DamageTower").Range;
            _model.PlaceEnemy(0,0, EnemyType.Normal);
            _model.PlaceTower(range+1, 0, TowerType.Damage);

            // Hatótávolságon belül pedig sebez.
            _model.PlaceEnemy(0,4, EnemyType.Normal);
            _model.PlaceTower(range,4, TowerType.Damage);
            var damageTower2 = (SVsIDamageTower) _model.Towers[range, 4];
            
            _model.HandleTick();
            Assert.True(_model.Enemies[0,0][0].Health == normalEnemyHealth);
            Assert.True(_model.Enemies[0,4][0].Health == normalEnemyHealth - damageTower2.Damage());
        }

        [Fact]
        /// <summary>
        /// Termelő torony termelő funkciójának tesztelése.
        /// </summary>
        public void GoldTower()
        {
            
            int goldTowerCost = Config.GetValue<TowerConfig>("GoldTower").Cost;
            
            _model = new SVsIModel();

            _model.NewGame(8,8);
            _model.Money = goldTowerCost;
            _model.PlaceTower(1,1, TowerType.Gold);

            var goldTower = (SVsIGoldTower) _model.Towers[1, 1];

            // Lerakásakor a torony egyből termel.
            _model.HandleTick();
            Assert.True(_model.Money == _model.SecondsElapsed + goldTower.Gold());

            // Egy időegység elteltével nem termel.
            _model.HandleTick();
            Assert.True(_model.Money == _model.SecondsElapsed + goldTower.Gold());

            // Két időegység elteltével nem termel.
            _model.HandleTick();
            Assert.True(_model.Money == _model.SecondsElapsed + goldTower.Gold());

            //  Három időegység elteltével termel.
            _model.HandleTick();
            Assert.True(_model.Money == _model.SecondsElapsed + 2 * goldTower.Gold());

            // Ha több torony van, mindegyik termel.
            _model.Money += goldTower.Cost;
            _model.PlaceTower(2,2, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Money ==  _model.SecondsElapsed + 3 * goldTower.Gold());

            _model.Money += goldTowerCost;
            _model.PlaceTower(3,3, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Money == _model.SecondsElapsed + 4 * goldTower.Gold());
        }


        [Fact]
        /// <summary>
        /// Gyógyító torony tesztelése.
        /// </summary>
        public void HealTower()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);

            int damageTowerCost = Config.GetValue<TowerConfig>("DamageTower").Cost;
            int goldTowerCost = Config.GetValue<TowerConfig>("GoldTower").Cost;
            int healTowerCost = Config.GetValue<TowerConfig>("HealTower").Cost;

            int damageTowerHealth = Config.GetValue<TowerConfig>("DamageTower").Health;
            int goldTowerHealth = Config.GetValue<TowerConfig>("GoldTower").Health;
            int healTowerHealth = Config.GetValue<TowerConfig>("HealTower").Health;

            _model.Money = damageTowerCost + goldTowerCost + healTowerCost;

            // Gyógyító torony meggyógyítja az összes többi toronyfajtát és lerakásakor működésbe lép; sajátmagát nem gyógyítja.
            _model.PlaceTower(3,3, TowerType.Damage);
            _model.PlaceTower(5,3, TowerType.Gold);
            _model.PlaceTower(4,3, TowerType.Heal);
            _model.HandleTick();        

            var healTower = (SVsIHealTower) _model.Towers[4, 3];

            Assert.True(_model.Towers[4,3].Health == healTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[3,3].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[5,3].Health == goldTowerHealth + healTower.Heal());

            // Négy időegységenként gyógyít.
            _model.HandleTick();
            _model.HandleTick();
            _model.HandleTick();
            _model.HandleTick();
            Assert.True(_model.Towers[3,3].Health == damageTowerHealth + 2 * healTower.Heal());
            Assert.True(_model.Towers[5,3].Health == goldTowerHealth + 2 * healTower.Heal());


            // Mindenkit meggyógyít önmaga 3x3-as környezetében.
            _model.Money = 6 * damageTowerCost + 2 * goldTowerCost + healTowerCost;
            _model.PlaceTower(6,6, TowerType.Heal);
            _model.PlaceTower(5,5, TowerType.Damage);
            _model.PlaceTower(5,6, TowerType.Damage);
            _model.PlaceTower(5,7, TowerType.Gold);
            _model.PlaceTower(6,5, TowerType.Damage);
            _model.PlaceTower(6,7, TowerType.Damage);
            _model.PlaceTower(7,5, TowerType.Gold);
            _model.PlaceTower(7,6, TowerType.Damage);
            _model.PlaceTower(7,7, TowerType.Damage);

            healTower = (SVsIHealTower) _model.Towers[6, 6];
            
            _model.HandleTick();
            Assert.True(_model.Towers[5,5].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[5,6].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[5,7].Health == goldTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[6,5].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[6,7].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[7,5].Health == goldTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[7,6].Health == damageTowerHealth + healTower.Heal());
            Assert.True(_model.Towers[7,7].Health == damageTowerHealth + healTower.Heal());
            

            // Egy tornyot több különböző torony is tud gyógyítani egyszerre.
            _model.Money = 3 * healTowerCost + goldTowerCost;

            _model.PlaceTower(0,5, TowerType.Heal);
            var healTower1 = (SVsIHealTower) _model.Towers[0, 5];

            _model.PlaceTower(0,7, TowerType.Heal);
            var healTower2 = (SVsIHealTower) _model.Towers[0, 7];

            _model.PlaceTower(1,6, TowerType.Heal);
            var healTower3 = (SVsIHealTower) _model.Towers[1, 6];

            _model.PlaceTower(0,6, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Towers[0,6].Health == goldTowerHealth + healTower1.Heal() + healTower2.Heal() + healTower3.Heal());
        }

        [Fact]
        /// <summary>
        /// Ellenségek mozgásának tesztelése.
        /// </summary>
        public void EnemyMovement()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);

            _model.PlaceEnemy(0,0, EnemyType.Buff);
            _model.PlaceEnemy(0,1, EnemyType.Normal);
            _model.PlaceEnemy(0,2, EnemyType.Speedy);

            // a SpeedyEnemy mozgott egyet előre
            _model.HandleTick();
            Assert.True(_model.Enemies[0,0].Count == 1);
            Assert.True(_model.Enemies[0,1].Count == 1);
            Assert.True(_model.Enemies[0,2].Count == 0);
            Assert.True(_model.Enemies[1,2].Count == 1);

            _model.HandleTick();
            _model.HandleTick();

            Assert.True(_model.Enemies[0,0].Count == 0);
            Assert.True(_model.Enemies[1,0].Count == 1);

            Assert.True(_model.Enemies[0,1].Count == 0);
            Assert.True(_model.Enemies[1,1].Count == 1);

            Assert.True(_model.Enemies[0,2].Count == 0);
            Assert.True(_model.Enemies[3,2].Count == 1);

            // Kárt tesznek a vár healthjében, ha odaérnek.
            int castleHealth = Config.GetValue<TowerConfig>("Castle").Health;   

            _model.PlaceEnemy(7,4, EnemyType.Buff);
            _model.PlaceEnemy(7,5, EnemyType.Normal);
            _model.PlaceEnemy(7,6, EnemyType.Speedy);
            _model.HandleTick();
            Assert.True(_model.Castle.Health == castleHealth-1);
            _model.HandleTick();
            _model.HandleTick();
             Assert.True(_model.Castle.Health == castleHealth-3);
        }

    }
}

