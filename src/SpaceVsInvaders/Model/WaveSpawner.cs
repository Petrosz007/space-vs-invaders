using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    /// <summary>
    /// Controls the distribution of enemies to the field.
    /// </summary>
    public class WaveSpawner
    {
        private Queue EnemiesToSpawn;

        /// <summary>
        /// Constructor of the class WaveSpawner
        /// </summary>
        public WaveSpawner()
        {
            EnemiesToSpawn = new Queue();
        }

        /// <summary>
        /// Provides as many enemies, as the count of coloumns of the game
        /// </summary>
        /// <param name="n">Number of coloumns</param>
        /// <returns>List of EnemyTypes</returns>
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
        /// <summary>
        /// Determines whether there are enemies left in the spawner's queue
        /// </summary>
        /// <returns>Bool</returns>
        public bool AreEnemiesLeft()
        {
            return EnemiesToSpawn.Count != 0;
        }
        /// <summary>
        /// Creates the EnemyTypes to be spawned
        /// </summary>
        /// <param name="time">Time elapsed ingame</param>
        /// <param name="col">Number of cols</param>
        /// <param name="towercount">Number of towers</param>
        /// <param name="towerupdates">Count of updates of the towers</param>
        /// <param name="threeminutes">How many times has 3 minutes passed</param>
        public void SpawnEnemies(int time, int col , int towercount, int towerupdates, int threeminutes) // határértékeket majd config fájlból
        {
            int value = time * 5 + col * 50 + towercount*10 + towerupdates*5 + 550*threeminutes;
            Random rnd = new Random();
            int number = 0;

            while(value >= 100){

                number = rnd.Next(1,100);
                
                if(value >= 8000)
                {
                    if(number <= 10) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 10 && number <= 55) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 55) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 600;
                }
                else if(value >= 5000)
                {
                    if(number <= 30) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 30 && number <= 65) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 65) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 500;
                }
                else if(value >= 2500)
                {
                    if(number <= 50) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 50 && number <= 75) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 75) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 400;
                }
                else if(value >= 1000)
                {
                    if(number <= 75) EnemiesToSpawn.Enqueue(EnemyType.Normal);
                    if(number > 75 && number <= 86) EnemiesToSpawn.Enqueue(EnemyType.Buff);
                    if(number > 86) EnemiesToSpawn.Enqueue(EnemyType.Speedy);
                    value -= 200;
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