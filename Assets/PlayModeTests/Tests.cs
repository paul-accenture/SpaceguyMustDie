﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class Tests
    {

        player myPlayer;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Test Level"); 
        }

        [UnityTest]
        public IEnumerator PlayerStartsOutNotAlive()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

            yield return new WaitForSeconds(0.1f);

            Assert.False(myPlayer.isAlive());
        }

        [UnityTest]
        public IEnumerator PlayerFallsFromGravity()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            yield return new WaitForSeconds(0.2f);

            Assert.Less(myPlayer.GetRigidbody2D().velocity.y, 0);
        }

        [UnityTest]
        public IEnumerator PlayerLandsOnGround()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            yield return new WaitForSeconds(1.1f);

            Assert.AreEqual(0, myPlayer.GetRigidbody2D().velocity.y);
        }

        [UnityTest]
        public IEnumerator PlayerRunsWhenStarted()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            myPlayer.go();
            yield return new WaitForSeconds(0.2f);

            Assert.Greater(myPlayer.GetRigidbody2D().velocity.x, 0);
            Assert.True(myPlayer.isAlive());
        }

        [UnityTest]
        public IEnumerator PlayerDiesWhenTouchingEnemy()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            myPlayer.go();

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            myPlayer.GetRigidbody2D().MovePosition(enemy.transform.position);

            yield return new WaitForSeconds(0.2f);

            Assert.False(myPlayer.isAlive());
        }

        [UnityTest]
        public IEnumerator PlayerJumpsOnSprings()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

            GameObject spring = GameObject.FindGameObjectWithTag("jumpBoard");
            myPlayer.GetRigidbody2D().MovePosition(spring.transform.position);

            yield return new WaitForSeconds(0.2f);

            Assert.Greater(myPlayer.GetRigidbody2D().velocity.y, 0);
        }

        [UnityTest]
        public IEnumerator PlayerRespawnsOnFlagIfNotEnoughKeys()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

            GameObject spawn = GameObject.FindGameObjectWithTag("Respawn");
            GameObject flag = GameObject.FindGameObjectWithTag("Flag");


            myPlayer.GetRigidbody2D().MovePosition(flag.transform.position);

            yield return new WaitForSeconds(0.01f);

            Assert.AreEqual(myPlayer.transform, spawn.transform);
            Assert.False(myPlayer.isAlive());
            
        }

        [UnityTest]
        public IEnumerator PlayerIgnoresKeysIfStateIsGreen()
        {

            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            gameState myState = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();
            GameObject key = GameObject.FindGameObjectWithTag("Key");

            myPlayer.GetRigidbody2D().MovePosition(key.transform.position);
            yield return new WaitForSeconds(0.01f);

            Assert.False(myState.keysGathered[0]);
        }

        [UnityTest]
        public IEnumerator PlayerGathersKeysIfStateIsRed()
        {

            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            gameState myState = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameState>();
            GameObject key = GameObject.FindGameObjectWithTag("Key");

            myState.updateState(gameState.state.RED);

            myPlayer.GetRigidbody2D().MovePosition(key.transform.position);
            yield return new WaitForSeconds(0.01f);

            Assert.True(myState.keysGathered[0]);
        }


    }
}