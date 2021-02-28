using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KineticEnergyFinder
{
    class KineticEnergyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Declare variables
        const double FORMULA_MULTIPLIER = 0.5d;
        const double CONVERT_TO_KM_H = 3.6d;
        const double CONVERT_TO_FT_S = 3.281d;
        const double CONVERT_TO_GRAM = 1000d;
        const double CONVERT_TO_OUNCE = 35.274d;
        const double CONVERT_TO_KILOJOULE = 1000d;

        double PermanentEnteredObjectMass = 0d;
        double PermanentEnteredObjectVelocity = 0d;
        #endregion

        #region Properties 
        private double enteredMass = 0d;
        public double EnteredMass
        {
            get { return enteredMass; }
            set { enteredMass = value; notifyChange(); }

        }

        private double enteredVelocity = 0d;
        public double EnteredVelocity
        {
            get { return enteredVelocity; }
            set { enteredVelocity = value; notifyChange(); }
        }

        private double kineticEnergyValue = 0d;
        public double KineticEnergyValue
        {
            get { return kineticEnergyValue; }
            set { kineticEnergyValue = value; notifyChange(); }
        }

        private string errorMessage = "";
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; notifyChange(); }
        }

        private string massUnit = "";

        private string velocityUnit = "";

        private string KEUnit = "";
        #endregion

        #region Calc Function
        public void Calc()
        {
            HandleErrorSituation("", "");

            (bool isMassValueValid, double validMassValue) = ValidInputChecker(EnteredMass);
            (bool isVelocityValueValid, double validVelocityValue) = ValidInputChecker(EnteredVelocity);

            if (isMassValueValid)
            {
                EnteredMass = validMassValue;
            }
            else
            {
                HandleErrorSituation("Please enter a valid mass value. \r\n", "mass");
            }


            if (isVelocityValueValid)
            {
                EnteredVelocity = validVelocityValue;
            }
            else
            {
                HandleErrorSituation("Please enter a valid velocity value. \r\n", "velocity");
            }

            KineticEnergyValue = ConvertKineticEnergyValue(CalcKineticEnergy(EnteredMass, EnteredVelocity), KEUnit);

        }
        #endregion

        #region Change Entered Values based on changing units
        public void MassUnitChanged(string name)
        {
            massUnit = name;
            EnteredMass = ConvertMassValue(PermanentEnteredObjectMass, massUnit);
        }
        public void VelocityUnitChanged(string name)
        {
            velocityUnit = name;
            EnteredVelocity = ConvertVelocityValue(PermanentEnteredObjectVelocity, velocityUnit);
        }
        public void KEUnitChanged(string name)
        {
            KEUnit = name;
            KineticEnergyValue = ConvertKineticEnergyValue(CalcKineticEnergy(EnteredMass, EnteredVelocity), KEUnit);
        }
        #endregion

        #region Validate Input
        private (bool, double) ValidInputChecker(double userInput)
        {
            bool isValid = userInput >= 0;
            double processedValue = isValid ? userInput : 0d;
            return (isValid, processedValue);
        }
        #endregion

        #region Error Handling
        private void HandleErrorSituation(string message, string failedField)
        {
            switch (failedField)
            {
                case "velocity":
                    ErrorMessage = message;
                    KineticEnergyValue = 0;
                    break;

                case "mass":
                    ErrorMessage = message;
                    KineticEnergyValue = 0;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Calculate Kinetic Energy
        public double CalcKineticEnergy(double mass, double velocity)
        {
            return FORMULA_MULTIPLIER
                * ConvertMassValue(mass, massUnit)
                * Math.Pow(ConvertVelocityValue(velocity, velocityUnit), 2);
        }
        #endregion

        #region Convert Values Between Units
        private double ConvertVelocityValue(double velocity, string unitVelocity)
        {
            double convertedVelocity = 0d;
            switch (unitVelocity)
            {
                case "ms":
                    convertedVelocity = velocity;
                    break;
                case "kmh":
                    convertedVelocity = CONVERT_TO_KM_H * velocity;
                    break;
                case "fts":
                    convertedVelocity = CONVERT_TO_FT_S * velocity;
                    break;
                default:
                    break;
            }

            return convertedVelocity;
        }

        private double ConvertMassValue(double mass, string unitMass)
        {
            double convertedMassValue = 0d;
            switch (unitMass)
            {
                case "kg":
                    convertedMassValue = mass;
                    break;
                case "g":
                    convertedMassValue = CONVERT_TO_GRAM * mass;
                    break;
                case "oz":
                    convertedMassValue = CONVERT_TO_OUNCE * mass;
                    break;
                default:
                    break;
            }

            return convertedMassValue;
        }

        private double ConvertKineticEnergyValue(double keValue, string unitKE)
        {
            double convertedKEValue = 0d;
            switch (unitKE)
            {
                case "j":
                    convertedKEValue = keValue;
                    break;
                case "kj":
                    convertedKEValue = keValue / CONVERT_TO_KILOJOULE;
                    break;
                default:
                    break;
            }

            return convertedKEValue;
        }
        #endregion

        #region SaveOriginalVariables
        public void SavePermanentVariable(string field, string userInput)
        {
            if (field == "velocity")
            {
                PermanentEnteredObjectVelocity = Convert.ToDouble(userInput);
            }
            else
                PermanentEnteredObjectMass = Convert.ToDouble(userInput);
        }
        #endregion

        #region PropertyChanged
        private void notifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion
    }
}
