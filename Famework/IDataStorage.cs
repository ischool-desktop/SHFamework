using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Framework
{
    public interface IDataStorage
    {
        void Load(XmlElement value);
    }
}