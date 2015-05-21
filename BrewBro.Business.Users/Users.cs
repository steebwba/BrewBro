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
        IRepository<User> _Repo;
        Groups _GroupBAL;

        public Users()
        {
            _Repo = new UsersRepository();
        }

        public Users(IRepository<User> repo, Groups groupBAL)
        {
            _Repo = repo;
            _GroupBAL = groupBAL;
        }


        //TODO Move and to constants file (possibly configure from resources file)
        // The following constants may be changed without breaking existing hashes.
        public const int SaltByteSize = 24;
        public const int HashByteSize = 24;
        public const int PBKDF2Iterations = 1000;

        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int PBKDF2Index = 2;

        /// <summary>
        /// Creates a hash based on the provided string.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string CreateHash(string password)
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

        private byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        private bool HashEquals(IList<byte> a, IList<byte> b)
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
        public bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2Index]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return HashEquals(hash, testHash);
        }

        public void Register(User user)
        {
            //TODO Server-side validation
            user.Password = CreateHash(user.Password);
            _Repo.Add(user);
        }

        /// <summary>
        /// Authenticates the specified user by email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public User Authenticate(string email, string password)
        {
            User foundByEmail = GetByEmail(email);

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

        /// <summary>
        /// Gets a user based on email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return _Repo.Query(u => !u.Deleted && u.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Searches users that match specified search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public List<User> Search(string searchText)
        {
            IList<User> retVal = _Repo.Query(u => !u.Deleted && (u.Name.ToLower().StartsWith(searchText.ToLower()) || u.Email.ToLower().StartsWith(searchText.ToLower())));

            RemovePasswordsFromResults(retVal);

            return retVal.ToList();
        }

        public List<User> Load(IEnumerable<Guid> ids)
        {
            IList<User> retVal = _Repo.Query(u => !u.Deleted && ids.Contains(u.Id));

            RemovePasswordsFromResults(retVal);

            return retVal.ToList();
        }

        public User Load(Guid id)
        {
            User retVal = _Repo.FindById(id);

            RemovePasswordsFromResults(retVal);

            //Dont init in constructor otherwise you'd get a circular reference leading to a stack overflow error
            if(_GroupBAL == null){
                _GroupBAL = new Groups();
            }

            retVal.Groups = _GroupBAL.FindByUser(id);

            return retVal;
        }

        private void RemovePasswordsFromResults(IEnumerable<User> users)
        {
            //Remove the password from search results for security
            //TODO possibly move password to different collection
            users.AsParallel().ForAll(u => u.Password = null);
        }

        private void RemovePasswordsFromResults(User user)
        {
            //Remove the password from search results for security
            //TODO possibly move password to different collection
            user.Password = null;
        }
    }
}
