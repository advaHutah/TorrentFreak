using AttributeProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionProject
{
    public class ReflectionClass
    {
        [IdAttribute(1)]
        class Car
        {
            private string model;

            public Car()
            {
                this.model = "Fiat ";
            }

            public void drive()
            {
                MessageBox.Show("driving " + model);
            }
        }
        [IdAttribute(2)]
        class Duck
        {
            private string name;

            public Duck()
            {
                this.name = "Donald ";
            }

            public void quack()
            {
                MessageBox.Show("Ga Ga " + name);
            }
        }
    }
}
