using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Setup
{
    public class EntityQueryTypeClass
    {
        EntityQueryType queryType;

        public EntityQueryTypeClass(EntityQueryType queryType)
        {
            this.queryType = queryType;
        }

        public EntityQueryType EntityQueryType
        {
            get { return queryType; }
        }

        public string Name
        {
            get
            {
                string str = Enum.GetName(typeof(EntityQueryType), queryType);
                return str;
            }
        }

        public static EntityQueryTypeClass[] EntityQueryTypeArray
        {
            get
            {
                EntityQueryType[] ar = (EntityQueryType[])Enum.GetValues(typeof(EntityQueryType));
                EntityQueryTypeClass[] arc = new EntityQueryTypeClass[ar.Length];

                for (int i=0; i < ar.Length; i++)
                {
                    arc[i] = new EntityQueryTypeClass(ar[i]);
                }
                return arc;
            }
        }
    }
}
