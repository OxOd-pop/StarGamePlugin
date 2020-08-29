using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarGame.Common;
using System;

namespace StarGame.Tool
{
    /// <summary>
    /// 计时器
    /// </summary>
    public struct Timer
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public float currentTime;
        /// <summary>
        /// 设置时间
        /// </summary>
        public float setTime;

        public static Timer zero = new Timer(0f);

        /// <summary>
        /// 初始化计时器
        /// </summary>
        /// <param name="_setTime">设置时间</param>
        public Timer(float _setTime)
        {
            currentTime = 0;
            this.setTime = _setTime;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="t"></param>
        public void Ass(Timer t)
        {
            this.currentTime = t.currentTime;
            this.setTime = t.setTime;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="c">当前时间</param>
        /// <param name="s">设置时间</param>
        public void Ass(float c,float s)
        {
            this.currentTime = c;
            this.setTime = s;
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="t">目标计时器</param>
        /// <returns></returns>
        public bool Equal(Timer t)
        {
            if (this.currentTime == t.currentTime && this.setTime == t.setTime) return true;
            else return false;
        }
    }

    public enum TimeUpdateMode
    {
        DeltaTime,
        unscaledDeltaTime
    }
    public class TimeTool
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

        /// <summary>
        /// 触发计时器
        /// </summary>
        /// <param name="timer">计时器</param>
        /// <param name="once">是否只计算一次</param>
        /// <param name="mode">刷新模式</param>
        /// <returns></returns>
        public static bool Timertrigger(ref Timer timer,bool once = false,TimeUpdateMode mode = TimeUpdateMode.DeltaTime)
        {
            if (timer.Equal(Timer.zero)) return false;
            if (timer.currentTime >= timer.setTime)
            {
                if(!once) timer.currentTime = 0;
                return true;
            }
            float time = 0;
            switch (mode)
            {
                case TimeUpdateMode.DeltaTime:
                    time = Time.deltaTime;
                    break;
                case TimeUpdateMode.unscaledDeltaTime:
                    time = Time.unscaledDeltaTime;
                    break;
                default:
                    break;
            }
            timer.currentTime += time;
            return false;
        }
    }
}

