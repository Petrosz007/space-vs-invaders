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

        [Fact]
        /// <summary>
        ///  Különböző tornyok elhelyezése a toronytáblán.
        /// </summary>
        public void PlacingTowers()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
           
            //  Nem sikerült a torony beszúrása, mert a játékosnak nincs elég pénze.
            Assert.False(_model.PlaceTower(1,1, TowerType.Damage));
            Assert.False(_model.PlaceTower(1,1, TowerType.Gold));
            Assert.False(_model.PlaceTower(1,1, TowerType.Heal));

            _model.Money = 10000;

            // Támadó torony beszúrása sikeres.
            Assert.True(_model.PlaceTower(1,1, TowerType.Damage));
            Assert.True(_model.Towers[1,1] is SVsIDamageTower);

            // Termelő torony beszúrása sikeres.
            _model.PlaceTower(2,2, TowerType.Gold);
             Assert.True(_model.Towers[2,2] is SVsIGoldTower);
            

            // Gyógyító torony beszúrása sikeres.
            _model.PlaceTower(3,3, TowerType.Heal);
             Assert.True(_model.Towers[3,3] is SVsIHealTower);

            // Tornyok beszúrása "kritikus" indexekre.
            _model.PlaceTower(0,0, TowerType.Heal);
            Assert.True(_model.Towers[0,0] is SVsIHealTower);

            _model.PlaceTower(0,7, TowerType.Gold);
            Assert.True(_model.Towers[0,7] is SVsIGoldTower);

            _model.PlaceTower(7,0, TowerType.Damage);
            Assert.True(_model.Towers[7,0] is SVsIDamageTower);

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
            
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 150;
            _model.PlaceTower(1,1, TowerType.Gold);

            // Lerakásakor a torony egyből termel.
            _model.HandleTick();
            Assert.True(_model.Money == (10+1));


            // Egy időegység elteltével nem termel.
            _model.HandleTick();
            Assert.True(_model.Money == (1+1+10));

            // Két időegység elteltével nem termel.
            _model.HandleTick();
            Assert.True(_model.Money == (1+1+1+10));

            //  Három időegység elteltével termel.
            _model.HandleTick();
            Assert.True(_model.Money == 24);

            // Ha több torony van, mindegyik termel.
            _model.Money = 174;
            _model.PlaceTower(2,2, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Money == 35);

            _model.Money = 185;
            _model.PlaceTower(3,3, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Money == 46);

        }


        [Fact]
        /// <summary>
        /// Gyógyító torony tesztelése.
        /// </summary>
        public void HealTower()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 450;


            // Gyógyító torony meggyógyítja az összes többi toronyfajtát és lerakásakor működésbe lép; sajátmagát nem gyógyítja.
            _model.PlaceTower(4,3, TowerType.Heal);
            _model.PlaceTower(3,3, TowerType.Damage);
            _model.PlaceTower(5,3, TowerType.Gold);
            _model.HandleTick();
            Assert.True(_model.Towers[4,3].Health == 50);
            Assert.True(_model.Towers[5,3].Health == 80);
            Assert.True(_model.Towers[3,3].Health == 105);

            // Négy időegységenként gyógyít.
            _model.HandleTick();
            _model.HandleTick();
            _model.HandleTick();
            _model.HandleTick();
            Assert.True(_model.Towers[5,3].Health == 85);
            Assert.True(_model.Towers[3,3].Health == 110);


            // Mindenkit meggyógyít önmaga 3x3-as környezetében.
            _model.Money = 1350;
            _model.PlaceTower(6,6, TowerType.Heal);
            _model.PlaceTower(5,5, TowerType.Damage);
            _model.PlaceTower(5,6, TowerType.Damage);
            _model.PlaceTower(5,7, TowerType.Gold);
            _model.PlaceTower(6,5, TowerType.Damage);
            _model.PlaceTower(6,7, TowerType.Damage);
            _model.PlaceTower(7,5, TowerType.Gold);
            _model.PlaceTower(7,6, TowerType.Damage);
            _model.PlaceTower(7,7, TowerType.Damage);
            _model.HandleTick();
            Assert.True(_model.Towers[5,5].Health == 105);
            Assert.True(_model.Towers[5,6].Health == 105);
            Assert.True(_model.Towers[5,7].Health == 80);
            Assert.True(_model.Towers[6,5].Health == 105);
            Assert.True(_model.Towers[6,7].Health == 105);
            Assert.True(_model.Towers[7,5].Health == 80);
            Assert.True(_model.Towers[7,6].Health == 105);
            Assert.True(_model.Towers[7,7].Health == 105);
            

            // Egy tornyot több különböző torony is tud gyógyítani egyszerre.
            _model.Money = 600;
            _model.PlaceTower(0,5, TowerType.Heal);
            _model.PlaceTower(0,6, TowerType.Gold);
            _model.PlaceTower(0,7, TowerType.Heal);
            _model.PlaceTower(1,6, TowerType.Heal);
            _model.HandleTick();
            Assert.True(_model.Towers[0,6].Health == 90);
        }
    }
}

