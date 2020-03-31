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
        public void PlaceTower()
        {
            _model = new SVsIModel(8,8);
            _model.NewGame(8,8);
           
            
            Assert.False(_model.PlaceTower(1,1, TowerType.Damage));

            //_model.PlaceTower(2,2, TowerType.Gold);
            //_model.PlaceTower(3,3, TowerType.Heal);
             _model.Money = 400;

            bool placed = _model.PlaceTower(1,1, TowerType.Damage);
            Assert.True(placed);
            Assert.True(_model.Towers[1,1] is SVsIDamageTower);
        }
    }
}
