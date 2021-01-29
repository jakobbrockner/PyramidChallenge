using System.Collections.Generic;

namespace Pyramid.Business.Classes
{
    public class PyramidItem
    {
        public int x { get; set; }
        public int y { get; set; }  
        public int Value { get; set; }
        public List<PyramidItem> Nodes { get; set; }
        
    }
}
