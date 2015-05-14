using BrewBro.Users.Data;
using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Business
{
    public class Users
    {
        //TODO Move and to constants file (possibly configure from resources file)
        // The following constants may be changed without breaking existing hashes.
        public const int SaltByteSize = 24;
        public const int HashByteSize = 24;
        public const int PBKDF2Iterations = 1000;

        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int PBKDF2Index = 2;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, PBKDF2Iterations, HashByteSize);
            return PBKDF2Iterations + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        private static bool SlowEquals(IList<byte> a, IList<byte> b)
        {
            var diff = (uint)a.Count ^ (uint)b.Count;

            for (var i = 0; (i < a.Count) && (i < b.Count); i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2Index]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Authenticates the specified user by email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static User Authenticate(string email, string password)
        {
            IUsersRepository<User> _Repo = new UsersRepository();
            User foundByEmail = _Repo.GetByEmail(email);

            if(foundByEmail == null)
            {
                return null;
            }
            else
            {
                //hash the password and check against whats in the database
                return (ValidatePassword(password, foundByEmail.Password)) ? foundByEmail : null;
            }

            
        }
    }
}
