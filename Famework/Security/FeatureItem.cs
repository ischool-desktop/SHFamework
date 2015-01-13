using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Security
{
    public abstract class FeatureItem
    {
        public FeatureItem()
        {
            Code = string.Empty;
            Editor = null;
        }

        public string Title { get; set; }

        public string Code { get; set; }

        public IAceEditor Editor { get; set; }
    }
}
