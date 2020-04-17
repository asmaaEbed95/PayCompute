using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services.Implementation
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;

        //we will use the numbers from www.gov.uk for tax 2019 to 2020
        public decimal TaxAmount(decimal totalAmount)
        {
            if (totalAmount <= 1042)
            {
                //tax free rate
                taxRate = .0m;
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > 1042 && totalAmount <= 3125)
            {
                //basic tax rate
                taxRate = 0.20m;

                //income tax
                tax = (1042 * .0m) + ((totalAmount - 1042) * taxRate);
            }
            else if (totalAmount > 3125 && totalAmount <= 12500)
            {
                //higher tax rate will apply
                taxRate = 0.40m;

                //income tax
                tax = (1042 * .0m) + ((3125 - 1042) * 0.20m) + ((totalAmount - 3125) * taxRate);
            }
            else if(totalAmount > 12500)
            {
                //addional tax rate
                taxRate = .45m;

                //income tax
                tax = (1042 * .0m) + ((3125 - 1042) * 0.20m) + ((12500 - 3125) * .40m + (totalAmount - 12500) * taxRate);
            }

            return tax;
        }
    }
}
