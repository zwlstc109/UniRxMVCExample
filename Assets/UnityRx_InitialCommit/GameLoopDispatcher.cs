using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRx_InitialCommit
{
    public class GameLoopDispatcher : MonoBehaviour
    {
        static object gate = new object();
        Queue<Action> actionQueue = new Queue<Action>();

        static GameLoopDispatcher instance;
        static bool initialized;

        private GameLoopDispatcher()
        {

        }

        public static GameLoopDispatcher Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }

        static void Initialize()//单例范本 可学
        {
            if (!initialized)
            {

                if (!Application.isPlaying) return;
                initialized = true;
                instance = new GameObject("GameLoopDispatcher").AddComponent<GameLoopDispatcher>();
            }

        }

        void Awake()
        {
            instance = this;
            initialized = true;
        }

        public void Update()
        {
            lock (actionQueue)
            {
                while (actionQueue.Count != 0)
                {
                    var action = actionQueue.Dequeue();
                    action();
                }
            }
        }

        public static void Post(Action item)
        {
            lock (gate)
            {
                Instance.actionQueue.Enqueue(item);
            }
        }

        new public static Coroutine StartCoroutineRx(IEnumerator routine)
        {
            lock (gate)
            {
                return Instance.StartCoroutine(routine);
            }
        }
    }
}