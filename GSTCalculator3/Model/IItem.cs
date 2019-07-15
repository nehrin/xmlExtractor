using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSTCalculator.Model
{
    public interface IItem
    {
        ItemType ItemType
        {
            get;
        }

        int Id { get; set; }
    }
}

