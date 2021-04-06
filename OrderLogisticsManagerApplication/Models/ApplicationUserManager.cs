using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using OrderLogisticsManagerApplication.Data;
using OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IServiceProvider services;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, 
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.services = services;
        }

        // TODO GET ROLE things
        

        #region Card
        public IEnumerable<Card> GetCardsByUser(ApplicationUser applicationUser)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.Card.Where(x => x.User == applicationUser);
        }

        public Card GetActiveCardByUser(ApplicationUser applicationUser)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.Card.Where(x => x.User == applicationUser && x.Status.StatusDescription == "Active").FirstOrDefault();
        }

        public IEnumerable<Card> GetCards()
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.Card;
        }

        public void CreateCard(string cardNumber, CardStatus status, ApplicationUser applicationUser)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            applicationIdentityContext.Card.Add(new Card() { CardNumber = cardNumber, Status = status, User = applicationUser});

            applicationIdentityContext.SaveChanges();
        }
        #endregion

        #region CardStatus
        public void CreateCardStatus(string statusDescription)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            applicationIdentityContext.CardStatuses.Add(new CardStatus() { StatusDescription = statusDescription });

            applicationIdentityContext.SaveChanges();
        }

        public CardStatus GetCardStatusById(int id)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();
        }

        public IEnumerable<CardStatus> GetCardStatuses()
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.CardStatuses;
        }
        #endregion

        #region UserStatus
        public void CreateUserStatus(string statusDescription)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            applicationIdentityContext.UserStatuses.Add(new UserStatus() { StatusDescription = statusDescription });

            applicationIdentityContext.SaveChanges();
        }

        public UserStatus GetUserStatusById(int id)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();
        }

        public UserStatus GetUserStatusByUser(ApplicationUser applicationUser)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == applicationUser.UserStatusId).FirstOrDefault();
        }

        public IEnumerable<UserStatus> GetUserStatuses()
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.UserStatuses;
        }
        #endregion

        #region WorkGroup
        public void CreateWorkGroup(string workGroupNumber, string workGroupName)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            applicationIdentityContext.WorkGroups.Add(new WorkGroup() {  WorkGroupNumber = workGroupNumber, WorkGroupName = workGroupName });

            applicationIdentityContext.SaveChanges();
        }

        public WorkGroup GetWorkGroupById(int id)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();
        }

        public WorkGroup GetWorkGroupByUser(ApplicationUser applicationUser)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == applicationUser.WorkGroupId).FirstOrDefault();
        }

        public WorkGroup GetWorkGroupByWorkGroupNumber(string workGroupNumber)
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupNumber == workGroupNumber).FirstOrDefault();
        }

        public IEnumerable<WorkGroup> GetWorkGroups()
        {
            var applicationIdentityContext = services.GetRequiredService<ApplicationIdentityContext>();

            return applicationIdentityContext.WorkGroups;
        }
        #endregion

    }
}
