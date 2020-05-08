using System.Runtime.InteropServices;
using System;
using Xunit;
using SpaceVsInvaders;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Tests
{
    public class WStest
    {
        private SVsIModel _model;
        public WStest()
        {
            Config.Initiate("normal.json");
        }

        [Fact]
        /// <summary>
        ///  Damage katasztrófa sebzésének tesztelése
        /// </summary>
        public void DamageCatastrophe_Damage()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 999999999;
            _model.IsSpawningEnemies = false;
            _model.IsCatastrophe = false;

            // Sebzi a tornyokat és az ellenségeket is, a típusuktól függetlenül, és annyit sebez, amennyit paraméterként megkap
            _model.PlaceEnemy(0,0, EnemyType.Buff);
            _model.PlaceEnemy(0,1, EnemyType.Normal);
            _model.PlaceEnemy(0,2, EnemyType.Speedy);

            _model.PlaceTower(1,0, TowerType.Heal);
            _model.PlaceTower(1,1, TowerType.Damage);
            _model.PlaceTower(1,2, TowerType.Gold);

            _model.HandleAsteroidCatastrophe(0,0,1);
            _model.HandleAsteroidCatastrophe(0,1,1);
            _model.HandleAsteroidCatastrophe(0,2,1);
            
            _model.HandleAsteroidCatastrophe(1,0,1);
            _model.HandleAsteroidCatastrophe(1,1,1);
            _model.HandleAsteroidCatastrophe(1,2,1);

            Assert.True(_model.Enemies[0,0][0].Health == Config.GetValue<TowerConfig>("BuffEnemy").Health -1);
            Assert.True(_model.Enemies[0,1][0].Health == Config.GetValue<TowerConfig>("NormalEnemy").Health -1);
            Assert.True(_model.Enemies[0,2][0].Health == Config.GetValue<TowerConfig>("SpeedyEnemy").Health -1);

            Assert.True(_model.Towers[1,0].Health == Config.GetValue<TowerConfig>("HealTower").Health -1);
            Assert.True(_model.Towers[1,1].Health == Config.GetValue<TowerConfig>("DamageTower").Health -1);
            Assert.True(_model.Towers[1,2].Health == Config.GetValue<TowerConfig>("GoldTower").Health -1);
        }

         [Fact]
        /// <summary>
        ///  Damage katasztrófa hatórköre
        /// </summary>
        public void DamageCatastrophe_Range()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 999999999;
            _model.IsSpawningEnemies = false;
            _model.IsCatastrophe = false;

            // Csak ott Sebzi a tornyokat és az ellenségeket ahol meg lett hívva
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                  _model.PlaceTower(i,j, TowerType.Damage);
                }
            }
    
            _model.HandleAsteroidCatastrophe(0,0,1);
            Assert.True(_model.Towers[0,0].Health == Config.GetValue<TowerConfig>("DamageTower").Health -1);
            for (int i = 1; i < 8; i++)
            {
                Assert.True(_model.Towers[i,0].Health == Config.GetValue<TowerConfig>("DamageTower").Health);
                Assert.True(_model.Towers[0,i].Health == Config.GetValue<TowerConfig>("DamageTower").Health);
            }
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                  Assert.True(_model.Towers[i,j].Health == Config.GetValue<TowerConfig>("DamageTower").Health);
                }
            }
        }

        [Fact]
        /// <summary>
        ///  Heal katasztrófa gyógyításának tesztelése
        /// </summary>
        public void HealeCatastrophe_Heal()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 999999999;
            _model.IsSpawningEnemies = false;
            _model.IsCatastrophe = false;

            // gyógyítja a tornyokat és az ellenségeket is, a típusuktól függetlenül, és annyit gyógyít, amennyit paraméterként megkap
            _model.PlaceEnemy(0,0, EnemyType.Buff);
            _model.PlaceEnemy(0,1, EnemyType.Normal);
            _model.PlaceEnemy(0,2, EnemyType.Speedy);

            _model.PlaceTower(1,0, TowerType.Heal);
            _model.PlaceTower(1,1, TowerType.Damage);
            _model.PlaceTower(1,2, TowerType.Gold);

            //előtte lesebezzük őket 10-el
            _model.HandleAsteroidCatastrophe(0,0,10);
            _model.HandleAsteroidCatastrophe(0,1,10);
            _model.HandleAsteroidCatastrophe(0,2,10);
            
            _model.HandleAsteroidCatastrophe(1,0,10);
            _model.HandleAsteroidCatastrophe(1,1,10);
            _model.HandleAsteroidCatastrophe(1,2,10);

            Assert.True(_model.Enemies[0,0][0].Health == Config.GetValue<TowerConfig>("BuffEnemy").Health -10);
            Assert.True(_model.Enemies[0,1][0].Health == Config.GetValue<TowerConfig>("NormalEnemy").Health -10);
            Assert.True(_model.Enemies[0,2][0].Health == Config.GetValue<TowerConfig>("SpeedyEnemy").Health -10);

            Assert.True(_model.Towers[1,0].Health == Config.GetValue<TowerConfig>("HealTower").Health -10);
            Assert.True(_model.Towers[1,1].Health == Config.GetValue<TowerConfig>("DamageTower").Health -10);
            Assert.True(_model.Towers[1,2].Health == Config.GetValue<TowerConfig>("GoldTower").Health -10);
            // majd meggyógyítjuk őket 10-el
            _model.HandleHealingCatastrophe(0,0,10);
            _model.HandleHealingCatastrophe(0,1,10);
            _model.HandleHealingCatastrophe(0,2,10);
            
            _model.HandleHealingCatastrophe(1,0,10);
            _model.HandleHealingCatastrophe(1,1,10);
            _model.HandleHealingCatastrophe(1,2,10);

            Assert.True(_model.Enemies[0,0][0].Health == Config.GetValue<TowerConfig>("BuffEnemy").Health );
            Assert.True(_model.Enemies[0,1][0].Health == Config.GetValue<TowerConfig>("NormalEnemy").Health );
            Assert.True(_model.Enemies[0,2][0].Health == Config.GetValue<TowerConfig>("SpeedyEnemy").Health );

            Assert.True(_model.Towers[1,0].Health == Config.GetValue<TowerConfig>("HealTower").Health );
            Assert.True(_model.Towers[1,1].Health == Config.GetValue<TowerConfig>("DamageTower").Health );
            Assert.True(_model.Towers[1,2].Health == Config.GetValue<TowerConfig>("GoldTower").Health );
        }

         [Fact]
        /// <summary>
        ///  Damage katasztrófa hatórköre
        /// </summary>
        public void HealeCatastrophe_Range()
        {
            _model = new SVsIModel();
            _model.NewGame(8,8);
            _model.Money = 999999999;
            _model.IsSpawningEnemies = false;
            _model.IsCatastrophe = false;

            // Csak ott Sebzi a tornyokat és az ellenségeket ahol meg lett hívva
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                  _model.PlaceTower(i,j, TowerType.Damage);
                }
            }
            // először lesebzem mindet
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                  _model.HandleAsteroidCatastrophe(i,j,10);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                  Assert.True(_model.Towers[i,j].Health == Config.GetValue<TowerConfig>("DamageTower").Health-10);
                }
            }

            //aztán felhealelem csak azt
            _model.HandleHealingCatastrophe(0,0,10);
            Assert.True(_model.Towers[0,0].Health == Config.GetValue<TowerConfig>("DamageTower").Health);

            for (int i = 1; i < 8; i++)
            {
                Assert.True(_model.Towers[i,0].Health == Config.GetValue<TowerConfig>("DamageTower").Health-10);
                Assert.True(_model.Towers[0,i].Health == Config.GetValue<TowerConfig>("DamageTower").Health-10);
            }
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                  Assert.True(_model.Towers[i,j].Health == Config.GetValue<TowerConfig>("DamageTower").Health-10);
                }
            }
        }
    }
}