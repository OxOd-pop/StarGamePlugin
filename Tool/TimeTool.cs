using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarGame.Common;
using System;

namespace StarGame.Tool
{
    [Serializable]
    public class Timer
    {
        public bool isFinish = false;

        public float interval;
        private Action callBack;

        public float time = 0;
        public Timer(float t, Action cb)
        {
            Reset(t, cb);
        }

        ~Timer()
        {
            callBack = null;
        }

        public void Reset(float t, Action cb)
        {
            this.interval = t;
            callBack = cb;
            this.time = 0;
            isFinish = false;
        }

        public void Stop()
        {
            this.time = 0;
            this.isFinish = true;
            this.callBack = null;
        }

        public void Update()
        {
            if (isFinish) return;
            if (this.time >= interval)
            {
                callBack?.Invoke();
                callBack = null;
                isFinish = true;
                this.time = 0;
            }
            this.time += Time.deltaTime;
        }
    }

    public class TimeTool : MonoSingleton<TimeTool>
    {
        #region 时间戳
        public static double timestamp
        {
            get { return GetTimestamp(DateTime.Now); }
        }

        public static DateTime GetTime(double timeStamp)
        {
            DateTime dateTimeStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1),TimeZoneInfo.Local);
            long lTime = (long)timeStamp * 10000000;
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        public static double GetTimestamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (time - startTime).TotalSeconds;
        }
        #endregion
        public  List<Timer> timers = new List<Timer>();

        private void Update()
        {
            this.TimerHandle();
        }

        private void TimerHandle()
        {
            if (timers.Count <= 0) return;


            for(int i = timers.Count-1; i >=0; i--)
            {
                timers[i].Update();
                if (timers[i].isFinish)
                {

                   timers.RemoveAt(i); 
                }
            }

            
        }

        public  Timer TimerTrigger(float interval,Action callback)
        {
            
            Timer tim = new Timer(interval, callback);
            timers.Add(tim);
            return tim;
        }
    }
}

