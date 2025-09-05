using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NDCWeb.Infrastructure.Filters
{
    /// <summary>
    /// Provides conditional <see cref="RequiredAttribute"/> 
    /// validation based on related property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredIfAttribute : RequiredAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether other property's value should
        /// match or differ from provided other property's value (default is <c>false</c>).
        /// </summary>
        public bool IsInverted { get; set; } = false;

        /// <summary>
        /// Gets or sets the other property name that will be used during validation.
        /// </summary>
        /// <value>
        /// The other property name.
        /// </value>
        public string OtherProperty { get; private set; }

        /// <summary>
        /// Gets or sets the other property value that will be relevant for validation.
        /// </summary>
        /// <value>
        /// The other property value.
        /// </value>
        public object OtherPropertyValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The other property.</param>
        /// <param name="otherPropertyValue">The other property value.</param>
        public RequiredIfAttribute(string otherProperty, object otherPropertyValue)
            : base()
        {
            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext
                .ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "Could not find a property named {0}.",
                    validationContext.ObjectType, OtherProperty));
            }

            // Determine whether to run [Required] validation
            object actualOtherPropertyValue = otherPropertyInfo
                .GetValue(validationContext.ObjectInstance, null);
            if (!IsInverted && Equals(actualOtherPropertyValue, OtherPropertyValue) ||
                IsInverted && !Equals(actualOtherPropertyValue, OtherPropertyValue))
            {
                return base.IsValid(value, validationContext);
            }
            return default;
        }
    }
}