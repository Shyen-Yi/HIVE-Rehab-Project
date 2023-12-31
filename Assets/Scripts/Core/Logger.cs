using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.hive.projectr
{
    public static class Logger
    {
        public static void Log(string msg)
        {
            Debug.Log($"{msg}\nFrame: {Time.frameCount}\nTime: {Time.time}");
        }

        public static void LogWarning(string msg)
        {
            Debug.LogWarning($"{msg}\nFrame: {Time.frameCount}\nTime: {Time.time}");
        }

        public static void LogError(string msg)
        {
            Debug.LogError($"{msg}\nFrame: {Time.frameCount}\nTime: {Time.time}");
        }
    }
}