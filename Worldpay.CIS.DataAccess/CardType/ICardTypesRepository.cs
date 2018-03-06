using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.CardType
{
    public interface ICardTypesRepository
    {
        Task<ICollection<CardTypes>> GetTransInquiryCardTypes();
    }
}
