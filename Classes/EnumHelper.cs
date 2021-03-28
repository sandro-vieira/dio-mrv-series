using System;
using System.ComponentModel;
using System.Reflection;

namespace DIO.Series
{
    public static class EnumHelper
    {
        /// <summary>
        /// Returns Enum description
        /// </summary>
        /// <param name="e">Enumerator</param>
        /// <returns>Enum description</returns>
        /// <example>
        ///     string desc = EnumHelper.GetEnumDescription(EGenero.Ficcao_Cientifica);
        ///     desc receives: "Ficção Científica"
        /// </example>
        public static string GetDescription(Enum e)
        {
            //Discovers the attributes of a field and provides access to field metadata.
            FieldInfo info = e.GetType().GetField(Enum.GetName(e.GetType(), e));

            //Specifies a description for a property or event.
            DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
 
            //Finding a custom attribute, returns its description
            if (enumAttributes.Length > 0)
                return enumAttributes[0].Description;

            //Otherwise, it returns default enumerator name
            return e.ToString();
        }
    }
}