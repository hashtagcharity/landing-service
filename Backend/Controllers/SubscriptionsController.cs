using Backend.Helpers;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Backend.Controllers
{
    public class SubscriptionsController : ApiController
    {
        protected CharityMongoContext Context { get; set; }
        private Regex _emailRegex { get; set; }

        public SubscriptionsController()
        {
            _emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            Context = new CharityMongoContext();
        }

        public Subscription Add(Subscription subscription)
        {
            if (subscription == null) throw new ArgumentNullException("subscription");
            if (String.IsNullOrEmpty(subscription.EmailAddress)) throw new ArgumentNullException("emailAddress");
            if (!_emailRegex.IsMatch(subscription.EmailAddress)) throw new ArgumentException("email address format error");

            subscription.SubscribedAt = DateTime.Now;
            subscription.SubscriptionCode = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=","");
            Context.Subscriptions.Insert(subscription);

            var unsubscribeLink = Url.Link("DefaultApi", new { Controller = "Subscriptions", Action = "Unsubscribe", id = subscription.SubscriptionCode });
            MailHelper.SendWelcomeEmail(subscription.EmailAddress, unsubscribeLink);
            return subscription;
        }

        [HttpGet]
        public StatisticsDTO Statistics()
        {
            var stats = new StatisticsDTO();
            stats.AllSubscriptions = this.Context.Subscriptions.Count(Query.NE("isUnsubscribed", BsonValue.Create(true)));
            return stats;
        }

        [HttpGet]
        public object Unsubscribe(string id)
        {
            this.Context.Subscriptions.Update(Query.EQ("subscriptionCode", id),Update.Set("isUnsubscribed",BsonValue.Create(true)));
            return true;
        }
    }
}
