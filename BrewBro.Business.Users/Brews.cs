using BrewBro.Users.Data;
using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Business
{
    public class Brews
    {
        IRepository<BrewHistory> _Repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Brews"/> class.
        /// </summary>
        public Brews()
        {
            _Repo = new BrewRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Brews"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="groupBAL">The group bal.</param>
        public Brews(IRepository<BrewHistory> repo)
        {
            _Repo = repo;
        }

        /// <summary>
        /// Inserts the history item for a brew event.
        /// </summary>
        /// <param name="historyItem">The history item.</param>
        public BrewHistory InsertHistory(Guid groupId, Guid userId)
        {
            BrewHistory historyItem = new BrewHistory()
            {
                Date = DateTime.Now,
                User = new User() { Id = userId },
                Group = new Group() { Id = groupId }
            };

            //Save the history item
            _Repo.Add(historyItem);

            //Now its saved, go and send the emails to notify the users in the group
            historyItem.Group = new Groups().Load(historyItem.Group.Id);

            var usersSelected = historyItem.Group.Users.Select(u => u.Id);
            historyItem.Group.Users.RemoveAll(u => !usersSelected.Contains(u.Id));

            historyItem.User = historyItem.Group.Users.First(x => x.Id == historyItem.User.Id);

            string emailUsername = ConfigurationManager.AppSettings["EmailUsername"];
            string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];

            historyItem.Group.Users.AsParallel().ForAll(x =>
            {
                bool userSelected = (x.Id == historyItem.User.Id);
                //TODO Put email content into resources file
                string subject = userSelected ? "Get the kettle on, bro!" : "Bro, There's a brew happening!";
                string body = userSelected ? "Oh man, looks like you need to get a brew on. Hop to it!" : string.Format("{0} needs to get the kettle on, give 'em a nudge!", historyItem.User.Name);


                using (MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["FromEmailAddress"], x.Email, subject, body))
                {
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Host = ConfigurationManager.AppSettings["SMTPServer"];
                        smtpClient.Timeout = 8000000;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Credentials = new NetworkCredential(emailUsername, emailPassword);

                        smtpClient.Send(mailMessage);
                    }
                }
            });


            return historyItem;
        }

        /// <summary>
        /// Loads the brew history for a group.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        public List<BrewHistory> LoadHistoryByGroup(Guid groupId)
        {
            Users userBAL = new Users();

            var history = _Repo.Query(h => h.Group.Id == groupId);

            var users = userBAL.Load(history.Select(h => h.User.Id).Distinct());


            history.AsParallel().ForAll(h =>
            {
                h.User = users.First(u => u.Id == h.User.Id);
            });

            return history.OrderByDescending(h => h.Date).ToList();
        }
    }
}
