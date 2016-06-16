using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MasterDetail {
    public class EmptyRule : ValidationRule {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) { 
        

            if (value == null || value.ToString() == String.Empty)
                return new ValidationResult(false, "Veuillez entrer un nom d'utilisateur");
            else
                return new ValidationResult(true, null);
        }
    }
}
