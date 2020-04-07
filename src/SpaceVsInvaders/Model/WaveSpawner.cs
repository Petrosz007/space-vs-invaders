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

        public WaveSpawner()
        {
            EnemiesToSpawn = new Queue();
        }
        public List<EnemyType> GetSpawnedEnemies(int n)
        {
            List<EnemyType> tmp = new List<EnemyType>();
            while(EnemiesToSpawn.Count > 0 && n > 0)
            {
                tmp.Add((EnemyType)EnemiesToSpawn.Dequeue());
                n--;
            }
            return tmp;
        }

        public bool AreEnemiesLeft()
        {
            return EnemiesToSpawn.Count != 0;
        }

        public void SpawnEnemies(int time, int n) // határértékeket majd config fájlból
        {
            int value = time * 6;
            Random rnd = new Random();
            int number = 0;

            while(value >= 100){

                number = rnd.Next(1,100);
                
                if(value >= 8000)
                {
                    if(number <= 10) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 10 && number <= 55) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 55) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 800;
                }
                else if(value >= 5000)
                {
                    if(number <= 30) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 30 && number <= 65) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 65) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 700;
                }
                else if(value >= 2500)
                {
                    if(number <= 50) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 50 && number <= 75) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 75) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 500;
                }
                else if(value >= 1000)
                {
                    if(number <= 75) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 75 && number <= 86) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 86) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 250;
                }
                else if(value >= 100)
                {
                    if(number <= 90) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 90) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    value -= 100;
                }
            }
        }
    }

}