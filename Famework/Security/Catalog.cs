using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Security
{
    public class Catalog
    {
        public Catalog()
        {
            SubCatalogs = new CatalogCollection();
            _features = new List<FeatureItem>();
        }

        public CatalogCollection SubCatalogs { get; set; }

        private List<FeatureItem> _features;
        public IList<FeatureItem> Features { get { return _features.AsReadOnly(); } }

        public void Add(FeatureItem feature)
        {
            _features.Add(feature);
        }

        public Catalog this[string name]
        {
            get
            {
                return SubCatalogs[name];
            }
        }
    }
}
