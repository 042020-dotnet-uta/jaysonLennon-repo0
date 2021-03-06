using System;
using System.Text.RegularExpressions;

namespace StoreDb
{
    /// <summary>
    /// Contains all customer-related information such as name, phone number, and address.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The ID for this <c>Customer</c> object.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The <c>Login</c> name for this customer.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The first name of this customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of this customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The address of this customer.
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// The phone number for this customer.
        /// </summary>
        public string PhoneNumber { get; set; }

        private string _Password;
        /// <summary>
        /// The password for this customer.
        /// </summary>
        /// <value>The password will always be automatically hashed when set.
        /// Therefore, this field should only ever be set with a plain-text password.</value>
        public string Password
        {
            get { return _Password; }
            set { _Password = StoreDbUtil.Hash.Sha256(value); }
        }

        /// <summary>
        /// The default <c>Location</c> where this customer's orders should be placed.
        /// </summary>
        /// <value>If this is null, then the customer has not set a default <c>Location</c>.</value>
        public virtual Location DefaultLocation { get; set; }

        /// <summary>
        /// Needed for EF.
        /// </summary>
        public Customer(){}

        /// <summary>
        /// Creates a new <c>Customer</c>.
        /// </summary>
        /// <param name="firstName">First name of customer.</param>
        public Customer(string firstName)
        {
            this.CustomerId = Guid.NewGuid();
            this.FirstName = firstName;
        }

        /// <summary>
        /// Validates the given name against validation rules.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>Whether the name passed validation rules.</returns>
        public static bool ValidateName(string name)
        {
            if (name.Trim().Length == 0 || name == null) return false;

            foreach (char c in name.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '.' || c == ' ' || c == '-') continue;
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether an email address is valid.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>Whether the email address is valid.</returns>
        public static bool ValidateEmail(string email)
        {
            var re = new Regex(@".+@.+\..+");
            return re.IsMatch(email);
        }
    }
}