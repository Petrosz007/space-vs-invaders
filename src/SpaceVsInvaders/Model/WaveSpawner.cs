using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    public class WaveSpawner
    {
        private Queue EnemiesToSpawn;
        public List<SVsIEnemy> GetSpawnedEnemies(int n)
        {
            List<SVsIEnemy> tmp = new List<SVsIEnemy>();
            while(EnemiesToSpawn.Count > 0)
            {
                tmp.Add((SVsIEnemy)EnemiesToSpawn.Dequeue());
            }
            return tmp;
        }

        public void SpawnEnemies(int time, int n) // határértékeket majd config fájlból
        {
            int value = time * n * 100;
            Random rnd = new Random();
            int number = 0;

            while(value >= 100){

                number = rnd.Next(1,100);
                
                if(value >= 8000)
                {
                    if(number <= 10) EnemiesToSpawn.Enqueue(new SVsINormalEnemy());
                    if(number > 10 && number <= 55) EnemiesToSpawn.Enqueue(new SVsIBuffEnemy());
                    if(number > 55) EnemiesToSpawn.Enqueue(new SVsISpeedyEnemy());
                    value -= 800;
                }
                else if(value >= 5000)
                {
                    if(number <= 30) EnemiesToSpawn.Enqueue(new SVsINormalEnemy());
                    if(number > 30 && number <= 65) EnemiesToSpawn.Enqueue(new SVsIBuffEnemy());
                    if(number > 65) EnemiesToSpawn.Enqueue(new SVsISpeedyEnemy());
                    value -= 600;
                }
                else if(value >= 2500)
                {
                    if(number <= 50) EnemiesToSpawn.Enqueue(new SVsINormalEnemy());
                    if(number > 50 && number <= 75) EnemiesToSpawn.Enqueue(new SVsIBuffEnemy());
                    if(number > 75) EnemiesToSpawn.Enqueue(new SVsISpeedyEnemy());
                    value -= 400;
                }
                else if(value >= 1000)
                {
                    if(number <= 75) EnemiesToSpawn.Enqueue(new SVsINormalEnemy());
                    if(number > 75 && number <= 86) EnemiesToSpawn.Enqueue(new SVsIBuffEnemy());
                    if(number > 86) EnemiesToSpawn.Enqueue(new SVsISpeedyEnemy());
                    value -= 250;
                }
                else if(value >= 100)
                {
                    if(number <= 90) EnemiesToSpawn.Enqueue(new SVsINormalEnemy());
                    if(number > 90) EnemiesToSpawn.Enqueue(new SVsIBuffEnemy());
                    value -= 100;
                }
            }
        }
    }

}