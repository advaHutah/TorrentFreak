using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttributeProject
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class IdAttribute : Attribute
    {
        private int id;

        public IdAttribute(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }

}
