using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _02.Contains
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceContains : IServiceContains
    {
        public int GetContainNumber(string value, string searchedInValue)
        {
            int containNumber = 0;
            int index = 0;
            while (index < searchedInValue.Length)
            {
                index = searchedInValue.IndexOf(value, index);
                if (index != -1)
                {
                    containNumber++;
                    index += value.Length;
                }
            }

            return containNumber;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        
    }
}
