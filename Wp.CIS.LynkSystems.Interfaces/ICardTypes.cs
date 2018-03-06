using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ICardTypes
    {
        Task<ICollection<CardTypes>> GetCISCardTypes();
    }
}
