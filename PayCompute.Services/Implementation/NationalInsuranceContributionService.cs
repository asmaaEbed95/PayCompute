using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services.Implementation
{
    public class NationalInsuranceContributionService : INationalInsuranceContributionService
    {
        private decimal NIRate;
        private decimal NIC;

        public decimal NIContribution(decimal totalAmount)
        {
            if (totalAmount < 719)
            {
                //lower earnings limit rate & below primary threshold
                NIRate = .0m;
                NIC = .0m;
            }
            else if (totalAmount >= 719 && totalAmount < 4167)
            {
                //between primary threshold and upper earnings limit (UEL)
                NIRate = .12m;
                NIC = ((totalAmount - 719) * NIRate);
            }
            else if (totalAmount > 4167)
            {
                //above upper earnings limit (UEL)
                NIRate = .02m;
                NIC = ((4167 - 719) * .12m) + ((totalAmount - 4167) * NIRate);
            }

            return NIC;
        }
    }
}
