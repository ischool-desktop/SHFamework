﻿using System;
using System.Collections.Generic;

using System.Text;

namespace Framework
{
    public enum ChangedSource
    {
        Local,Remote
    }

    /// <summary>
    /// 提供Insert、Update、Delete事件的資料
    /// </summary>
    public class DataChangedEventArgs : EventArgs
    {
        public List<string> PrimaryKeys { get; private set; }
        public ChangedSource Source { get; private set; }

        public DataChangedEventArgs(IEnumerable<string> primaryKeys,ChangedSource source)
        {
            PrimaryKeys = new List<string>(primaryKeys);
            Source = source;
        }
    }
}