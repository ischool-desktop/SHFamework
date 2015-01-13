﻿using System;
using System.Collections.Generic;

using System.Text;

namespace Framework
{
    public class @Decimal
    {
        static public decimal Parse(string Value)
        {
            decimal i;

            return decimal.TryParse(Value, out i) ? i : 0;
        }

        static public decimal? ParseAllowNull(string Value)
        {
            decimal i;

            if (decimal.TryParse(Value, out i))
                return i;
            else
                return null;
        }

        static public string GetString(decimal Value)
        {
            return Value.ToString();
        }

        static public string GetString(decimal? Value)
        {
            return Value == null ? "" : Value.ToString();
        }
    }
}