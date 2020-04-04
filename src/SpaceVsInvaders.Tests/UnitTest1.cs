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
            Assert.False(_model.PlaceTower(1,1, TowerType.Damage));
            Assert.False(_model.PlaceTower(1,1, TowerType.Gold));
            Assert.False(_model.PlaceTower(1,1, TowerType.Heal));

            // Támadó torony beszúrása sikeres.
            _model.Money = damageTowerCost;
            Assert.True(_model.PlaceTower(1,1, TowerType.Damage));
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

            Assert.True(_model.Towers[4,3].Health == healTowerHealth);
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
    }
}

