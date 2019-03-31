using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker2
{
    public class Entity
    {
        public String Message { get; set; }
        public String FirstID { get; set; }
        public String SecondID { get; set; }

        public Entity(string m,string f,string s)
        {
            Message = m;
            FirstID = f;
            SecondID = s;
        }
    }
}
