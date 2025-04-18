﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Common
{
    public class CommonHelper
    {
        public static string GetTimeSpan(TimeSpan ts)
        {
            if (ts.TotalDays > 365)
            {
                return Math.Floor(ts.TotalDays / 365) + "年前";
            }
            else if (ts.TotalDays > 30)
            {
                return Math.Floor(ts.TotalDays / 30) + "月前";
            }
            else if (ts.TotalHours > 24)
            {
                return Math.Floor(ts.TotalDays) + "天前";
            }
            else if (ts.TotalHours > 1)
            {
                return Math.Floor(ts.TotalHours) + "小时前";
            }
            else if (ts.TotalMinutes > 1)
            {
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }
    }
}
