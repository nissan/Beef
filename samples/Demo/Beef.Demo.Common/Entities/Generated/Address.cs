/*
 * This file is automatically generated; any changes will be lost. 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Beef.Entities;
using Beef.RefData;
using Newtonsoft.Json;
using RefDataNamespace = Beef.Demo.Common.Entities;

namespace Beef.Demo.Common.Entities
{
    /// <summary>
    /// Represents the Address entity.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class Address : EntityBase
    {
        #region PropertyNames
      
        /// <summary>
        /// Represents the <see cref="Street"/> property name.
        /// </summary>
        public const string Property_Street = nameof(Street);

        /// <summary>
        /// Represents the <see cref="City"/> property name.
        /// </summary>
        public const string Property_City = nameof(City);

        #endregion

        #region Privates

        private string _street;
        private string _city;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Street.
        /// </summary>
        [JsonProperty("street", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Display(Name="Street")]
        public string Street
        {
            get { return _street; }
            set { SetValue(ref _street, value, false, StringTrim.End, StringTransform.EmptyToNull, Property_Street); }
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        [JsonProperty("city", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Display(Name="City")]
        public string City
        {
            get { return _city; }
            set { SetValue(ref _city, value, false, StringTrim.End, StringTransform.EmptyToNull, Property_City); }
        }

        #endregion

        #region ICopyFrom
    
        /// <summary>
        /// Performs a copy from another <see cref="Address"/> updating this instance.
        /// </summary>
        /// <param name="from">The <see cref="Address"/> to copy from.</param>
        public override void CopyFrom(object from)
        {
            var fval = ValidateCopyFromType<Address>(from);
            CopyFrom((Address)fval);
        }
        
        /// <summary>
        /// Performs a copy from another <see cref="Address"/> updating this instance.
        /// </summary>
        /// <param name="from">The <see cref="Address"/> to copy from.</param>
        public void CopyFrom(Address from)
        {
            CopyFrom((EntityBase)from);
            Street = from.Street;
            City = from.City;

            OnAfterCopyFrom(from);
        }
    
        #endregion
        
        #region ICloneable
        
        /// <summary>
        /// Creates a deep copy of the <see cref="Address"/>.
        /// </summary>
        /// <returns>A deep copy of the <see cref="Address"/>.</returns>
        public override object Clone()
        {
            var clone = new Address();
            clone.CopyFrom(this);
            return clone;
        }
        
        #endregion
        
        #region ICleanUp

        /// <summary>
        /// Performs a clean-up of the <see cref="Address"/> resetting property values as appropriate to ensure a basic level of data consistency.
        /// </summary>
        public override void CleanUp()
        {
            base.CleanUp();
            Street = Cleaner.Clean(Street, StringTrim.End, StringTransform.EmptyToNull);
            City = Cleaner.Clean(City, StringTrim.End, StringTransform.EmptyToNull);

            OnAfterCleanUp();
        }
    
        /// <summary>
        /// Indicates whether considered initial; i.e. all properties have their initial value.
        /// </summary>
        /// <returns><c>true</c> indicates is initial; otherwise, <c>false</c>.</returns>
        public override bool IsInitial
        {
            get
            {
                return Cleaner.IsInitial(Street)
                    && Cleaner.IsInitial(City);
            }
        }

        #endregion

        #region PartialMethods
      
        partial void OnAfterCleanUp();

        partial void OnAfterCopyFrom(Address from);

        #endregion
    } 
}
