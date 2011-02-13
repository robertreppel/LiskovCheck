using System;
using System.Collections.Generic;
using System.Reflection;

namespace LiskovChecker
{
    internal class LiskovChecker    
    {
        private readonly LiskovSubstitutionPrinciple _liskovSubstitutionPrinciple;

        public LiskovChecker(LiskovSubstitutionPrinciple liskovSubstitutionPrinciple)
        {
            _liskovSubstitutionPrinciple = liskovSubstitutionPrinciple;
        }

        public List<string> Check(List<Type> types)
        {
            var validationResultList = new List<string>();

            bool noneOfTheTypesInheritFromAnythingOtherThanObject = true;
            foreach (var type in types)
            {
                string validationResult = _liskovSubstitutionPrinciple.Validate(type);

                if (validationResult.Contains("It's pointless to check for Liskov here.") == false)
                {
                    noneOfTheTypesInheritFromAnythingOtherThanObject = false;
                    validationResultList.Add(validationResult);                    
                } 
            }

            if(noneOfTheTypesInheritFromAnythingOtherThanObject)
            {
                return new List<string>() { "No subclassing was used in this assembly." };
            }
            return validationResultList;
        }

    }
}