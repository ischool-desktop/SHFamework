using System;
using System.Collections.Generic;

namespace Framework
{
    public class EntityCache<T> : Dictionary<string,T>
    {
        public CacheSet<T> GetByIDs(IEnumerable<string> IDs)
        {
            CacheSet<T> CacheSet = new CacheSet<T>();

            //針對傳入的ID檢查Cache當中是否有相對應的物件，若有的話放到物件列表當中，沒有的話將ID存起來
            foreach (string strID in IDs)
                if (this.ContainsKey(strID))
                    CacheSet.Records.Add(this[strID]);
                else
                    CacheSet.NoExistIDs.Add(strID);
            return CacheSet;
        }

        public int Remove(IEnumerable<string> IDs)
        {
            int Result = 0;

            foreach (string ID in IDs)
            {
                if (this.ContainsKey(ID))
                {
                    this.Remove(ID);
                    Result++;
                }
            }

            return Result;
        }

        public void NotifyRemove(object sender, DataChangedEventArgs e)
        {
            this.Remove(e.PrimaryKeys);
        }

    }

    public class CacheSet<T>
    {
        public List<string> NoExistIDs;
        public List<T> Records;

        public CacheSet()
        {
            NoExistIDs = new List<string>();
            Records = new List<T>();
        }
    }
}