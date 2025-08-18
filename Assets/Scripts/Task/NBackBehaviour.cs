using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Task
{
    [RequireComponent(typeof(TaskManagerBehaviour))]
    public class NBackBehaviour : TextTaskBehaviour
    {
        #region Parameter
        [Header("Parameter")]
        public int N = 1; // Task Difficulty N 1-3
        public int V = 1; // V Options of difficulty N 0-2
        private int _vIdx;
        #endregion
        private Dictionary<int, string[]> _nBackTasks = new();


        private void Start() {
            _nBackTasks[1] = new string[]{ 
                "F K R R W Q L D D H M P S S T B V C C X G N A A Y Z J U U E",
                "T H J B B N P X S S V L O M K K Q F R G G W D Y Z Z E A I I",
                "L M D E E G S S O P P V Q R N W X X A Y C F F H Z B K L L D"
                };
            _nBackTasks[2] = new string[]{
                "X K X L Q R Q D M D F S P T V S B C B G N G Y A H Z J Z O K",
                "H T A P R W R S V S O M T Q K Q F L I D Y D Z E Z U H Q H N",
                "L M A D F D G K H P Q P T S T W V W X Y R H Z H S U H L A L"
            };

            if (AssertV())
            {
                _vIdx = V-1;
                Debug.Log("initial " + N + "-back " + V );
                Task = new NBackTask(_nBackTasks[N][_vIdx], N);
            };
        }

        public void RandomN() {
            N = UnityEngine.Random.Range(1, _nBackTasks.Count);
            Debug.Log("this is " + N + "-back " + V);
        }
        public void RandomV() {
            V = UnityEngine.Random.Range(0, _nBackTasks[N].Length);
            Debug.Log("this is " + N + "-back " + V);
        }

        public void NextN() {
            N = (N % _nBackTasks.Count) + 1; // 0 -> 2; 1 -> 1; 2 -> 0;
            Debug.Log("this is " + N + "-back " + V);
        }
        public void NextV() {
            _vIdx = (N + _nBackTasks[N].Length + 1) % _nBackTasks[N].Length; // 0 -> 2; 1 -> 1; 2 -> 0;
            V = _vIdx + 1; 
            Debug.Log("this is " + N + "-back " + V);
        }

        private bool AssertV(){
            if (V <= 0) throw new ArgumentException("V must be between 1 and 3.");
            if (V > 3) throw new ArgumentException("V must be between 1 and 3.");
            return true;
        }
    }
}
