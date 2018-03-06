
using System;
using System.Collections.Generic;

using Wp.CIS.LynkSystems.Model;

namespace UnitTestDataAccess
{
    public class MockCustomerRepository
    {
        
        List<CustomerProfile> customers;

        public bool FailGet { get; set; }

        public MockCustomerRepository()
        {
            customers = new List<CustomerProfile>() {
            new CustomerProfile(){customerID = 191807,
            activationDt = DateTime.Now,
             statusIndicator =0 ,
            legalType =0,
            businessEstablishedDate ="",
            lynkAdvantageDate =DateTime.Now,
            customerNbr ="",
            classCode =0,
            sensitivityLevel =0,
            stmtTollFreeNumber ="",
            deactivationDt = DateTime.Now,
            legalDesc ="",
            senseLvlDesc ="",
            statDesc ="",
            lynkAdvantage =0,
            pinPadPlus =0,
            giftLynk =0,
            rewardsLynk =0,
            demoID =0,
            custName ="",
            custContact ="",
            prinID =0,
            prinName ="",
            prinAddress ="",
            prinCity ="",
            prinState ="",
            prinZipcode ="",
            prinSSN ="",
            custFederalTaxID ="",
            irsVerificationStatus =0,
             propHasEmployees =0
            }

            };

        }


    }
}
